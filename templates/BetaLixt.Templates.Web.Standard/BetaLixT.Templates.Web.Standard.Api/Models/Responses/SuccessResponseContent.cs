namespace BetaLixT.Templates.Web.Standard.Api.Models.Responses
{
    public class SuccessResponseContent<T>
    {
        public SuccessResponseContent(T data)
        {
            this.ResultData = data;
        }
        public string StatusMessage { get => ResponseContentStatusMessages.Success; }
        public T ResultData { get; set; }
    }

    public class SuccessResponseContent
    {
        public string StatusMessage { get => ResponseContentStatusMessages.Success; }
    }
}
