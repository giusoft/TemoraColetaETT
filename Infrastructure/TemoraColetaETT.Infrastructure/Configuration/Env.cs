using DotNetEnv;

namespace TemoraColetaETT.Infrastructure.Configuration
{
    public static class Env
    {
        public static string ApiBaseUrl { get; private set; } = string.Empty;
        static Env()
        {
            string envPath = Path.Combine(AppContext.BaseDirectory, ".env");
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
                ApiBaseUrl = DotNetEnv.Env.GetString("API_BASE_URL");
            }
        }

        public static void Load()
        {
            DotNetEnv.Env.Load();
            ApiBaseUrl = DotNetEnv.Env.GetString("API_BASE_URL");
        }

        public static string? Get(string key) => Environment.GetEnvironmentVariable(key);
    }
}