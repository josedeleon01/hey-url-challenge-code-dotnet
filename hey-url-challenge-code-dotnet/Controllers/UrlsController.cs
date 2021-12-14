using System;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services.Interfaces;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using HeyUrlChallengeCodeDotnet.Models;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlService _urlService;
        private readonly IStatisticsService _statisticsService;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlService urlService, IStatisticsService statisticsService)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _urlService = urlService;
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            model.Urls = await _urlService.ReaAllAsync();
            //model.NewUrl = new();
            return View(model);
        }

        [Route("/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            Url urlObj = await _urlService.ReadByCode(url);
            if (urlObj == null)
                return RedirectToAction("NotFoundPage");

            urlObj.Count++;

            await _urlService.UpdateUrl(urlObj);
            await _statisticsService.SaveAsync(new UrlStatistics 
            {   Id  = new Guid(),
                Browser = this.browserDetector.Browser.Name, 
                Platform = this.browserDetector.Browser.OS, 
                Created  = DateTime.Now, 
                Url = urlObj
            });

            return Redirect(urlObj.OriginalUrl);
        }

        [Route("urls/{url}")]
        public async Task<IActionResult> Show(string url)
        {
            Url urlObj = await _urlService.ReadByCode(url);
            if (urlObj == null)
                return RedirectToAction("NotFoundPage");
            IEnumerable<UrlStatistics> urlStatistics = await _statisticsService.ReadByUrlIdAsync(urlObj.Id);

            return View(new ShowViewModel
            {

                Url = urlObj,
                DailyClicks = urlStatistics.OrderBy(d => d.Created).GroupBy(x => x.Created.ToString("dd/MM/yyyy")).Select(group => new
                {
                    Metric = group.Key,
                    Count = group.Count()
                }).ToDictionary(v => v.Metric, v => v.Count),

                BrowseClicks = urlStatistics.GroupBy(x => x.Browser).Select(group => new 
                {
                    Metric = group.Key,
                    Count = group.Count()
                }).ToDictionary(v => v.Metric, v => v.Count),

                PlatformClicks = urlStatistics.GroupBy(x => x.Platform).Select(group => new 
                {
                    Metric = group.Key,
                    Count = group.Count()
                }).ToDictionary(v => v.Metric, v => v.Count)
            });

        }

        [HttpPost("urls/{url}")]
        public async Task<IActionResult> Create(string url)
        {
            try
            {
                var isValidUrl = _urlService.IsValidUrl(url);
                if (!isValidUrl)
                    return View("Error", new ErrorViewModel { ErrorMessage = $"Invalid Url: {url}" });

                var baseUrl = $"{Request?.Scheme}://{Request?.Host.Value}/";
                var generatedCode = _urlService.GenerateCode();
                await _urlService.SaveUrl(generatedCode, baseUrl, url);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = e.StackTrace });
            }
        }

        [Route("404")]
        public IActionResult NotFoundPage()
        {
            return View("404");
        }
    }
}