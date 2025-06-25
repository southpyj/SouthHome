namespace SouthHome.Backend.Common
{
    public static class Util
    {
        public static IConfiguration Configuration { get; set; } // 配置文件

        private static long Twepoch = new DateTimeOffset(2025, 7, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds(); // 起始时间戳（2025-07-01 00:00:00）

        private const int WorkerIdBits = 5; // 机器标识位数
        private const int SequenceBits = 12; // 序列号位数

        private const long MaxSequence = -1L ^ -1L << SequenceBits; // 最大序列号

        private const int WorkerIdShift = SequenceBits;
        private const int TimestampShift = SequenceBits + WorkerIdBits;

        private static long _lastTimestamp = -1;
        private static long _sequence = 0;

        private static object _idLock = new();

        private static object _accountLock = new();

        private static int _accountCounter = 0;
        
        private const int MAX_COUNTER = 9999; // 每毫秒最大生成9999个值

        private const int BASE_VALUE = 100000;


        private const string config = "appsettings.Development.json";

        static Util()
        {
            _lastTimestamp = GetTimestamp();
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(config, optional: true, reloadOnChange: true)
                .Build();
        }
        /// <summary>
        /// 雪花算法生成Uid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static long GenerateUniqueGuid()
        {
            lock (_idLock)
            {
                long timestamp = GetTimestamp();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception("Invalid system clock");
                }
                if (timestamp == _lastTimestamp)
                {
                    _sequence = _sequence + 1 & MaxSequence;
                    if (_sequence == 0)
                    {
                        timestamp = WaitForNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                return timestamp - Twepoch << TimestampShift | GetWorkId() << WorkerIdShift | _sequence;
            }
        }

        /// <summary>
        /// 生成基于时间的唯一递增 账号
        /// </summary>
        public static string GenerateAccount()
        {
            // 获取当前秒级时间戳
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // 与起点时间戳的差值
            long offset = currentTimestamp - Twepoch;

            int sequence;

            // 线程安全处理
            lock (_accountLock)
            {
                sequence = _accountCounter++;
                if (_accountCounter > MAX_COUNTER)
                {
                    _accountCounter = 0;  // 序列号溢出归零
                }
            }

            // 账号 = 基准值 + 时间偏移量 + 自增序列号
            long accountNumber = BASE_VALUE + offset * 10000 + sequence;

            // 转为字符串，确保长度在 6-16 位之间
            string accountStr = accountNumber.ToString();

            if (accountStr.Length > 16)
            {
                return accountStr[..16];  // 超出16位时截取前16位
            }

            return accountStr;
        }

        public static string GetImageFolder()
        {
            string imageFolder = Path.Combine(Constants.PublicFolder,"image");
            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            return imageFolder;
        }

        public static string GetImageUrl()
        {
            string imageUrl = Path.Combine(Constants.PublicUrl, "image");
            return imageUrl;
        }

        private static long GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private static long GetWorkId()
        {
            long rooId = Convert.ToInt64(Configuration["RoomId"]);
            long machineId = Convert.ToInt64(Configuration["MachineId"]);
            return rooId << 3 | machineId;
        }

        private static long WaitForNextMillis(long lastTimestamp)
        {
            long timestamp = GetTimestamp();

            while (timestamp <= lastTimestamp)
            {
                timestamp = GetTimestamp();
            }

            return timestamp;
        }
    }
}
