namespace BetaLixT.Templates.Web.Standard.Utility.Startup
{
    public interface IInitializer
    {
        bool IsRequired { get; }
        void Initialize();
    }
}