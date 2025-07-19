using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Configuration;
using Core.Dtos.Author;
using Core.Services;
using IntegrationTests.Fakers;

namespace IntegrationTests;

[Collection("PublicationApi")]
[Trait("Category", "Integration")]
[Trait("Controller", "Author")]
public class AuthorsControllerIntegrationTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;

    public AuthorsControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _factory.ResetDatabaseAsync();

    [Fact(DisplayName = "POST /api/authors creates author (201)")]
    public async Task CreateAuthor_ReturnsCreated()
    {
        // Arrange
        var dto = EntityFakers.AuthorCreateDtoFaker.Generate();
        
        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync(AuthorsEndpoints.Create, dto);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthorCreateDto>();
        Assert.NotNull(result);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.Equal(dto.LastName, result.LastName);
    }

    [Fact(DisplayName = "GET /api/authors returns collection of authors")]
    public async Task GetAllAuthors_ReturnsCollection()
    {
        // Arrange: create several authors
        for (int i = 0; i < 3; i++)
        {
            var dto = EntityFakers.AuthorCreateDtoFaker.Generate();
            var response = await _factory.HttpClient.PostAsJsonAsync(AuthorsEndpoints.Create, dto);
            response.EnsureSuccessStatusCode();
        }

        // Act
        var responseAll = await _factory.HttpClient.GetAsync(AuthorsEndpoints.GetAll);
        responseAll.EnsureSuccessStatusCode();
        var authors = await responseAll.Content.ReadFromJsonAsync<List<AuthorSelectDto>>();

        // Assert
        Assert.NotNull(authors);
        Assert.True(authors.Count >= 3);
        Assert.All(authors, a => Assert.False(string.IsNullOrWhiteSpace(a.FirstName)));
    }

    [Fact(DisplayName = "PUT /api/authors/{id} updates author (200)")]
    public async Task UpdateAuthor_ReturnsOk()
    {
        // Arrange: create author
        var createDto = EntityFakers.AuthorCreateDtoFaker.Generate();
        var createResponse = await _factory.HttpClient.PostAsJsonAsync(AuthorsEndpoints.Create, createDto);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<AuthorCreateDto>();
        Assert.NotNull(created);

        var updateDto = EntityFakers.AuthorUpdateDtoFaker.Generate();
        var updateUrl = string.Format(AuthorsEndpoints.Update, created.Id);
        
        // Act: update author
        var updateResponse = await _factory.HttpClient.PutAsJsonAsync(updateUrl, updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var updated = await updateResponse.Content.ReadFromJsonAsync<AuthorUpdateDto>();
        Assert.NotNull(updated);
        Assert.Equal(updateDto.FirstName, updated.FirstName);
    }

    [Fact(DisplayName = "PUT /api/authors/{id} returns 404 for non-existent id")]
    public async Task UpdateAuthor_ReturnsNotFound_WhenNotFound()
    {
        // Arrange
        var updateDto = EntityFakers.AuthorUpdateDtoFaker.Generate();
        var updateUrl = string.Format(AuthorsEndpoints.Update, 9999);
        
        // Act
        var response = await _factory.HttpClient.PutAsJsonAsync(updateUrl, updateDto);
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact(DisplayName = "DELETE /api/authors/{id} deletes author (204)")]
    public async Task DeleteAuthor_ReturnsNoContent()
    {
        // Arrange: create author
        var createDto = EntityFakers.AuthorCreateDtoFaker.Generate();
        var createResponse = await _factory.HttpClient.PostAsJsonAsync(AuthorsEndpoints.Create, createDto);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<AuthorCreateDto>();
        Assert.NotNull(created);

        // Act: delete author
        var deleteUrl = string.Format(AuthorsEndpoints.Delete, created.Id);
        var deleteResponse = await _factory.HttpClient.DeleteAsync(deleteUrl);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact(DisplayName = "DELETE /api/authors/{id} returns 404 for non-existent id")]
    public async Task DeleteAuthor_ReturnsNotFound_WhenNotFound()
    {
        // Arrange
        var nonExistentId = 9999;
        var deleteUrl = string.Format(AuthorsEndpoints.Delete, nonExistentId);
        
        // Act
        var response = await _factory.HttpClient.DeleteAsync(deleteUrl);
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
} 