namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class EntityMissingException : BaseException
    { 
        public EntityMissingException(int code, string message, string? details = null)
		: base(code, message, details)
        {} 
    }
}
