namespace BSChzzkChat.Structs
{
    struct LiveStatusContent
    {
        public string liveTitle { get; set; }
        public string status { get; set; }
        public long concurrentUserCount { get; set; }
        public long accumulateCount { get; set; }
        public bool paidPromotion { get; set; }
        public bool adult { get; set; }
        public string chatChannelId { get; set; }
        public string[] tags { get; set; }
        public string categoryType { get; set; }
        public string liveCategory { get; set; }
        public string liveCategoryValue { get; set; }
        public string livePollingStatusJson { get; set; }
        public string faultStatus { get; set; }
        public string userAdultStatus { get; set; }
        public bool chatActive { get; set; }
        public string chatAvailableGroup { get; set; }
        public string chatAvailableCondition { get; set; }
        public long minFollowerMinute { get; set; }
        public bool chatDonationRankingExposure { get; set; }
    }
}
