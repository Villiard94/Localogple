namespace Localogple.Rest.Requests
{
    public class LogLocationRequest
    {
        public long[] Dates { get; set; }
        public string[] Latitudes { get; set; }
        public string[] Longitudes { get; set; }
        public string UserId { get; set; }
        public string Language { get; set; }
        public string ActiveCompany { get; set; }
        public string Token { get; set; }
    }
}