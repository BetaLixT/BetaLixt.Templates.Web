namespace BetaLixT.Templates.Web.Standard.Data.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool IsDone { get; set;}
    }
}