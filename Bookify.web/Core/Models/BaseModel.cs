namespace Bookify.web.Core.Models
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastUpdatedDate { get; set; }
    }
}
