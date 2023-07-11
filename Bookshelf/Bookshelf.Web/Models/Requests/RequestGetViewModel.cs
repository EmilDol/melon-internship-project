using Bookshelf.Core.DTOs.Requests;

namespace Bookshelf.Web.Models.Requests
{
    public class RequestGetViewModel
    {
        public List<RequestGetDTO> Requests { get; set; } = null!;
    }
}
