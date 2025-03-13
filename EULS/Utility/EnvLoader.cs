using DotNetEnv;

namespace EULS.Utility
{
    public static class EnvLoader
    {
        public static void Load()
        {
            Env.Load();
        }

        public static string[] GetJadualUrls() 
        {
            return Environment.GetEnvironmentVariable("JADUAL_URLS")?.Split(',') ?? Array.Empty<string>();
        }
    }
}
