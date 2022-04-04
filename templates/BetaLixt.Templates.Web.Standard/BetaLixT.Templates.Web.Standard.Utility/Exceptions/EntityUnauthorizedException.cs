namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class TransactionUnauthorizedException : BaseException
    { 
        public TransactionUnauthorizedException(int code, string message, string? details = null)
		: base(code, message, details)
        {} 
    }
}
