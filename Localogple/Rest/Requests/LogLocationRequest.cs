namespace Localogple.Rest.Requests
{
    public class LogLocationRequest
    {
        public string[] Dates { get; set; }
        public string[] Latitudes { get; set; }
        public string[] Longitudes { get; set; }
        public string UserId { get; set; }
        public string Langugage { get; set; }
    }
}