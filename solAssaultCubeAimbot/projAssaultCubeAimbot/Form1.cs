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
        ProcessModule module;
        ProcessMemoryReader mem = new ProcessMemoryReader();

        Player p;
        List<Player> e = new List<Player>();
        
        bool gameFound = false;
        int target = -1;

        // 1 radian = 180/PI degrees
        float radianToDegree = 180 / (float)Math.PI;

        float[,] teleCoords = new float[2, 3];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            try
            {
                // Find and Attach to game
                pro = Process.GetProcessesByName("ac_client");
                module = pro[0].MainModule;
                mem.ReadProcess = pro[0];
                mem.OpenProcess();
                
                // Read game data, begin hacks
                setupPlayerAndEnemy();
                gameFound = true;

                btnAttach.BackColor = Color.Green; //User Feedback
                btnAttach.Enabled = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Game not found!");
                //throw ex;
            }
        }

        private void setupPlayerAndEnemy()
        {
            // Example: "ac_client.exe" + 10F4F4 + offsets
            p = new Player(0x10F4F4, 0x40, 0x44, 0x34, 0x38, 0x3C, 0xF8)
            {
                multi = new int[] { 0x0 }
            };
            p.pointerAddress = mem.ReadMultiLevelPointer(pro[0].MainModule.BaseAddress.ToInt32() + p.baseAddr , 4 , p.multi);

            // TODO: Find out how many enemies in game
            int totalEnemies = 1;
            for(int i = 1; i <= totalEnemies; i++)
            {
                // Player, enemies and team all share same offset
                var e = new Player(0x10F4F8 , 0x40 , 0x44 , 0x34 , 0x38 , 0x3C , 0xF8);
                e.multi = new int[] { 0x4*i, 0x0 };
                e.pointerAddress = mem.ReadMultiLevelPointer(pro[0].MainModule.BaseAddress.ToInt32() + e.baseAddr , 4 , e.multi);
                this.e.Add(e);
            }            
        }

        private void tmrProcess_Tick(object sender, EventArgs e)
        {
            if (gameFound && !pro[0].HasExited)
            {
                getCurrentData();

                // AIMBOT - right mouse click
                if ((ProcessMemoryReaderApi.GetKeyState(02) & 0x8000) != 0)
                {
                    Aimbot();
                }

                // Client sided Health hack - (F5 Key)
                if ((ProcessMemoryReaderApi.GetKeyState(0x74) & 0x8000) != 0)
                {
                    mem.WriteInt(p.pointerAddress + p.health , 999);
                }

                //TELEPORTER

                /*
                VK_F1 = 0x70, SAVE 1
                VK_F2 = 0x71, SAVE 2
                VK_F3 = 0x72, LOAD 1
                VK_F4 = 0x73, LOAD 2
                */

                if ((ProcessMemoryReaderApi.GetKeyState(0x70) & 0x8000) != 0)
                {
                    teleCoords[0 , 0] = mem.ReadFloat(p.pointerAddress + p.xPos);
                    teleCoords[0 , 1] = mem.ReadFloat(p.pointerAddress + p.yPos);
                    teleCoords[0 , 2] = mem.ReadFloat(p.pointerAddress + p.zPos);
                }
                else
                {
                    target = -1;
                }

                if ((ProcessMemoryReaderApi.GetKeyState(0x71) & 0x8000) != 0)
                {
                    teleCoords[1 , 0] = mem.ReadFloat(p.pointerAddress + p.xPos);
                    teleCoords[1 , 1] = mem.ReadFloat(p.pointerAddress + p.yPos);
                    teleCoords[1 , 2] = mem.ReadFloat(p.pointerAddress + p.zPos);
                }
                else
                {
                    target = -1;
                }

                if ((ProcessMemoryReaderApi.GetKeyState(0x72) & 0x8000) != 0)
                {
                    mem.WriteFloat(p.pointerAddress + p.xPos , teleCoords[0 , 0]);
                    mem.WriteFloat(p.pointerAddress + p.yPos , teleCoords[0 , 1]);
                    mem.WriteFloat(p.pointerAddress + p.zPos , teleCoords[0 , 2]);
                }
                else
                {
                    target = -1;
                }

                if ((ProcessMemoryReaderApi.GetKeyState(0x73) & 0x8000) != 0)
                {
                    mem.WriteFloat(p.pointerAddress + p.xPos , teleCoords[1 , 0]);
                    mem.WriteFloat(p.pointerAddress + p.yPos , teleCoords[1 , 1]);
                    mem.WriteFloat(p.pointerAddress + p.zPos , teleCoords[1 , 2]);
                }
                else
                {
                    target = -1;
                }
                
            }//if gamefound
            else
            {
                gameFound = false;
                btnAttach.BackColor = Color.Red;
                btnAttach.Enabled = true;
            }
        }

        private void getCurrentData()
        {
            // Read Data, store it within objects and labels
            p.PosX_Value = mem.ReadFloat(p.pointerAddress + p.xPos);
            p.PosY_Value = mem.ReadFloat(p.pointerAddress + p.yPos);
            p.PosZ_Value = mem.ReadFloat(p.pointerAddress + p.zPos);
            p.Health_Value = mem.ReadInt(p.pointerAddress + p.health);

            //for (int i = 0; i < e.Count; i++)
            //{
            //
            //}
            e[0].PosX_Value = mem.ReadFloat(e[0].pointerAddress + e[0].xPos);
            e[0].PosY_Value = mem.ReadFloat(e[0].pointerAddress + e[0].yPos);
            e[0].PosZ_Value = mem.ReadFloat(e[0].pointerAddress + e[0].zPos);
            e[0].Health_Value = mem.ReadInt(e[0].pointerAddress + e[0].health);

            lblXpos.Text = p.PosX_Value.ToString("0.00");
            lblZpos.Text = p.PosZ_Value.ToString("0.00");
            lblYpos.Text = p.PosY_Value.ToString("0.00");
            lblHealth.Text = p.Health_Value.ToString();

            lblXposEn.Text = e[0].PosX_Value.ToString("0.00");
            lblYposEn.Text = e[0].PosY_Value.ToString("0.00");
            lblZposEn.Text = e[0].PosZ_Value.ToString("0.00");
            lblHealthEn.Text = e[0].Health_Value.ToString();
        }

        private void Aimbot()
        {
            if (p.Health_Value > 0) // player alive
            {
                if (target == -1 || e[target].health <= 0) // if no target selected, or target dead
                {
                    target = FindClosestEnemyIndex(); // find new aimbot target
                }

                AimAtTarget(e[target]);

            }//if alive
        }

        private int FindClosestEnemyIndex()
        {
            float[] distances = new float[e.Count]; 

            for (int i = 0; i < e.Count; i++)
            {
                if (e[i].health > 0)//if enemy is alive
                {
                    distances[i] = Get3dDistance(e[i]);
                }
                else
                {
                    distances[i] = float.MaxValue; //Dead enemy will not be aimed at
                }
            }

            int closest = 0;
            for(int i = 0; i < distances.Length; i++)
            {
                if(distances[i] < distances[closest])
                {
                    closest = i;
                }
            }

            return closest;
        }

        private float Get3dDistance(Player e)
        {
            // Pythagoras for 3D vector magnitude
            return (float)
                (Math.Sqrt(
                    Math.Pow((e.PosX_Value - p.PosX_Value) , 2) +
                    Math.Pow((e.PosY_Value - p.PosY_Value) , 2) +
                    Math.Pow((e.PosZ_Value - p.PosZ_Value) , 2)
                ));
        }

        private void AimAtTarget(Player e)
        {
            // [asin] returns arc sine of x in the interval -PI/2 to PI/2
            float pitch = (float)Math.Asin((e.PosZ_Value - p.PosZ_Value) / Get3dDistance(e)) * radianToDegree;

            // [atan2] returns arc tangent of y/x in the interval -PI to PI radians
            float yaw = -(float)Math.Atan2(e.PosX_Value - p.PosX_Value, e.PosY_Value - p.PosY_Value) * radianToDegree + 180;

            // pitch is for Y, which looks up and down
            mem.WriteFloat(p.pointerAddress + p.yMouse, pitch);

            // yaw is for X, which looks left and right
            mem.WriteFloat(p.pointerAddress + p.xMouse, yaw);
        }
    }
}
