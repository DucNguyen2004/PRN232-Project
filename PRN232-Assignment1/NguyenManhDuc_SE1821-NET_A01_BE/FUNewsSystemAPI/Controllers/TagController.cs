using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FUNewsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController()
        {
            _tagService = new TagService();
        }

        // GET: api/category
        [HttpGet]
        public ActionResult<List<Tag>> GetAllTags()
        {
            return _tagService.GetAllTags();
        }
    }
}
