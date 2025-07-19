using IntegrationTests.Configuration;

namespace IntegrationTests;

[CollectionDefinition("PublicationApi")]
public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory> { }