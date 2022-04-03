namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class TransactionCheckFailedException : BaseException
    { 
        public TransactionCheckFailedException(int code, string message, string? details = null)
		: base(code, message, details)
        {} 
    }
}
