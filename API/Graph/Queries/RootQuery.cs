using Data.Entities;
using Data.Repositories.Interfaces;
using TagEntity = Data.Entities.Tag;

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
    
    [UseProjection]
    [GraphQLName("literatureDirection")]
    public IQueryable<LiteratureDirection> GetDirection([Service]ILiteratureDirectionRepository repository, long id) => 
        repository.GetByIdAsyncAsQueryable(id);
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [GraphQLName("publications")]
    public IQueryable<Publication> GetPublications([Service]IPublicationRepository repository) => 
        repository.GetAllAsync();
    
    [UseProjection]
    [GraphQLName("publication")]
    public IQueryable<Publication> GetPublication([Service]IPublicationRepository repository, long id) => 
        repository.GetByIdAsyncAsQueryable(id);
    
    [UseProjection]
    [GraphQLName("tags")]
    public IQueryable<TagEntity> GetTags([Service]ITagRepository repository) => 
        repository.GetAllAsync();
    
} 