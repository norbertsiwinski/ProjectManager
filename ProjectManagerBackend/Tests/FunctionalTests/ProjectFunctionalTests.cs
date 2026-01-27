using System.Net;
using Common;

namespace ProjectManager.FunctionalTests;

public class ProjectFunctionalTests(TestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Get_Projects_returns_200()
    {
        var response = await Client.GetAsync("/api/projects");
        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
    }
}