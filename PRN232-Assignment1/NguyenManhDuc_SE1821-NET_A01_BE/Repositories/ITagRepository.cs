using BusinessObjects;

namespace Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag? GetTagById(int id);
    }
}
