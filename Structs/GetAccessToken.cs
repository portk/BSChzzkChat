namespace BSChzzkChat.Structs
{
    struct GetAccessToken
    {
        public int code { get; set; }
        public string message { get; set; }
        public AccessTokenContent content { get; set; }
        public bool realNameAuth { get; set; }
        public string extraToken { get; set; }
    }
}
