namespace BSChzzkChat.Structs
{
    struct MyInfoContent
    {
        public bool hasProfile { get; set; }
        public string userIdHash { get; set; }
        public string nickname { get; set; }
        public string profileImageUrl { get; set; }
        public string[] penalties { get; set; }
        public bool officialNotiAgree { get; set; }
        public string officialNotiAgreeUpdatedDate { get; set; }
        public bool verifiedMark { get; set; }
        public bool loggedIn { get; set; }
    }
}
