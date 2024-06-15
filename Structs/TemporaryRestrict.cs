namespace BSChzzkChat.Structs
{
    struct TemporaryRestrict
    {
        public bool temporaryRestrict { get; set; }
        public long times { get; set; }
        public long? duration { get; set; }
        public long? createdTime { get; set; }
    }
}
