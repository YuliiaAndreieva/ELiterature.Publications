using Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class RawSqlService
{
    private readonly IRawSqlRepository _rawSqlRepository;
    private readonly ILogger<RawSqlService> _logger;

    public RawSqlService(IRawSqlRepository rawSqlRepository, ILogger<RawSqlService> logger)
    {
        _rawSqlRepository = rawSqlRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<dynamic>> GetPublicationsByAuthorAsync(long authorId)
    {
        const string sql = @"
            SELECT p.Id, p.Title, p.Description, p.Type, p.Text, p.PublicationYear
            FROM Publications p
            INNER JOIN AuthorPublication ap ON p.Id = ap.PublicationsId
            WHERE ap.AuthorsId = @AuthorId
            ORDER BY p.PublicationYear DESC";

        _logger.LogInformation("Getting publications for author {AuthorId}", authorId);
        return await _rawSqlRepository.QueryAsync<dynamic>(sql, new { AuthorId = authorId });
    }

    public async Task<IEnumerable<dynamic>> GetPublicationsByDirectionAsync(long directionId)
    {
        const string sql = @"
            SELECT p.Id, p.Title, p.Description, p.Type, p.Text, p.PublicationYear
            FROM Publications p
            INNER JOIN LiteratureDirectionPublication ldp ON p.Id = ldp.PublicationsId
            WHERE ldp.LiteratureDirectionsId = @DirectionId
            ORDER BY p.PublicationYear DESC";

        _logger.LogInformation("Getting publications for direction {DirectionId}", directionId);
        try
        {
            var result = await _rawSqlRepository.QueryAsync<dynamic>(sql, new { DirectionId = directionId });
            var count = result.Count();
            _logger.LogInformation("Found {Count} publications for direction {DirectionId}", count, directionId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting publications for direction {DirectionId}. SQL: {Sql}", directionId, sql);
            throw;
        }
    }
    
    public async Task<IEnumerable<dynamic>> GetPublicationCountByTypeAsync()
    {
        const string sql = @"
            SELECT Type, COUNT(*) as Count
            FROM Publications
            GROUP BY Type
            ORDER BY Count DESC";

        _logger.LogInformation("Getting publication count grouped by type");
        return await _rawSqlRepository.GroupByAsync(sql);
    }
    
    public async Task<int> UpdatePublicationAsync(long publicationId, string title, string description, int type, string text, DateOnly? publicationYear)
    {
        const string sql = @"
            UPDATE Publications 
            SET Title = @Title, 
                Description = @Description, 
                Type = @Type, 
                Text = @Text, 
                PublicationYear = @PublicationYear
            WHERE Id = @Id";

        var parameters = new
        {
            Id = publicationId,
            Title = title,
            Description = description,
            Type = type,
            Text = text,
            PublicationYear = publicationYear
        };

        _logger.LogInformation("Updating publication for ID {PublicationId} with title '{Title}'", publicationId, title);
        return await _rawSqlRepository.ExecuteAsync(sql, parameters);
    }
} 