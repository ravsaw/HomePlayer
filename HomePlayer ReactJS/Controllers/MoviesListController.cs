using HomePlayer_ReactJS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HomePlayer_ReactJS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesListController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<MoviesListController> _logger;

        public MoviesListController(ILogger<MoviesListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos).ToString());
            return Enumerable.Range(1, 5).Select(index => new Movie
            {
                Name = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMovie()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Movies");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
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
