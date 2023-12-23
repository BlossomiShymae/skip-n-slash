using BlossomiShymae.SkipNSlash.Core;

namespace BlossomiShymae.SkipNSlash.Tests;

public class CoreUnitTests
{
    public HttpClient HttpClient = new();

    [Fact]
    public async Task VersionsTest()
    {
        var versions = await Versions.GetAllAsync(HttpClient);

        Assert.True(versions.Count() > 10);
    }

    [Fact]
    public async Task FilesExportedTest()
    {
        var text = await FilesExported.GetAsync("latest", HttpClient);
        
        var results = text.Search("gwen");

        Assert.True(results.Count() > 1000);
    }
}