namespace Bookshelf.Core.DTOs.Requests
{
    public class RequestDetailsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string DateAdded { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public int Upvotes { get; set; }

        public string Link { get; set; } = null!;

        public string? Motivation { get; set; } = null!;

        public bool Followed { get; set; }

        public bool Upvoted { get; set; }

        public List<string> Categories { get; set; } = null!;
    }
}