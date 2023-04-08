namespace WebBookShopAPI.Data.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "You are not authorized dude",
                403 => "You are not allowed here, sry :с",
                404 => "Resource not found",
                500 => "Server error. Errors are the path to the dark side. Errors lead to anger. Anger lead to hate. Hates lead to career change",
                _ => null
            };
        }
    }
}
