namespace WebBookShopAPI.Data.Errors
{
    public class ApiValidationErrorResponsecs : ApiResponse
    {
        public ApiValidationErrorResponsecs() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
