namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class BaseException : Exception
    {
        public readonly int ErrorCode = int.MinValue;
        public readonly string? ErrorDetails;

	public BaseException(int errorCode, string message, string? details = null)
		: base(message)
	{
		this.ErrorCode = errorCode;	
		this.ErrorDetails = details;
	}
    }
}
