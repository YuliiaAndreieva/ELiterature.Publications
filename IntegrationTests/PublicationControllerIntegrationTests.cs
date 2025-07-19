using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Configuration;
using Core.Dtos;
using IntegrationTests.Fakers;

namespace IntegrationTests;

[Collection("PublicationApi")]
[Trait("Category", "Integration")]
[Trait("Controller", "Publication")]
public class PublicationControllerIntegrationTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;

    public PublicationControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _factory.ResetDatabaseAsync();

    [Fact(DisplayName = "POST /api/publication/create creates publication (201)")]
    public async Task CreatePublication_ReturnsCreated()
    {
        // Arrange
        var dto = EntityFakers.CreatePublicationDtoFaker.Generate();
        
        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync(PublicationEndpoints.Create, dto);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreatePublicationDto>();
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
    }

    [Fact(DisplayName = "PUT /api/publication/{id}/update updates publication (200)")]
    public async Task UpdatePublication_ReturnsOk()
    {
        // Arrange
        var createDto = EntityFakers.CreatePublicationDtoFaker.Generate();
        var createResponse = await _factory.HttpClient.PostAsJsonAsync(PublicationEndpoints.Create, createDto);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<CreatePublicationDto>();
        Assert.NotNull(created);
        
        var updateDto = EntityFakers.UpdatePublicationDtoFaker.Generate();
        var updateUrl = string.Format(PublicationEndpoints.Update, created.Id);
        
        // Act
        var updateResponse = await _factory.HttpClient.PutAsJsonAsync(updateUrl, updateDto);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var updated = await updateResponse.Content.ReadFromJsonAsync<UpdatePublicationDto>();
        Assert.NotNull(updated);
        Assert.Equal(updateDto.Title, updated.Title);
    }

    [Fact(DisplayName = "PUT /api/publication/{id}/update returns 400 for non-existent id")]
    public async Task UpdatePublication_ReturnsBadRequest_WhenNotFound()
    {
        // Arrange
        var updateDto = EntityFakers.UpdatePublicationDtoFaker.Generate();
        var updateUrl = string.Format(PublicationEndpoints.Update, 9999);
        
        // Act
        var response = await _factory.HttpClient.PutAsJsonAsync(updateUrl, updateDto);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        Assert.NotNull(error);
        Assert.Contains("not found", error["error"], StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "GET /api/publication returns collection of publications")]
    public async Task GetAllPublications_ReturnsCollection()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            var dto = EntityFakers.CreatePublicationDtoFaker.Generate();
            var response = await _factory.HttpClient.PostAsJsonAsync(PublicationEndpoints.Create, dto);
            response.EnsureSuccessStatusCode();
        }
        
        // Act
        var responseAll = await _factory.HttpClient.GetAsync(PublicationEndpoints.GetAll);
        responseAll.EnsureSuccessStatusCode();
        var publications = await responseAll.Content.ReadFromJsonAsync<List<CreatePublicationDto>>();
        
        // Assert
        Assert.NotNull(publications);
        Assert.True(publications.Count >= 3);
        Assert.Contains(publications, p => !string.IsNullOrWhiteSpace(p.Title));
        Assert.All(publications, p => Assert.False(string.IsNullOrWhiteSpace(p.Title)));
    }

    [Fact(DisplayName = "GET /api/publication/{id} returns 404 for non-existent id")]
    public async Task GetPublication_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 9999;
        
        // Act
        var response = await _factory.HttpClient.GetAsync(string.Format(PublicationEndpoints.GetById, nonExistentId));
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
} 