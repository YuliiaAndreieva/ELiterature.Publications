using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Graph.Resolvers;

public class AuthorResolvers
{
    public IQueryable<Publication> GetPublications([Parent] Author author, [Service] ELiteratureDbContext db)
    {
        return db.Publications.Include(p => p.Photos).Where(p => p.Authors.Any(a => a.Id == author.Id));
    }
}