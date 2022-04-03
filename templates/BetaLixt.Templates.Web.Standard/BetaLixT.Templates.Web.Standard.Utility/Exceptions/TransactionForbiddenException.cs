namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class TransactionForbiddenException : BaseException
    { 
        public TransactionForbiddenException(int code, string message, string? details = null)
		: base(code, message, details)
        {} 
    }
}
