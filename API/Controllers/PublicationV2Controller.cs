using Microsoft.AspNetCore.Mvc;
using Core.Services;
using System.Net;
using Core.Dtos;

namespace API.Controllers;

[ApiController]
[Route("api/v2/publications")]
public class PublicationV2Controller : ControllerBase
{
    private readonly RawSqlService _rawSqlService;
    private readonly ILogger<PublicationV2Controller> _logger;

    public PublicationV2Controller(RawSqlService rawSqlService, ILogger<PublicationV2Controller> logger)
    {
        _rawSqlService = rawSqlService;
        _logger = logger;
    }

    /// <summary>
    /// Get publications with optional filtering by author, direction, or type
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<object>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPublications(
        [FromQuery] long? authorId = null,
        [FromQuery] long? directionId = null,
        [FromQuery] int? type = null)
    {
        try
        {
            if (authorId.HasValue)
            {
                _logger.LogInformation("Getting publications for author {AuthorId}", authorId);
                var publications = await _rawSqlService.GetPublicationsByAuthorAsync(authorId.Value);
                
                if (!publications.Any())
                {
                    _logger.LogWarning("No publications found for author {AuthorId}", authorId);
                    return NotFound($"No publications found for author {authorId}");
                }

                return Ok(publications);
            }
            
            if (directionId.HasValue)
            {
                _logger.LogInformation("Getting publications for direction {DirectionId}", directionId);
                var publications = await _rawSqlService.GetPublicationsByDirectionAsync(directionId.Value);
                
                if (!publications.Any())
                {
                    _logger.LogWarning("No publications found for direction {DirectionId}", directionId);
                    return NotFound($"No publications found for direction {directionId}");
                }

                return Ok(publications);
            }
            
            if (type.HasValue)
            {
                _logger.LogInformation("Getting publication count for type {Type}", type);
                var count = await _rawSqlService.GetPublicationCountBySpecificTypeAsync(type.Value);
                
                if (count == 0)
                {
                    _logger.LogWarning("No publications found for type {Type}", type);
                    return NotFound($"No publications found for type {type}");
                }

                return Ok(new { Type = type.Value, Count = count });
            }
            
            _logger.LogInformation("Getting publication count by type");
            var counts = await _rawSqlService.GetPublicationCountByTypeAsync();
            return Ok(counts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting publications. AuthorId: {AuthorId}, DirectionId: {DirectionId}, Type: {Type}", 
                authorId, directionId, type);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update entire publication
    /// </summary>
    [HttpPut("{publicationId}")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdatePublication(long publicationId, [FromBody] UpdatePublicationRequest request)
    {
        try
        {
            _logger.LogInformation("Updating publication for ID {PublicationId} with title '{Title}'", publicationId, request.Title);
            var affectedRows = await _rawSqlService.UpdatePublicationAsync(
                publicationId, 
                request.Title, 
                request.Description, 
                request.Type, 
                request.Text, 
                request.PublicationYear);
            
            if (affectedRows == 0)
            {
                _logger.LogWarning("No publication found with ID {PublicationId}", publicationId);
                return NotFound($"Publication {publicationId} not found");
            }

            return Ok(new { AffectedRows = affectedRows, Message = "Publication updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating publication for ID {PublicationId}", publicationId);
            return StatusCode(500, "Internal server error");
        }
    }
}