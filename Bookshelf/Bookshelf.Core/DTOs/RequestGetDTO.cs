namespace Bookshelf.Core.Models
{
    public class RequestGetDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public List<string> Categories { get; set; } = null!;
    }
}
