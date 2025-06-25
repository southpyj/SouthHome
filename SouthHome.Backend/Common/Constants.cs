namespace SouthHome.Backend.Common
{
    /// <summary>
    /// 常量类
    /// </summary>
    public static class Constants
    {
        private static string _env = "";  // 当前使用的环境变量的值

        private static string _envFile = $"appsettings{_env}.json";  // 环境变量文件名

        private static readonly IConfigurationRoot _configuration;  // 配置根对象

        public static string SecretString;  // 密钥字符串

        public static string PublicFolder;  // 公共文件夹路径

        public static string PublicUrl;  // 公共URL

        static Constants()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(_envFile)
                .Build();
            SecretString = _configuration["SecretString"]!;
            PublicFolder = _configuration["PublicFolder"]!;
            PublicUrl = _configuration["PublicUrl"]!;
        }

        public static DateTime CurrentTimestamp
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
