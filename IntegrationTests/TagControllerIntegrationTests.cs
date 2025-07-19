using System.Net;
using System.Net.Http.Json;
using Core.Dtos;
using IntegrationTests.Configuration;
using IntegrationTests.Fakers;

namespace IntegrationTests;

[Collection("PublicationApi")]
[Trait("Category", "Integration")]
[Trait("Controller", "Tag")]
public class TagControllerIntegrationTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;

    public TagControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _factory.ResetDatabaseAsync();

    [Fact(DisplayName = "POST /api/tag creates tag (200)")]
    public async Task CreateTag_ReturnsOk()
    {
        // Arrange
        var dto = EntityFakers.TagCreateDtoFaker.Generate();
        
        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync(TagEndpoints.Create, dto);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<TagCreateDto>();
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
    }

    [Fact(DisplayName = "POST /api/tag creates tag with faker data")]
    public async Task CreateTag_WithFakerData_ReturnsOk()
    {
        // Arrange
        var dto = EntityFakers.TagCreateDtoFaker.Generate();
        
        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync(TagEndpoints.Create, dto);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<TagCreateDto>();
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
    }
} 