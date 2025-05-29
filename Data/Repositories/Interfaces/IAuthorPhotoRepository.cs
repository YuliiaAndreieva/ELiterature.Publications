using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IAuthorPhotoRepository
{
    Task<AuthorPhoto> AddAsync(AuthorPhoto photo);

    Task<List<AuthorPhoto>> GetByAuthorIdAsync(
        long authorId);

    Task DeleteAsync(
        long photoId);

    Task<List<PublicationPhoto>> GetByPublicationIdAsync(long pubId);

    Task<PublicationPhoto> AddAsync(PublicationPhoto photo);
}