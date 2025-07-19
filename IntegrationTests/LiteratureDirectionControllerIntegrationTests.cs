using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Configuration;
using Core.Dtos.LiteratureDirection;
using IntegrationTests.Fakers;

namespace IntegrationTests;

[Collection("PublicationApi")]
[Trait("Category", "Integration")]
[Trait("Controller", "LiteratureDirection")]
public class LiteratureDirectionControllerIntegrationTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;

    public LiteratureDirectionControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _factory.ResetDatabaseAsync();

    [Fact(DisplayName = "POST /api/literaturedirection creates direction (200)")]
    public async Task CreateLiteratureDirection_ReturnsOk()
    {
        // Arrange
        var dto = EntityFakers.LiteratureDirectionCreateDtoFaker.Generate();
        
        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync(LiteratureDirectionEndpoints.Create, dto);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<LiteratureDirectionCreateDto>();
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(dto.Description, result.Description);
        Assert.Equal(dto.StartCentury, result.StartCentury);
        Assert.Equal(dto.EndCentury, result.EndCentury);
    }

    [Fact(DisplayName = "POST /api/literaturedirection creates multiple directions")]
    public async Task CreateMultipleDirections_AllReturnOk()
    {
        // Arrange & Act: create multiple directions
        for (int i = 0; i < 3; i++)
        {
            var dto = EntityFakers.LiteratureDirectionCreateDtoFaker.Generate();
            var response = await _factory.HttpClient.PostAsJsonAsync(LiteratureDirectionEndpoints.Create, dto);
            
            // Assert: each creation should succeed
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<LiteratureDirectionCreateDto>();
            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
        }
    }
} 