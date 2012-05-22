using System.Configuration;

namespace tallyvu
{
    public static class Credentials
    {
        public static string AccountSid = ConfigurationManager.AppSettings["AccountSid"];
        public static string AuthToken = ConfigurationManager.AppSettings["AuthToken"];
    }
}