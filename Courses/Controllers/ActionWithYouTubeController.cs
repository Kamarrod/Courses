using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Courses.Domain.Api_keys;
using System.Drawing;

namespace Courses.Controllers
{
        [Authorize]
    public class ActionWithYouTubeController : Controller
    {
        public async Task<IActionResult> ShowVideo(string url)
        {
            string embedUrl = url;
            try
            {
                var keys = new API_keys();
                var videoId = HttpUtility.ParseQueryString(new Uri(url).Query).Get("v");
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = keys.YouTubeAPI,
                    ApplicationName = this.GetType().ToString()
                });

                var videoRequest = youtubeService.Videos.List("snippet");
                videoRequest.Id = videoId;
                var videoResponse = videoRequest.Execute();

                embedUrl = string.Format("https://www.youtube.com/embed/{0}?start=0&mute=1 ", videoId);

                ViewBag.Title = videoResponse.Items[0].Snippet.Title;
                ViewBag.Description = videoResponse.Items[0].Snippet.Description;
                ViewBag.EmbedUrl = embedUrl;
            } catch (Exception ex) 
            {
                RedirectToAction("Error", ex.Message);
            }
            return View("ShowVideo", embedUrl);
        }
    }
}
