namespace BSChzzkChat.Structs
{
    struct PingPongObj
    {
        public string ver { get; set; }
        public int cmd { get; set; }

        public PingPongObj(string ver, int cmd)
        {
            this.ver = ver;
            this.cmd = cmd;
        }
    }
}
