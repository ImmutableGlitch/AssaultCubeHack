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

        public int Health { get; set; }
        public float MouseX { get; set; }
        public float MouseY { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
    }
}
