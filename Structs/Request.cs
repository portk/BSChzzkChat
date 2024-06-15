namespace BSChzzkChat.Structs
{
    struct Request
    {
        public string SongCode;
        public string SongName;

        public Request(string songCode, string songName)
        {
            SongCode = songCode;
            SongName = songName;
        }

        override
        public string ToString()
        {
            return $@"{SongCode} {this.SongName}";
        }
    }
}
