using Common;

namespace ProjectManager.FunctionalTests
{
    public class BaseFunctionalTest(TestWebAppFactory factory) 
        : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient Client = factory.CreateClient();
    }
}
