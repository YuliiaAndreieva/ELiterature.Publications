using API.Graph.Filters;
using Data.Entities;
using Data.Repositories;

namespace API.Graph.Queries;

public class AuthorQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [GraphQLName("authors")]
    public IQueryable<Author> GetAllAuthors([Service]IAuthorsRepository repository) => repository.GetAllAsync();
    
    [UseProjection]
    [GraphQLName("getAuthor")]
    public IQueryable<Author> GetAuthor([Service]IAuthorsRepository repository, long id) => repository.GetAuthorByIdAsync(id);
}