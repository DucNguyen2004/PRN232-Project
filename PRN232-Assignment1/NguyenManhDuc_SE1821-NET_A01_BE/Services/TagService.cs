using BusinessObjects;
using Repositories;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService()
        {
            _tagRepository = new TagRepository();
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }

        public Tag? GetTagById(int id)
        {
            return _tagRepository.GetTagById(id);
        }
    }
}
