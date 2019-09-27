using ProcessMemoryReaderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace projAssaultCubeAimbot
{
    public partial class Form1 : Form
    {

        Process[] pro;
        readonly ProcessMemoryReader mem = new ProcessMemoryReader();
        bool gameFound = false;

        Entity player;
        readonly List<Entity> enemies = new List<Entity>();
        int totalPlayers = -1;
        int target = -1;

        // 1 radian = 180/PI degrees
        readonly float radianToDegree = 180 / (float)Math.PI;
        readonly float[,] teleCoords = new float[2, 3];

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAttach_Click(object sender, EventArgs e)
        {
            try
            {
                // Find and Attach to game
                pro = Process.GetProcessesByName("ac_client");
                mem.ReadProcess = pro[0];
                mem.OpenProcess();

                // Below address holds total players which includes teammates
                totalPlayers = mem.ReadInt(pro[0].MainModule.BaseAddress.ToInt32() + 0x110D98);

                // Read game data, begin hacks
                SetupPlayerAndEnemy();
                gameFound = true;

                btnAttach.BackColor = Color.FromArgb(34,177,76); // Green
                btnAttach.Enabled = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Game not found!");
                throw ex;
            }
        }

        private void SetupPlayerAndEnemy()
        {
            // 10F4F4 + offsets for player
            player = new Entity(0x10F4F4, 0x40, 0x44, 0x34, 0x38, 0x3C, 0xF8)
            {
                multi = new int[] { 0x0 }
            };

            // Base address of the module "ac_client.exe" is used
            player.pointerAddress = mem.ReadMultiLevelPointer(pro[0].MainModule.BaseAddress.ToInt32() + player.baseAddr, 4, player.multi);
            int playerTeamNum = mem.ReadInt(player.pointerAddress + 0x32C);

            for (int i = 1; i <= totalPlayers; i++)
            {
                // Player, enemies and team all share same offset
                var e = new Entity(0x10F4F8, 0x40, 0x44, 0x34, 0x38, 0x3C, 0xF8)
                {
                    multi = new int[] { 0x4 * i, 0x0 }
                };
                e.pointerAddress = mem.ReadMultiLevelPointer(pro[0].MainModule.BaseAddress.ToInt32() + e.baseAddr, 4, e.multi);
                if(mem.ReadInt(e.pointerAddress + 0x32C) != playerTeamNum) this.enemies.Add(e); // Add enemies of other team. Solution only works in TDM
            }
        }

        private void TmrProcess_Tick(object sender, EventArgs e)
        {
            if (gameFound && !pro[0].HasExited)
            {
                GetCurrentData();

                // AIMBOT - right mouse click
                if ((ProcessMemoryReaderApi.GetKeyState(02) & 0x8000) != 0) Aimbot();

                // Client sided Health hack - (F5 Key)
                if ((ProcessMemoryReaderApi.GetKeyState(0x74) & 0x8000) != 0)
                {
                    mem.WriteInt(player.pointerAddress + player.health, 999);
                }

                #region Teleporter

                // VK_F1 = 0x70, Save Current location as A
                if ((ProcessMemoryReaderApi.GetKeyState(0x70) & 0x8000) != 0)
                {
                    SaveCoords(0);
                }

                // VK_F2 = 0x71, Save Current location as B
                if ((ProcessMemoryReaderApi.GetKeyState(0x71) & 0x8000) != 0)
                {
                    SaveCoords(1);
                }

                // VK_F3 = 0x72, Load location A
                if ((ProcessMemoryReaderApi.GetKeyState(0x72) & 0x8000) != 0)
                {
                    LoadCoords(0);
                }

                // VK_F4 = 0x73, Load location B
                if ((ProcessMemoryReaderApi.GetKeyState(0x73) & 0x8000) != 0)
                {
                    LoadCoords(1);
                }

                #endregion

            }//if gamefound
            else
            {
                gameFound = false;
                btnAttach.BackColor = Color.FromArgb(220, 61, 65); // Red
                btnAttach.Enabled = true;
            }
        }

        private void SaveCoords(int index)
        {
            teleCoords[index, 0] = mem.ReadFloat(player.pointerAddress + player.xPos);
            teleCoords[index, 1] = mem.ReadFloat(player.pointerAddress + player.yPos);
            teleCoords[index, 2] = mem.ReadFloat(player.pointerAddress + player.zPos);
        }

        private void LoadCoords(int index)
        {
            mem.WriteFloat(player.pointerAddress + player.xPos, teleCoords[index, 0]);
            mem.WriteFloat(player.pointerAddress + player.yPos, teleCoords[index, 1]);
            mem.WriteFloat(player.pointerAddress + player.zPos, teleCoords[index, 2]);
        }

        /// <summary>
        /// Read the player and enemy data from memory
        /// </summary>
        private void GetCurrentData()
        {
            // Read player data, store and display it
            player.PosX_Value = mem.ReadFloat(player.pointerAddress + player.xPos);
            player.PosY_Value = mem.ReadFloat(player.pointerAddress + player.yPos);
            player.PosZ_Value = mem.ReadFloat(player.pointerAddress + player.zPos);
            player.Health_Value = mem.ReadInt(player.pointerAddress + player.health);

            lblXpos.Text = player.PosX_Value.ToString("0.00");
            lblZpos.Text = player.PosZ_Value.ToString("0.00");
            lblYpos.Text = player.PosY_Value.ToString("0.00");
            lblHealth.Text = player.Health_Value.ToString();

            // Read and store enemy position data
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].PosX_Value = mem.ReadFloat(enemies[i].pointerAddress + enemies[i].xPos);
                enemies[i].PosY_Value = mem.ReadFloat(enemies[i].pointerAddress + enemies[i].yPos);
                enemies[i].PosZ_Value = mem.ReadFloat(enemies[i].pointerAddress + enemies[i].zPos);
                enemies[i].Health_Value = mem.ReadInt(enemies[i].pointerAddress + enemies[i].health);
            }
        }

        private void Aimbot()
        {
            // enemy count hasn't changed
            if (totalPlayers == mem.ReadInt(pro[0].MainModule.BaseAddress.ToInt32() + 0x110D98))
            {
                // player alive
                if (player.Health_Value > 0)
                {
                    // if no target selected, or target dead
                    //if (target == -1 || enemies[target].Health_Value <= 0)
                    //{
                        // find new aimbot target
                        target = FindClosestEnemyIndex();
                    //}
                    //else
                    //{
                        AimAtTarget(enemies[target]);
                    //}
                    //Debug.WriteLine(enemies[target].Health_Value);
                    
                }
            }
            else
            {
                // new server or game mode has been chosen
                totalPlayers = mem.ReadInt(pro[0].MainModule.BaseAddress.ToInt32() + 0x110D98);
                target = -1;
                SetupPlayerAndEnemy();
            }

        }

        private int FindClosestEnemyIndex()
        {
            float[] distances = new float[enemies.Count];

            for (int i = 0; i < enemies.Count; i++)
            {
                //if enemy is alive
                if (enemies[i].Health_Value > 0)
                {
                    distances[i] = Get3dDistance(enemies[i]);
                }
                else
                {
                    //Dead enemy will not be aimed at
                    distances[i] = float.MaxValue;
                }
            }

            int closest = 0;
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < distances[closest])
                {
                    closest = i;
                }
            }

            return closest;
        }

        private float Get3dDistance(Entity e)
        {
            // Pythagoras for 3D vector magnitude
            return (float)
                (Math.Sqrt(
                    Math.Pow((e.PosX_Value - player.PosX_Value), 2) +
                    Math.Pow((e.PosY_Value - player.PosY_Value), 2) +
                    Math.Pow((e.PosZ_Value - player.PosZ_Value), 2)
                ));
        }

        private void AimAtTarget(Entity e)
        {
            // [asin] returns arc sine of x in the interval -PI/2 to PI/2
            float pitch = (float)Math.Asin((e.PosZ_Value - player.PosZ_Value) / Get3dDistance(e)) * radianToDegree;

            // [atan2] returns arc tangent of y/x in the interval -PI to PI radians
            float yaw = -(float)Math.Atan2(e.PosX_Value - player.PosX_Value, e.PosY_Value - player.PosY_Value) * radianToDegree + 180;

            // pitch is for Y, which looks up and down
            mem.WriteFloat(player.pointerAddress + player.yMouse, pitch);

            // yaw is for X, which looks left and right
            mem.WriteFloat(player.pointerAddress + player.xMouse, yaw);
        }
    }
}
