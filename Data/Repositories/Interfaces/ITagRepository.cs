namespace Data.Repositories.Interfaces;
using TagEntity = Data.Entities.Tag;

public interface ITagRepository
{
    IQueryable<TagEntity> GetAllAsync();
    Task<TagEntity> CreateAsync(TagEntity tag);

    Task<List<TagEntity>> GetTagsByIdsAsync(
        List<long> ids);
}