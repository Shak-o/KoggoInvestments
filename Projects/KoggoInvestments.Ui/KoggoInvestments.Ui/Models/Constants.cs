namespace TodoREST
{
    public static class Constants
    {
        public static string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "44.207.32.191" : "localhost";
        public static string Scheme = "http";
        public static string Port = "5017";
        public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}/Stocks/status";
    }
  //  http://44.207.32.191:5017
}
