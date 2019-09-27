using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projAssaultCubeAimbot
{
    class PlayerDataAddresses
    {
        public int xMouse, yMouse, xPos, yPos, zPos, health; //addresses stored as int

        public PlayerDataAddresses(int xMouse, int yMouse, int xPos, int yPos, int zPos, int health)
        {
            this.xMouse = xMouse;
            this.yMouse = yMouse;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.health = health;
        }

        public struct PlayerData
        {
            
            public int baseAddr;
            public int[] multiLevel;
            public PlayerDataAddresses offsets;
        }

        public struct PlayerDataVec //rename all instances of vec or data vec to PlayerValues/enemyValues
        {
            //actual values stored as float/int based on offsets
            public float xMouse, yMouse, xPos, yPos, zPos;
            public int health;
        }
    }
}
