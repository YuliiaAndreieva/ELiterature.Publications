namespace IntegrationTests.Configuration;

public static class PublicationEndpoints
{
    public const string Create = "/api/publication/create";
    public const string Update = "/api/publication/{0}/update";
    public const string GetAll = "/api/publication";
    public const string GetById = "/api/publication/{0}";
}

public static class AuthorsEndpoints
{
    public const string GetAll = "/api/authors";
    public const string Create = "/api/authors";
    public const string Update = "/api/authors/{0}";
    public const string Delete = "/api/authors/{0}";
}

public static class TagEndpoints
{
    public const string Create = "/api/tag";
}

public static class LiteratureDirectionEndpoints
{
    public const string Create = "/api/literaturedirection";
} 