namespace TodoREST
{
    public static class Constants
    {
        public static string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost";
        public static string Scheme = "https";
        public static string Port = "7141";
        public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}/Stocks/status";
    }
}
