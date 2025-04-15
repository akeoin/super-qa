using System.Threading.Tasks;
using AkeoIN.SuperQA.Models.TokenAuth;
using AkeoIN.SuperQA.Web.Controllers;
using Shouldly;
using Xunit;

namespace AkeoIN.SuperQA.Web.Tests.Controllers
{
    public class HomeController_Tests: SuperQAWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}