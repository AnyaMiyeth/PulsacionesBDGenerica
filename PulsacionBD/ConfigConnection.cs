



using System.Configuration;

namespace PulsacionBD
{
   public static class ConfigConnection
    {
        public static string connectionString= ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static string ProviderName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
    }
    
}
