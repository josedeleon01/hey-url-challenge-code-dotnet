using NUnit.Framework;
using Moq;
using hey_url_challenge_code_dotnet.Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using Microsoft.Extensions.Logging;
using HeyUrlChallengeCodeDotnet.Controllers;
using Shyjus.BrowserDetection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class UrlsControllerTest
    {
        private Mock<IUrlService> UrlServiceMock;
        private Mock<IStatisticsService> StatisticsServiceMock;

        [SetUp]
        public void Setup()
        {
            UrlServiceMock = new Mock<IUrlService>();
            StatisticsServiceMock = new Mock<IStatisticsService>();
        }

        [Test]
        public async Task Index_Return_Not_Null_TypeOf_IActionResult_When_Executed_Successfully()
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>();
            UrlServiceMock.Setup(x => x.ReaAllAsync()).Returns(Task.FromResult<IEnumerable<Url>>(new List<Url>()));
            var controller = new UrlsController(logger.Object, browserDetector.Object, UrlServiceMock.Object, StatisticsServiceMock.Object);

            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [TestCase("")]
        [TestCase(null)]
        public async Task Show_Should_Redirect_To_NotFoundPage_When_Invalid_Url(string url)
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>();
            var service = new Mock<IUrlService>();
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object, StatisticsServiceMock.Object);

            var result = await controller.Show(url) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.True(result.ActionName.Contains("NotFoundPage"));
        }

        [Test]
        public async Task Show_Should_Redirect_To_NotFoundPage_When_Executed_With_Not_Existent_Url()
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            service.Setup(x => x.ReadByCode(It.IsAny<string>())).Returns(Task.FromResult((Url)null));
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object, StatisticsServiceMock.Object);

            var result = await controller.Show("ABCDE") as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.True(result.ActionName.Contains("NotFoundPage"));
        }

        [Test]
        public async Task Create_Should_Redirect_To_Error_When_Executed_With_Invalid_Url()
        {
            var url = new Url { ShortUrl = "ABCDE" };
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>(MockBehavior.Loose);
            var service = new Mock<IUrlService>();
            var controller = new UrlsController(logger.Object, browserDetector.Object, service.Object, StatisticsServiceMock.Object);

            var result = await controller.Create("ABCDE") as ViewResult;

            Assert.True(result.ViewName.Contains("Error"));
        }

        [Test]
        public async Task Visit_Should_Redirect_To_NotFoundPage_When_Executed_Invalid_Url()
        {
            var logger = new Mock<ILogger<UrlsController>>();
            var browserDetector = new Mock<IBrowserDetector>();
            UrlServiceMock.Setup(x => x.ReaAllAsync()).Returns(Task.FromResult<IEnumerable<Url>>(new List<Url>()));
            var controller = new UrlsController(logger.Object, browserDetector.Object, UrlServiceMock.Object, StatisticsServiceMock.Object);

            var result = await controller.Visit("ABCDE") as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.True(result.ActionName.Contains("NotFoundPage"));
        }
        
    }
}