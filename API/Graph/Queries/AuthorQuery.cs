using Data.Context;
using Data.Entities;

namespace API.Graph.Queries;

public class AuthorQuery
{
    [UseProjection]
    [GraphQLName("authors")]
    public IQueryable<Author> GetAuthors([Service]ELiteratureDbContext dbContext) => dbContext.Authors!;
    
    [GraphQLName("getAuthor")]
    public Author GetAuthor([Service]ELiteratureDbContext dbContext, long id)
    {
        return dbContext.Authors.FirstOrDefault(w => w.Id == id)!;
    }
}