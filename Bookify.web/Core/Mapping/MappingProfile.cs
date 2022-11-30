namespace Bookify.web.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Category
            CreateMap<Category, CategoryVM>();
            CreateMap<Category, CategoryFormVM>().ReverseMap();

            //Author
            CreateMap<Author, AuthorVM>();
            CreateMap<Author, AuthorFormVM>().ReverseMap();
        }
    }
}
