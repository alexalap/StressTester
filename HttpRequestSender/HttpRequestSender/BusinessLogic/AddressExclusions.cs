namespace HttpRequestSender.BusinessLogic
{
    /// <summary>
    /// Excludes specific urls from the address analyzation.
    /// 
    /// These special sites usually provided by some sort of third-party or are there for maintenance
    /// or any other kind of reasons that are not meant for the common public to view.
    /// </summary>
    public static class AddressExclusions
    {
        public static readonly string[] UrlExclusions = new string[]
        {
            "cdn-cgi",
            "mailto",
        };
    }
}
