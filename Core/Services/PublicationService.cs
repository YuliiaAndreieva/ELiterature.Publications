using Core.Common;
using Core.Dtos;
using Core.Dtos.Photo;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class PublicationService : IPublicationService
{
    private readonly ILiteratureDirectionRepository _directionRepository;
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAuthorPhotoRepository _photoRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<PublicationService> _logger;

    public PublicationService(
        ILiteratureDirectionRepository directionRepository,
        IPublicationRepository publicationRepository,
        IAuthorPhotoRepository photoRepository,
        IAuthorsRepository authorsRepository,
        ITagRepository tagRepository,
        ILogger<PublicationService> logger)
    {
        _directionRepository = directionRepository; 
        _publicationRepository = publicationRepository;
        _photoRepository = photoRepository;
        _authorsRepository = authorsRepository;
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public IEnumerable<Publication> GetAllPublicationsAsync()
    {
        try
        {
            return _publicationRepository.GetAllAsync().ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all publications");
            return Enumerable.Empty<Publication>();
        }
    }
    
    public async Task<Result<Publication>> GetPublicationByIdAsync(long id)
    {
        try
        {
            var publication = await _publicationRepository.GetByIdAsync(id);
            return Result<Publication>.Success(publication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting publication");
            return Result<Publication>.Failure("Can not get publication");
        }
    }

    /// <summary>
    /// Creates publication
    /// </summary>
    public async Task<Result<CreatePublicationDto>> CreatePublicationAsync(CreatePublicationDto dto)
    {
        try
        {
            if (dto is null)
                return Result<CreatePublicationDto>.Failure("DTO cannot be null");

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
            
            if (dto.Photos.Any())
            {
                var photosResult = await AddPhotosAsync(publication, dto.Photos);
                if (!photosResult.IsSuccess)
                    return Result<CreatePublicationDto>.Failure(photosResult.Error!);
            }

            dto.Id = publication.Id;

            return Result<CreatePublicationDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating publication");
            return Result<CreatePublicationDto>.Failure(ex);
        }
    }

    /// <summary>
    /// Updates publication
    /// </summary>
    public async Task<Result<UpdatePublicationDto>> UpdatePublicationAsync(long id, UpdatePublicationDto dto)
    {
        try
        {
            if (dto is null)
                return Result<UpdatePublicationDto>.Failure("DTO cannot be null");
                
            var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
                return Result<UpdatePublicationDto>.Failure($"Publication with id {id} not found");
                
            var updateResult = await UpdatePublicationFieldsAsync(publication, dto);
            if (!updateResult.IsSuccess)
                return Result<UpdatePublicationDto>.Failure(updateResult.Error!);

            var authorsResult = await UpdateAuthorsAsync(publication, dto.AuthorIds);
            if (!authorsResult.IsSuccess)
                return Result<UpdatePublicationDto>.Failure(authorsResult.Error!);

            var directionsResult = await UpdateDirectionsAsync(publication, dto.DirectionIds);
            if (!directionsResult.IsSuccess)
                return Result<UpdatePublicationDto>.Failure(directionsResult.Error!);

            var tagsResult = await UpdateTagsAsync(publication, dto.TagIds);
            if (!tagsResult.IsSuccess)
                return Result<UpdatePublicationDto>.Failure(tagsResult.Error!);

            if (dto.Photos.Any())
            {
                var photosResult = await UpdatePhotosAsync(publication, dto.Photos);
                if (!photosResult.IsSuccess)
                    return Result<UpdatePublicationDto>.Failure(photosResult.Error!);
            }

            await _publicationRepository.UpdateAsync(publication);
            return Result<UpdatePublicationDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating publication with id {PublicationId}", id);
            return Result<UpdatePublicationDto>.Failure(ex);
        }
    }

    /// <summary>
    /// Updates the main fields of the publication entity from the DTO
    /// </summary>
    private Task<Result> UpdatePublicationFieldsAsync(Publication publication, UpdatePublicationDto dto)
    {
        try
        {
            if (publication == null || dto == null)
                return Task.FromResult(Result.Failure("Publication or DTO cannot be null"));

            publication.Title = dto.Title;
            publication.Description = dto.Description;
            publication.PublicationYear = dto.PublicationYear;
            publication.Type = dto.Type;
            publication.Text = dto.Text;

            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating publication fields");
            return Task.FromResult(Result.Failure(ex));
        }
    }

    /// <summary>
    /// Updates the authors collection of the publication
    /// </summary>
    private async Task<Result> UpdateAuthorsAsync(Publication publication, List<long> newAuthorIds)
    {
        try
        {
            if (publication == null || newAuthorIds == null)
                return Result.Failure("Publication or author IDs cannot be null");

            var currentAuthorIds = publication.Authors.Select(a => a.Id).ToHashSet();
            var newAuthorIdsSet = newAuthorIds.ToHashSet();

            // Remove authors not in new list
            foreach (var author in publication.Authors.Where(a => !newAuthorIdsSet.Contains(a.Id)).ToList())
                publication.Authors.Remove(author);

            // Add new authors
            var authorsToAddIds = newAuthorIdsSet.Except(currentAuthorIds).ToList();
            if (authorsToAddIds.Count > 0)
            {
                var authorsToAdd = await _authorsRepository.GetAuthorsByIdsAsync(authorsToAddIds);
                foreach (var author in authorsToAdd)
                    publication.Authors.Add(author);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating authors");
            return Result.Failure(ex);
        }
    }

    /// <summary>
    /// Updates the literature directions collection of the publication
    /// </summary>
    private async Task<Result> UpdateDirectionsAsync(Publication publication, List<long> newDirectionIds)
    {
        try
        {
            if (publication == null || newDirectionIds == null)
                return Result.Failure("Publication or direction IDs cannot be null");

            var currentDirIds = publication.LiteratureDirection.Select(d => d.Id).ToHashSet();
            var newDirIds = newDirectionIds.Select(Convert.ToInt64).ToHashSet();

            foreach (var dir in publication.LiteratureDirection.Where(d => !newDirIds.Contains(d.Id)).ToList())
                publication.LiteratureDirection.Remove(dir);

            var dirsToAddIds = newDirIds.Except(currentDirIds).ToList();
            if (dirsToAddIds.Count > 0)
            {
                var dirsToAdd = await _directionRepository.GetDirectionsByIdsAsync(dirsToAddIds);
                foreach (var dir in dirsToAdd)
                    publication.LiteratureDirection.Add(dir);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating directions");
            return Result.Failure(ex);
        }
    }

    /// <summary>
    /// Updates the tags collection of the publication
    /// </summary>
    private async Task<Result> UpdateTagsAsync(Publication publication, List<long> newTagIds)
    {
        try
        {
            if (publication == null || newTagIds == null)
                return Result.Failure("Publication or tag IDs cannot be null");

            var currentTagIds = publication.Tags.Select(t => t.Id).ToHashSet();
            var newIds = newTagIds.Select(Convert.ToInt64).ToHashSet();

            foreach (var tag in publication.Tags.Where(t => !newIds.Contains(t.Id)).ToList())
                publication.Tags.Remove(tag);

            var tagsToAddIds = newIds.Except(currentTagIds).ToList();
            if (tagsToAddIds.Count > 0)
            {
                var tagsToAdd = await _tagRepository.GetTagsByIdsAsync(tagsToAddIds);
                foreach (var tag in tagsToAdd)
                    publication.Tags.Add(tag);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating tags");
            return Result.Failure(ex);
        }
    }

    /// <summary>
    /// Updates the photos collection of the publication
    /// </summary>
    private async Task<Result> UpdatePhotosAsync(Publication publication, List<PublicationPhotoDto> photos)
    {
        try
        {
            if (publication == null || photos == null)
                return Result.Failure("Publication or photos cannot be null");

            var existingPhotos = await _photoRepository.GetByPublicationIdAsync(publication.Id);
            var dtoIds = photos.Select(p => p.Id).ToHashSet();

            // Delete photos not in DTO
            foreach (var photo in existingPhotos)
            {
                if (!dtoIds.Contains(photo.Id))
                    await _photoRepository.DeleteAsync(photo.Id);
            }

            foreach (var photoDto in photos)
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

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating photos");
            return Result.Failure(ex);
        }
    }

    /// <summary>
    /// Adds photos to publication
    /// </summary>
    private async Task<Result> AddPhotosAsync(Publication publication, List<PublicationPhotoDto> photos)
    {
        try
        {
            if (publication == null || photos == null)
                return Result.Failure("Publication or photos cannot be null");

            foreach (var photoDto in photos)
            {
                var publicationPhoto = new PublicationPhoto
                {
                    PhotoUrl = photoDto.PhotoUrl,
                    Type = photoDto.Type,
                    PublicationId = publication.Id
                };
                await _photoRepository.AddAsync(publicationPhoto);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding photos");
            return Result.Failure(ex);
        }
    }
}