namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class FailedResponseContent
    {
        public string? StatusMessage { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorDetails { get; set; }
        public string? TransactionId { get; set; }
    }
}
