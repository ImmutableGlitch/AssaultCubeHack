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

        Process[] AC;
        ProcessModule mainModule;
        ProcessMemoryReader mem = new ProcessMemoryReader();
        PlayerDataAddresses.PlayerData mainPlayer = new PlayerDataAddresses.PlayerData();

        //Addresses
        int playerBase = 0x509B74; //find player using the base address
        //int[] playerMultiLevel = new int[] { 0x30 }; //offset for the base address to create the pointer. array is used so games with multiple offsets can be used
        int[] playerMultiLevel = new int[] { 0x0 }; //The offset of 30 was only used to bring us closer to relevant player struct
        //This means any code below using this variable will need to be removed/modified
        //PlayerDataAddresses playerOffsets = new PlayerDataAddresses(0x10, 0x14, 0x4, 0xC, 0x8, 0xC8);//mouse x + y, xyz, health
        PlayerDataAddresses playerOffsets = new PlayerDataAddresses(0x40, 0x44, 0x34, 0x3C, 0x38, 0xF8); //Same Offsets with the extra 30 added


        List<PlayerDataAddresses.PlayerData> enemyAddresses = new List<PlayerDataAddresses.PlayerData>();
        int[] enemyOneMultiLevel = new int[] { 0x1c, 0x4, 0x28, 0x30 }; //offset for base pointer which brings us to address before X axis

        float PI = 3.14159265f;//math.Pi could be used but returns a double
        bool gameFound = false;
        int currentTarget = -1;

        float[,] teleCoords = new float[2, 3];

        public Form1()
        {
            InitializeComponent();
        }

        private void SetupEnemyVars()
        {
            PlayerDataAddresses.PlayerData enemyOne = new PlayerDataAddresses.PlayerData();
            //"ac_client.exe"+0010F30C offsets 1c,4,28,30
            enemyOne.baseAddr = AC[0].MainModule.BaseAddress.ToInt32() + 0x0010F30C; //base address
            enemyOne.multiLevel = enemyOneMultiLevel; //pointer offsets
            enemyOne.offsets = mainPlayer.offsets; //Enemy has the same offsets as player for the health etc
            enemyAddresses.Add(enemyOne);
        }

        private void tmrProcess_Tick(object sender, EventArgs e)
        {
            if (gameFound && !AC[0].HasExited)
            {
                int playerBase = mem.ReadMultiLevelPointer(mainPlayer.baseAddr, 4, mainPlayer.multiLevel); //Returns the current base address in memory
                int enemyBase = mem.ReadMultiLevelPointer(enemyAddresses[0].baseAddr, 4, enemyAddresses[0].multiLevel);

                //Upodate labels with data
                lblXpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.xPos).ToString();
                lblYpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.yPos).ToString();
                lblZpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.zPos).ToString();
                lblHealth.Text = mem.ReadInt(playerBase + mainPlayer.offsets.health).ToString();

                lblXposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.xPos).ToString();
                lblYposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.yPos).ToString();
                lblZposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.zPos).ToString();
                lblHealthEn.Text = mem.ReadInt(enemyBase + mainPlayer.offsets.health).ToString(); //same offset as player

                //Health hack, doesnt work online
                //mem.WriteInt(playerBase + mainPlayer.offsets.health, 999);

                //AIMBOT
                int aimHotkey = ProcessMemoryReaderApi.GetKeyState(02);//right mouse click, virtual key with C++ function
                if ((aimHotkey & 0x8000) != 0) //CHECK: why offset/hex is needed here
                {
                    Aimbot();
                }
                else
                {
                    currentTarget = -1;
                }

                //TELEPORTER

                /*
                VK_F1 = 0x70, SAVE 1
                VK_F2 = 0x71, SAVE 2
                VK_F3 = 0x72, LOAD 1
                VK_F4 = 0x73, LOAD 2
                */

                int teleSave1 = ProcessMemoryReaderApi.GetKeyState(0x70);
                if ((teleSave1 & 0x8000) != 0)
                {
                    teleCoords[0, 0] = mem.ReadFloat(playerBase + mainPlayer.offsets.xPos);
                    teleCoords[0, 1] = mem.ReadFloat(playerBase + mainPlayer.offsets.yPos);
                    teleCoords[0, 2] = mem.ReadFloat(playerBase + mainPlayer.offsets.zPos);
                }
                else
                {
                    currentTarget = -1;
                }

                int teleSave2 = ProcessMemoryReaderApi.GetKeyState(0x71);
                if ((teleSave2 & 0x8000) != 0)
                {
                    teleCoords[1, 0] = mem.ReadFloat(playerBase + mainPlayer.offsets.xPos);
                    teleCoords[1, 1] = mem.ReadFloat(playerBase + mainPlayer.offsets.yPos);
                    teleCoords[1, 2] = mem.ReadFloat(playerBase + mainPlayer.offsets.zPos);
                }
                else
                {
                    currentTarget = -1;
                }

                int teleJump1 = ProcessMemoryReaderApi.GetKeyState(0x72);
                if ((teleJump1 & 0x8000) != 0)
                {
                    mem.WriteFloat(playerBase + mainPlayer.offsets.xPos, teleCoords[0, 0]);
                    mem.WriteFloat(playerBase + mainPlayer.offsets.yPos, teleCoords[0, 1]);
                    mem.WriteFloat(playerBase + mainPlayer.offsets.zPos, teleCoords[0, 2]);
                }
                else
                {
                    currentTarget = -1;
                }

                int teleJump2 = ProcessMemoryReaderApi.GetKeyState(0x73);
                if ((teleJump2 & 0x8000) != 0)
                {
                    mem.WriteFloat(playerBase + mainPlayer.offsets.xPos, teleCoords[1, 0]);
                    mem.WriteFloat(playerBase + mainPlayer.offsets.yPos, teleCoords[1, 1]);
                    mem.WriteFloat(playerBase + mainPlayer.offsets.zPos, teleCoords[1, 2]);
                }
                else
                {
                    currentTarget = -1;
                }
            }//if gamefound
            else
            {
                gameFound = false; //game has ended so stop performing readMemory etc
                btnAttach.BackColor = Color.Red;
                btnAttach.Enabled = true;
                //Blank all labels, useful if many labels are on form
                //foreach (Control label in this.Controls.OfType<Label>())
                //{
                //    label.Text = "";
                //}
            }
        }

        private void Aimbot()
        {

            PlayerDataAddresses.PlayerDataVec playerDataVec = GetPlayerData(mainPlayer);
            List<PlayerDataAddresses.PlayerDataVec> enemiesVectorList = new List<PlayerDataAddresses.PlayerDataVec>();

            for (int i = 0; i < enemyAddresses.Count; i++)
            {
                PlayerDataAddresses.PlayerDataVec enemyVector = GetPlayerData(enemyAddresses[i]);
                enemiesVectorList.Add(enemyVector);
            }

            if (playerDataVec.health > 0) //if player is currently alive
            {
                int newTarget = 0;

                //STEP 1: Find a target to aim at
                if (currentTarget != -1) //locked onto enemy
                {
                    if (enemiesVectorList[currentTarget].health > 0) //if enemy is alive
                    {
                        newTarget = currentTarget; //use this enemy for aimbot
                    }
                    else
                    {
                        newTarget = FindClosestEnemyIndex(enemiesVectorList.ToArray(), playerDataVec); //find new aimbot target
                    }
                }
                else
                {
                    newTarget = FindClosestEnemyIndex(enemiesVectorList.ToArray(), playerDataVec);//find new aimbot target
                }


                //STEP 2: Aim at existing or new target
                if (newTarget != -1)//Make certain that target has been set
                {
                    currentTarget = newTarget; //set currentTarget for next aimbot iteration, which will either keep the same target or assign a new one
                    if (enemiesVectorList[newTarget].health > 0)//if target is alive
                    {
                        AimAtTarget(enemiesVectorList[newTarget], playerDataVec); //aim at him
                    }
                }
            }//if alive
        }

        private PlayerDataAddresses.PlayerDataVec GetPlayerData(PlayerDataAddresses.PlayerData updatePlayer)
        {
            //This method is called non-stop to read in game values

            //Create object that will store the in game values
            PlayerDataAddresses.PlayerDataVec playerReturn = new PlayerDataAddresses.PlayerDataVec();

            //get the in game base address
            int playerBase = mem.ReadMultiLevelPointer(updatePlayer.baseAddr, 4, updatePlayer.multiLevel);

            //use the base address along with the correct offset to store the in game value
            playerReturn.xMouse = mem.ReadFloat(playerBase + updatePlayer.offsets.xMouse);
            playerReturn.yMouse = mem.ReadFloat(playerBase + updatePlayer.offsets.yMouse);
            playerReturn.xPos = mem.ReadFloat(playerBase + updatePlayer.offsets.xPos);
            playerReturn.yPos = mem.ReadFloat(playerBase + updatePlayer.offsets.yPos);
            playerReturn.zPos = mem.ReadFloat(playerBase + updatePlayer.offsets.zPos);
            playerReturn.health = mem.ReadInt(playerBase + updatePlayer.offsets.health);
            return playerReturn;
        }

        private int FindClosestEnemyIndex(PlayerDataAddresses.PlayerDataVec[] enemiesVectorArray, PlayerDataAddresses.PlayerDataVec myPosition)
        {
            //TODO: Clean this method up, then rename all variables in this class and PlayerInfo. Use structs instead of class
            float[] distances = new float[enemiesVectorArray.Length]; //array of distances depending on number of enemy targets

            for (int i = 0; i < enemiesVectorArray.Length; i++)
            {
                if (enemiesVectorArray[i].health > 0)//if enemy is alive
                {
                    distances[i] = Get3dDistance(enemiesVectorArray[i], myPosition);
                }
                else
                {
                    distances[i] = float.MaxValue;//very high value assigned, represents target that is far away so will not be aimed at
                }
            }

            float[] distances2 = new float[distances.Length];
            Array.Copy(distances, distances2, distances.Length); //copy first array to second, poop efficiency
            Array.Sort(distances2); //pos 0 = shortest distance

            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] == distances2[0]) // if current distance is the shortest to enemy target
                {
                    lblDistance.Text = distances[i].ToString();
                    return i; //return the index of that enemy
                }
            }

            return -1;
        }

        private float Get3dDistance(PlayerDataAddresses.PlayerDataVec to, PlayerDataAddresses.PlayerDataVec from)
        {
            return (float)
                (Math.Sqrt(
                Math.Pow((to.xPos - from.xPos), 2) +
                Math.Pow((to.yPos - from.yPos), 2) +
                Math.Pow((to.zPos - from.zPos), 2)
                ));
        }

        private void AimAtTarget(PlayerDataAddresses.PlayerDataVec enemyVector, PlayerDataAddresses.PlayerDataVec playerVector)
        {
            //Learn aimbot math below, may not work in other games

            int playerBase = mem.ReadMultiLevelPointer(mainPlayer.baseAddr, 4, mainPlayer.multiLevel);

            //yaw is for X, which looks left and right
            //pitch is for Y, which looks up and down
            //roll is for Z, which isn't used in this case
            float pitch1 = (float)Math.Atan2(enemyVector.zPos - playerVector.zPos, Get3dDistance(enemyVector, playerVector)) * 180 / PI; //First method uses Atan2, not sure who suggested
            float pitch2 = (float)Math.Asin((enemyVector.zPos - playerVector.zPos) / Get3dDistance(enemyVector, playerVector)) * 180 / PI; //Method used by Fleep
            float RotationV = (float)(Math.Atan2(enemyVector.zPos - playerVector.zPos, Math.Sqrt((enemyVector.xPos - playerVector.xPos) * (enemyVector.xPos - playerVector.xPos) + (enemyVector.yPos - playerVector.yPos) * (enemyVector.yPos - playerVector.yPos))) * 180.00 / Math.PI);//New Code suggestion by creator of other aimbot...much more advanced

            float yawX = -(float)Math.Atan2(enemyVector.xPos - playerVector.xPos, enemyVector.yPos - playerVector.yPos)
                / PI * 180 + 180;

            mem.WriteFloat(playerBase + mainPlayer.offsets.xMouse, yawX);

            //Either option 1,2 or 3 for the yMouse. Learn the calculations
            //mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, pitch1);
            //mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, pitch2);
            mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, RotationV);
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            try
            {
                AC = Process.GetProcessesByName("ac_client");
                mainModule = AC[0].MainModule;
                mem.ReadProcess = AC[0];
                mem.OpenProcess();
                gameFound = true;

                mainPlayer.baseAddr = playerBase; //Global address variable
                mainPlayer.multiLevel = playerMultiLevel; //set offset for base addr to create the pointer

                mainPlayer.offsets = new PlayerDataAddresses(playerOffsets.xMouse, playerOffsets.yMouse, playerOffsets.xPos, playerOffsets.zPos, playerOffsets.yPos, playerOffsets.health);//CHECK: Why xzY, and not xYz

                SetupEnemyVars();
                btnAttach.BackColor = Color.Green; //User Feedback
                btnAttach.Enabled = false;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Game not found!");
                //throw ex;
            }
        }
    }
}
