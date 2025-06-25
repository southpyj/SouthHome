namespace SouthHome.Backend.Common.Http
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }  // 响应数据

        public int Status { get; set; } = 200;  // HTTP 状态码，默认 200

        public bool IsSuccess { get; set; } = true;  // 是否成功，默认 true

        public string Message { get; set; } = string.Empty;  // 响应消息，默认空字符串

        /// <summary>
        /// Creates a successful service response containing the specified data.
        /// </summary>
        /// <param name="data">The data to include in the service response.</param>
        /// <returns>A <see cref="ServiceResponse{T}"/> instance with the specified data and a successful status.</returns>
        public static ServiceResponse<T> Success(T data)
        {
            ServiceResponse<T> response = new();
            response.Data = data;
            return response;
        }

        /// <summary>
        /// Creates a failed service response with no data.
        /// </summary>
        /// <param name="message">failed message</param>
        /// <returns></returns>
        public static ServiceResponse<T> Error(string message)
        {
            ServiceResponse<T> response = new()
            {
                IsSuccess = false,
                Message = message,
                Status = 500
            };

            return response;
        }

        /// <summary>
        /// Creates a failed service response with the specified error message and status code.
        /// </summary>
        /// <param name="massage">The error message describing the failure.</param>
        /// <param name="status">The status code representing the error condition.</param>
        /// <returns>A <see cref="ServiceResponse{T}"/> instance indicating failure, with the provided message and status code.</returns>
        public static ServiceResponse<T> Error(string massage, int status)
        {
            ServiceResponse<T> response = new()
            {
                IsSuccess = false,
                Message = massage,
                Status = status
            };

            return response;
        }
    }
}
