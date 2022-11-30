namespace Bookify.web.Core.ViewModels
{
    public class CategoryFormVM
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = Errors.MaxLength), MinLength(5, ErrorMessage = Errors.MinLength)]
        [Remote("AllowItem", null, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
        public string Name { get; set; } = null!;
    }
}
