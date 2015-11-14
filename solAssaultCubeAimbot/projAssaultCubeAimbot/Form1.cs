using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessMemoryReaderLib;
using System.Globalization;
using System.Diagnostics;

namespace projAssaultCubeAimbot
{
    public partial class Form1 : Form
    {

        Process[] allProcesses;
        Process acClient;
        ProcessModule mainModule;
        ProcessMemoryReader mem = new ProcessMemoryReader();
        PlayerDataAddresses.PlayerData mainPlayer = new PlayerDataAddresses.PlayerData();

        //Addresses
        int playerBase = 0x509B74; //find player using the base address
        int[] playerMultiLevel = new int[] { 0x30 }; //offset for the base address to create the pointer. array is used so games with multiple offsets can be used
        PlayerDataAddresses playerOffsets = new PlayerDataAddresses(0x10, 0x14, 0x4, 0xC, 0x8, 0xC8);//mouse x + y, xyz, health


        List<PlayerDataAddresses.PlayerData> enemyAddresses = new List<PlayerDataAddresses.PlayerData>();
        int[] enemyOneMultiLevel = new int[] { 0x1c, 0x4, 0x28, 0x30 }; //offset for base pointer which brings us to address before X axis

        float PI = 3.14159265f;//math.Pi could be used but returns a double
        bool gameFound = false;
        int currentTarget = -1;

        float X = 0.0f;
        float Y = 0.0f;
        float Z = 0.0f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrProcess.Enabled = true;
        }

        private void SetupEnemyVars()
        {
            PlayerDataAddresses.PlayerData enemyOne = new PlayerDataAddresses.PlayerData();
            //"ac_client.exe"+0010F30C offsets 1c,4,28,30
            enemyOne.baseAddr = acClient.MainModule.BaseAddress.ToInt32() + 0x0010F30C; //base address
            enemyOne.multiLevel = enemyOneMultiLevel; //pointer offsets
            enemyOne.offsets = mainPlayer.offsets; //Enemy has the same offsets as player for the health etc
            enemyAddresses.Add(enemyOne);//add this enemy to our list
        }

        private void tmrProcess_Tick(object sender, EventArgs e)
        {
            if (gameFound)
            {
                int playerBase = mem.ReadMultiLevelPointer(mainPlayer.baseAddr, 4, mainPlayer.multiLevel); //store actual pointer location
                lblXpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.xPos).ToString(); //use pointer with X axis offset, store that float value as a string in label
                lblYpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.yPos).ToString();
                lblZpos.Text = mem.ReadFloat(playerBase + mainPlayer.offsets.zPos).ToString();
                lblHealth.Text = mem.ReadInt(playerBase + mainPlayer.offsets.health).ToString();

                int enemyBase = mem.ReadMultiLevelPointer(enemyAddresses[0].baseAddr, 4, enemyAddresses[0].multiLevel); //store actual pointer location
                lblXposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.xPos).ToString(); //use pointer with X axis offset, store that float value as a string in label
                lblYposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.yPos).ToString();
                lblZposEn.Text = mem.ReadFloat(enemyBase + mainPlayer.offsets.zPos).ToString();
                lblHealthEn.Text = mem.ReadInt(enemyBase + mainPlayer.offsets.health).ToString(); //same offset as player

                int aimHotkey = ProcessMemoryReaderApi.GetKeyState(02);//right mouse click, virtual key with C++ function
                if ((aimHotkey & 0x8000) != 0) //8000 checks that we are holding the correct vKey?
                {
                    Aimbot();
                }
                else
                {
                    currentTarget = -1;
                }

                //TELEPORTER BELOW WORKS BUT THE HOTKEYS ARE FUCKED. THEY WONT RESPOND UNLESS ITS LEFT OR RIGHT MOUSE CLICK

                //int teleSaveHotkey = ProcessMemoryReaderApi.GetKeyState(02);//up 26
                //if ((teleSaveHotkey & 0x8000) != 0)
                //{
                //    X = mem.ReadFloat(playerBase + mainPlayer.offsets.xPos);
                //    Y = mem.ReadFloat(playerBase + mainPlayer.offsets.yPos);
                //    Z = mem.ReadFloat(playerBase + mainPlayer.offsets.zPos);
                //    //MessageBox.Show(string.Format("Saved:X={0} Y={1} Z={2}", X, Y, Z));
                //}
                //else
                //{
                //    currentTarget = -1;
                //}

                //int teleJumpHotkey = ProcessMemoryReaderApi.GetKeyState(01);//down 28
                //if ((teleJumpHotkey & 0x8000) != 0)
                //{
                //    mem.WriteFloat(playerBase + mainPlayer.offsets.xPos, X);
                //    mem.WriteFloat(playerBase + mainPlayer.offsets.yPos, Y);
                //    mem.WriteFloat(playerBase + mainPlayer.offsets.zPos, Z);
                //    //MessageBox.Show(string.Format("Jumping:X={0} Y={1} Z={2}", X, Y, Z));
                //}
                //else
                //{
                //    currentTarget = -1;
                //}
            }//if

            try
            {
                //if (allProcesses != null)
                if (acClient != null)
                {
                    //if (allProcesses[0].HasExited) //run program as admin! to check that assaultCube process has ended or not
                    if (acClient.HasExited) //run program as admin! to check that assaultCube process has ended or not
                    {
                        gameFound = false; //game has ended so stop performing readMemory etc
                        btnAttach.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //throw ex;
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

        private PlayerDataAddresses.PlayerDataVec GetPlayerData(PlayerDataAddresses.PlayerData updatePlayer) //CHECK WHY THERE IS AN UNUSED ARGUMENT
        {
            //This method constantly reads in game values

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
                ((to.xPos - from.xPos) * (to.xPos - from.xPos)) +
                ((to.yPos - from.yPos) * (to.yPos - from.yPos)) +
                ((to.zPos - from.zPos) * (to.zPos - from.zPos))
                ));
        }

        private void AimAtTarget(PlayerDataAddresses.PlayerDataVec enemyVector, PlayerDataAddresses.PlayerDataVec playerVector)
        {
            //Learn aimbot math below, may not work in other games

            int playerBase = mem.ReadMultiLevelPointer(mainPlayer.baseAddr, 4, mainPlayer.multiLevel);

            //yaw is for X, which looks left and right
            //pitch is for Y, which looks up and down
            //roll is for Z, which isn't used in this case
            float pitch1 = (float)Math.Atan2(enemyVector.zPos - playerVector.zPos, Get3dDistance(enemyVector, playerVector)) * 180 / PI; //finding the new value of where to aim the mouse
            float pitch2 = (float)Math.Asin((enemyVector.zPos - playerVector.zPos) / Get3dDistance(enemyVector, playerVector)) * 180 / PI; //New pitch Code suggestion by fleep or youtube comment
            float RotationV = (float)(Math.Atan2(enemyVector.zPos - playerVector.zPos, Math.Sqrt((enemyVector.xPos - playerVector.xPos) * (enemyVector.xPos - playerVector.xPos) + (enemyVector.yPos - playerVector.yPos) * (enemyVector.yPos - playerVector.yPos))) * 180.00 / Math.PI);//New Code suggestion by creator of other aimbot...much more advanced

            float yawX = -(float)Math.Atan2(enemyVector.xPos - playerVector.xPos, enemyVector.yPos - playerVector.yPos)
                / PI * 180 + 180;

            //Either option 1,2 or 3 for the yMouse. Learn the calculations
            mem.WriteFloat(playerBase + mainPlayer.offsets.xMouse, yawX);

            //mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, pitch1);
            //mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, pitch2);
            mem.WriteFloat(playerBase + mainPlayer.offsets.yMouse, RotationV);
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            allProcesses = Process.GetProcesses();
            foreach( Process p in allProcesses)
            {
                if (p.ProcessName.ToString().Contains("ac_client"))
                {
                    acClient = p;
                    mainModule = acClient.MainModule;//get module of the process?
                    mem.ReadProcess = acClient;//read the process
                    mem.OpenProcess();//open the process
                    gameFound = true;

                    mainPlayer.baseAddr = playerBase; //create a player object and set the base addr to the global int variable so it is easy to change
                    mainPlayer.multiLevel = playerMultiLevel; //set offset for players base addr to create the pointer

                    //this is xzY. for some reason xYz stops aimbot working
                    mainPlayer.offsets = new PlayerDataAddresses(playerOffsets.xMouse, playerOffsets.yMouse, playerOffsets.xPos, playerOffsets.zPos, playerOffsets.yPos, playerOffsets.health);//ISSUE HERE

                    SetupEnemyVars();
                    btnAttach.BackColor = Color.Green;
                    break;
                }
            }
        }
    }
}
