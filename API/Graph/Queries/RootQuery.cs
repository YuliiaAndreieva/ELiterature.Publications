using Data.Entities;
using Data.Repositories.Interfaces;

namespace API.Graph.Queries;

public class RootQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [GraphQLName("authors")]
    public IQueryable<Author> GetAllAuthors([Service]IAuthorsRepository repository) => repository.GetAllAsync();
    
    [UseProjection]
    [GraphQLName("author")]
    public IQueryable<Author> GetAuthor([Service]IAuthorsRepository repository, long id) => repository.GetByIdAsyncAsQueryable(id);

    [UseProjection]
    [GraphQLName("occupations")]
    public IQueryable<Occupation> GetOccupations([Service]IOccupationRepository repository) => 
        repository.GetAllAsync();

    [UseProjection]
    [GraphQLName("literatureDirections")]
    public IQueryable<LiteratureDirection> GetLiteratureDirections([Service]ILiteratureDirectionRepository repository) => 
        repository.GetAllAsync();
} 