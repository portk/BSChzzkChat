namespace BSChzzkChat.Structs
{
    struct AccessTokenContent
    {
        public string accessToken { get; set; }
        public TemporaryRestrict temporaryRestrict { get; set; }
        public bool realNameAuth { get; set; }
        public string extraToken { get; set; }
    }
}
