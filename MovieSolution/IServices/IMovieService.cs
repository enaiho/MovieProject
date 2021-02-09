using MovieSolution.DataTransferObject;
using MovieSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieSolution.IServices
{
    public interface IMovieService
    {
        IEnumerable<Movie> SaveMovies(Movie request);
        IEnumerable<Movie> GetMetaData(int movieId,string filePath);
        IEnumerable<MovieStatsResponse> GetStatsResponses(string metadataFilePath,string statsFilePath);
    }

}
