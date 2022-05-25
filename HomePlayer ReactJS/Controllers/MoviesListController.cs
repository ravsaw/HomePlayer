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
            return GetMoviesFromJson();
        }

        private static IEnumerable<Movie> GetMoviesFromJson()
        {
            var pathToJson = Path.Combine(Directory.GetCurrentDirectory(), "Model", "MoviesDB.json");
            using (StreamReader r = new StreamReader(pathToJson))
            {
                string json = r.ReadToEnd();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                return movies ??= Enumerable.Empty<Movie>();
            }
        }

        private static void SaveMoviesToJson(IEnumerable<Movie> movies)
        {
            var pathToJson = Path.Combine(Directory.GetCurrentDirectory(), "Model", "MoviesDB.json");
            using (StreamWriter sw = new StreamWriter(pathToJson, false))
            {
                sw.WriteLine(JsonConvert.SerializeObject(movies));
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMovie()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),"HomePlayer");
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(path, fileName);
                    var movie = new Movie() { Name = fileName, Path = fullPath };
                    var movies = GetMoviesFromJson().Append(movie);
                    SaveMoviesToJson(movies);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok();
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
