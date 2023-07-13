using Bookshelf.Core.DTOs.Resources;

namespace Bookshelf.Web.Models.Resources
{
    public class ResourceAddViewModel
    { 
        public ResourceAddDTO? Resource { get; set; }

        public int Id { get; set; }

        public bool ComesFromRequest { get; set; } = false;
    }
}
