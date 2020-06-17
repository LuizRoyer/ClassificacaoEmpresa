using ApiControleEmpresa;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace ApiControleEmpresaTests.Fixtures
{
    class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestContext()
        {
            SetupClient();
        }
        private void SetupClient()
        {
            _server = new TestServer(new Microsoft.AspNetCore.Hosting.WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
