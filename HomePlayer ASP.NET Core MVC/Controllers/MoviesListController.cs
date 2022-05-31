using HomePlayer_ReactJS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HomePlayer_ReactJS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesListController : ControllerBase
    {

        private readonly ILogger<MoviesListController> _logger;

        public MoviesListController(ILogger<MoviesListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "HomePlayer");
            var movies = new List<Movie>();
            var files = Directory.GetFiles(path);
            foreach (var fullpath in files)
            {
                var file = Path.GetFileName(fullpath);
                var movie = new Movie(title: file);
                movies.Add(movie);
            }
            return movies;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),"HomePlayer");
                Directory.CreateDirectory(path);
                if (file.Length > 0)
                {
                    var fileName = formCollection["title"].FirstOrDefault(Guid.NewGuid().ToString("n").Substring(0, 8)) + ".mp4"; //ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Redirect("/movies-list");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }


}
