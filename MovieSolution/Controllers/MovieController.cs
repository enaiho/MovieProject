using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSolution.Services;
using MovieSolution.Models;
using MovieSolution.IServices;
using MovieSolution.DataTransferObject;
using Microsoft.Extensions.Configuration;

namespace MovieSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        IConfiguration _iconfiguration;
        private readonly IMovieService movieService;


        public MovieController(IMovieService movie,IConfiguration iconfiguration)
        {
            movieService = movie;
            _iconfiguration = iconfiguration;
        }


        [HttpPost]
        [Route("[action]")]
        [Route("/metadata")]

        public IActionResult SaveMovieMetaData(Movie movie)
        {
            return Ok(movieService.SaveMovies(movie));
        }

        [HttpGet]
        [Route("[action]")]
        [Route("/metadata/{movieId}")]
        public IActionResult GetMetaData(int movieId)
        {

            string metadataFilePath = _iconfiguration.GetSection("Data").GetSection("MetaDataFilePath").Value;
            var metadata = movieService.GetMetaData(movieId,metadataFilePath);
            if (metadata.ToList().Count() == 0)
                return NotFound();

            return Ok(metadata);
                 
        }

        [HttpGet]
        [Route("[action]")]
        [Route("/movies/stats")]
        public IActionResult GetStats()
        {
            string metadataFilePath = _iconfiguration.GetSection("Data").GetSection("MetaDataFilePath").Value;
            string statsFilePath = _iconfiguration.GetSection("Data").GetSection("StatsFilePath").Value;
            return Ok(movieService.GetStatsResponses(metadataFilePath,statsFilePath ));

        }
    }

}
