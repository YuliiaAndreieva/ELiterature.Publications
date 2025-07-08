using Core.Dtos.Author;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly ILiteratureDirectionRepository _directionRepository;
    private readonly IOccupationRepository _occupationRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAuthorPhotoRepository _photoRepository;

    public AuthorService(
        IAuthorsRepository authorsRepository,
        IOccupationRepository occupationRepository,
        ILiteratureDirectionRepository directionRepository,
        IPublicationRepository publicationRepository,
        IAuthorPhotoRepository photoRepository)
    {
        _authorsRepository = authorsRepository;
        _occupationRepository = occupationRepository;
        _directionRepository = directionRepository;
        _publicationRepository = publicationRepository;
        _photoRepository = photoRepository;
    }

    public async Task<AuthorUpdateDto?> UpdateAsync(long id, AuthorUpdateDto dto)
    {
        var author = await _authorsRepository.GetByIdAsync(id);
        if (author == null) return null;

        author.FirstName = dto.FirstName;
        author.LastName = dto.LastName;
        author.MiddleName = dto.MiddleName;
        author.DateOfBirth = dto.DateOfBirth;
        author.DateOfDeath = dto.DateOfDeath;
        author.Biography = dto.Biography;
        
        author.LiteratureDirection = await _directionRepository.GetDirectionsByIdsAsync(dto.DirectionIds);
        author.Occupations = await _occupationRepository.GetOccupationsByIdAsync(dto.OccupationIds);
        author.Publications = await _publicationRepository.GetPublicationsByIdsAsync(dto.PublicationIds);
        
        if (dto.Photos != null)
        {
            var existingPhotos = await _photoRepository.GetByAuthorIdAsync(author.Id);
            
            var dtoIds = dto.Photos.Select(p => p.Id).ToList();
            var photosToDelete = existingPhotos.Where(p => !dtoIds.Contains(p.Id)).ToList();
            foreach (var photo in photosToDelete)
            {
                await _photoRepository.DeleteAsync(photo.Id);
            }
            
            foreach (var photoDto in dto.Photos)
            {
                if (photoDto.Id == 0)
                {
                    var newPhoto = new AuthorPhoto
                    {
                        PhotoUrl = photoDto.PhotoUrl,
                        Type = photoDto.Type,
                        AuthorId = author.Id,
                        Quote = photoDto.Quote
                    };
                    await _photoRepository.AddAsync(newPhoto);
                }
            }
        }
        
        await _authorsRepository.UpdateAsync(author);
        return dto;
    }

    public async Task<AuthorCreateDto> CreateAsync(AuthorCreateDto dto)
    {
        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            DateOfBirth = dto.DateOfBirth,
            DateOfDeath = dto.DateOfDeath,
            Biography = dto.Biography,
            LiteratureDirection = await _directionRepository.GetDirectionsByIdsAsync(dto.DirectionIds),
            Occupations = await _occupationRepository.GetOccupationsByIdAsync(dto.OccupationIds),
            Publications = await _publicationRepository.GetPublicationsByIdsAsync(dto.PublicationIds)
        };

        await _authorsRepository.CreateAsync(author);
        
        foreach (var authorPhoto in dto.Photos.Select(photoDto => new AuthorPhoto
                 {
                     PhotoUrl = photoDto.PhotoUrl,
                     Id = photoDto.Id,
                     Type = photoDto.Type,
                     AuthorId = author.Id
                 }))
        {
            await _photoRepository.AddAsync(authorPhoto);
        }

        return dto;
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var author = await _authorsRepository.GetByIdAsync(id);
        if (author == null)
            return false;
        
        // await _photoRepository.DeleteByAuthorIdAsync(id);

        await _authorsRepository.DeleteAsync(author);
        return true;
    }

    public IEnumerable<AuthorSelectDto> GetAllForSelectAsync()
    {
        var authors =  _authorsRepository.GetAllAsync();
        return  authors.Select(a => new AuthorSelectDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            MiddleName = a.MiddleName
        });
    }
}

public class AuthorSelectDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
}