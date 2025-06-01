using Core.Dtos;
using Core.Dtos.Moodboard;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Entities.Enums;
using Data.Repositories.Interfaces;

namespace Core.Services;

public class PublicationService : IPublicationService
{
    private readonly ILiteratureDirectionRepository _directionRepository;
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAuthorPhotoRepository _photoRepository;
    private readonly ITagRepository _tagRepository;

    public PublicationService(
        ILiteratureDirectionRepository directionRepository,
        IPublicationRepository publicationRepository,
        IAuthorPhotoRepository photoRepository,
        IAuthorsRepository authorsRepository,
        ITagRepository tagRepository)
    {
        _directionRepository = directionRepository; 
        _publicationRepository = publicationRepository;
        _photoRepository = photoRepository;
        _authorsRepository = authorsRepository;
        _tagRepository = tagRepository;
    }

    public IEnumerable<Publication> GetAllPublicationsAsync()
    {
        return _publicationRepository.GetAllAsync().ToList();
    }

    public async Task<UpdatePublicationDto?> UpdateAsync(long id, UpdatePublicationDto dto)
    {
        var publication = await _publicationRepository.GetByIdAsync(id);
        if (publication == null) return null;

        publication.Title = dto.Title;
        publication.Description = dto.Description;
        publication.PublicationYear = dto.PublicationYear;
        publication.Type = dto.Type;
        publication.Text = dto.Text;
        
        // Authors
        var currentAuthorIds = publication.Authors.Select(a => a.Id).ToList();
        var newAuthorIds = dto.AuthorIds;
        var authorsToRemove = publication.Authors.Where(a => !newAuthorIds.Contains(a.Id)).ToList();
        foreach (var author in authorsToRemove)
            publication.Authors.Remove(author);
        var authorsToAddIds = newAuthorIds.Except(currentAuthorIds).ToList();
        if (authorsToAddIds.Any())
        {
            var authorsToAdd = await _authorsRepository.GetAuthorsByIdsAsync(authorsToAddIds);
            foreach (var author in authorsToAdd)
                publication.Authors.Add(author);
        }

// Directions
        var currentDirIds = publication.LiteratureDirection.Select(d => d.Id).ToList();
        var newDirIds = dto.DirectionIds.Select(Convert.ToInt64).ToList();
        var dirsToRemove = publication.LiteratureDirection.Where(d => !newDirIds.Contains(d.Id)).ToList();
        foreach (var dir in dirsToRemove)
            publication.LiteratureDirection.Remove(dir);
        var dirsToAddIds = newDirIds.Except(currentDirIds).ToList();
        if (dirsToAddIds.Any())
        {
            var dirsToAdd = await _directionRepository.GetDirectionsByIdsAsync(dirsToAddIds);
            foreach (var dir in dirsToAdd)
                publication.LiteratureDirection.Add(dir);
        }

// Tags
        var currentTagIds = publication.Tags.Select(t => t.Id).ToList();
        var newTagIds = dto.TagIds.Select(Convert.ToInt64).ToList();
        var tagsToRemove = publication.Tags.Where(t => !newTagIds.Contains(t.Id)).ToList();
        foreach (var tag in tagsToRemove)
            publication.Tags.Remove(tag);
        var tagsToAddIds = newTagIds.Except(currentTagIds).ToList();
        if (tagsToAddIds.Any())
        {
            var tagsToAdd = await _tagRepository.GetTagsByIdsAsync(tagsToAddIds);
            foreach (var tag in tagsToAdd)
                publication.Tags.Add(tag);
        }


        if (dto.Photos != null)
        {
            var existingPhotos = await _photoRepository.GetByPublicationIdAsync(publication.Id);

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
                    var newPhoto = new PublicationPhoto
                    {
                        PhotoUrl = photoDto.PhotoUrl,
                        Type = photoDto.Type,
                        PublicationId = publication.Id
                    };
                    await _photoRepository.AddAsync(newPhoto);
                }
            }
        }

        await _publicationRepository.UpdateAsync(publication);
        return dto;
    }

    public async Task<CreatePublicationDto> CreateAsync(CreatePublicationDto dto)
    {
        var publication = new Publication
        {
            Title = dto.Title,
            Description = dto.Description,
            PublicationYear = dto.PublicationYear,
            Type = dto.Type,
            Text = dto.Text,
            Authors = await _authorsRepository.GetAuthorsByIdsAsync(dto.AuthorIds),
            LiteratureDirection = await _directionRepository.GetDirectionsByIdsAsync(dto.DirectionIds),
            Tags = await _tagRepository.GetTagsByIdsAsync(dto.TagIds)
        };

        await _publicationRepository.CreateAsync(publication);
        
        if (dto.Photos != null)
        {
            foreach (var photoDto in dto.Photos)
            {
                var publicationPhoto = new PublicationPhoto
                {
                    PhotoUrl = photoDto.PhotoUrl,
                    Type = photoDto.Type,
                    PublicationId = publication.Id
                };
                await _photoRepository.AddAsync(publicationPhoto);
            }
        }

        return dto;
    }
    
    public async Task<IEnumerable<PublicationMoodboardDto>> GetRandomPublicationsForMoodboardAsync(int count)
    {
        var publications = await _publicationRepository.GetRandomPublicationsWithImagesAsync(count);
        
        return publications.Select(p => new PublicationMoodboardDto
        {
            Id = p.Id,
            Title = p.Title,
            ImageUrl = p.Photos.FirstOrDefault(ph => ph.Type == PhotoType.AssociatedPhoto)?.PhotoUrl ?? string.Empty,
        });
    }
}