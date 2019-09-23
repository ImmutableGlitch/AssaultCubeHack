namespace projAssaultCubeAimbot
{
    class Player
    {
        public int baseAddr, xMouse, yMouse, xPos, yPos, zPos, health;
        public int[] multi;
        public int pointerAddress;

        public Player(int baseAddr, int xMouse, int yMouse, int xPos, int yPos, int zPos, int health)
        {
            this.baseAddr = baseAddr;
            this.xMouse = xMouse;
            this.yMouse = yMouse;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.health = health;
        }

        public int Health_Value { get; set; }

        public float PosX_Value { get; set; }

        public float PosY_Value { get; set; }

        public float PosZ_Value { get; set; }
    }
}
