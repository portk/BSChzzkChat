namespace BSChzzkChat.Structs
{
    struct ConnectBdyObj
    {
        public string uid { get; set; }
        public int devType { get; set; }
        public string accTkn { get; set; }
        public string auth { get; set; }

        public ConnectBdyObj(string uid, int devType, string accTkn)
        {
            this.uid = uid;
            this.devType = devType;
            this.accTkn = accTkn;
            this.auth = (uid != "") ? "SEND" : "READ";
        }
    }
}
