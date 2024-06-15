namespace BSChzzkChat.Structs
{
    struct ConnectObj
    {
        public string ver { get; set; }
        public int cmd { get; set; }
        public string svcid { get; set; }
        public string cid { get; set; }
        public ConnectBdyObj bdy { get; set; }
        public long tid { get; set; }

        public ConnectObj(string ver, int cmd, string svcid, string cid, ConnectBdyObj bdy, int tid)
        {
            this.ver = ver;
            this.cmd = cmd;
            this.svcid = svcid;
            this.cid = cid;
            this.bdy = bdy;
            this.tid = tid;
        }
    }
}
