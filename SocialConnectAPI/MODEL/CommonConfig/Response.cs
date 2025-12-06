namespace MODEL.CommonConfig
{
    public class Response
    {
        public string? Message { get; set; }
        public APIStatus Status { get; set; }
        public object? Data { get; set; }
    }
    public enum APIStatus
    {
        Successful = 0,
        Error = 1,
        SystemError = 2
    }
    public static class Messages
    {
        public const string Successfully = "Successfully";
        public const string UpdateSuccess = "Update Successfully";
        public const string DeleteSuccess = "Delete Successfully";
        public const string AddSuccess = "Add Successfully";
        public const string ErrorWhileFetchingData = "Error While Fetching Data";
        public const string InvalidPostedData = "Posted Invalid Data";
        public const string Result = "Result Found!";
        public const string UpdateFail = "Updated Fail";
        public const string GenerateTokenSuccess = "Generate Token Successfully";
        public const string GenerateTokenFail = "Generate Token Failed";
    }
}
