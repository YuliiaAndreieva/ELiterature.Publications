using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AuthorPhotoRepository : IAuthorPhotoRepository
{
    private readonly ELiteratureDbContext _dbContext;
    public AuthorPhotoRepository(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthorPhoto> AddAsync(AuthorPhoto photo)
    {
        _dbContext.Photos.Add(photo);
        await _dbContext.SaveChangesAsync();
        return photo;
    }

    public async Task<List<AuthorPhoto>> GetByAuthorIdAsync(long authorId)
    {
        return await _dbContext.Photos
            .OfType<AuthorPhoto>()
            .Where(p => p.AuthorId == authorId)
            .ToListAsync();
    }

    public async Task DeleteAsync(long photoId)
    {
        var photo = await _dbContext.Photos.FindAsync(photoId);
        if (photo != null)
        {
            _dbContext.Photos.Remove(photo);
            await _dbContext.SaveChangesAsync();
        }
    }
}