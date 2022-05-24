using HomePlayer_ReactJS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
