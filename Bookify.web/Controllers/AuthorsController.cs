using Microsoft.AspNetCore.Mvc;

namespace Bookify.web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.AsNoTracking().ToList();

            var viewModel = _mapper.Map<IEnumerable<CategoryVM>>(categories);

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnlyAttribute]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = _mapper.Map<Category>(model);

            _context.Add(category);
            _context.SaveChanges();

            var viewModel = _mapper.Map<CategoryVM>(category);


            return PartialView("_CategoryRow", viewModel);
        }

        [HttpGet]
        [AjaxOnlyAttribute]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            var model = _mapper.Map<CategoryFormVM>(category);

            return PartialView("_Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = _context.Categories.Find(model.Id);

            if (category is null)
                return NotFound();

            category = _mapper.Map(model, category);
            category.LastUpdatedDate = DateTime.Now;

            _context.Update(category);
            _context.SaveChanges();

            var viewModel = _mapper.Map<CategoryVM>(category);

            return PartialView("_CategoryRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggelStatus(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedDate = DateTime.Now;

            _context.SaveChanges();

            return Ok(category.LastUpdatedDate.ToString());
        }

        public IActionResult AllowItem(CategoryFormVM categoryFormVM)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == categoryFormVM.Name);

            var isAllow = category is null || category.Id.Equals(categoryFormVM.Id);

            return Json(isAllow);
        }
    }
}
