using MovieSolution.Models;
using MovieSolution.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MovieSolution.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MovieSolution.Services
{

    public class MovieService : IMovieService
    {

        public IEnumerable<Movie> SaveMovies(Movie request) {


            if( request != null)
            {

                List<Movie> database = new List<Movie>();
                database.Add(request);
                return database;

            }

            return null;
        }
        public IEnumerable<Movie> GetMetaData(int movieId,string filePath)
        {



            var metadata = MovieIntegration.ReadMetaDataCSV(filePath);
            var results = metadata.Where(s => s.MovieId == movieId).OrderBy(s => s.Language);
            var latest_metadata = results.GroupBy(e => e.Language).Select(g => g.Last()).ToList();


            foreach (var result in latest_metadata)
            {
                if (string.IsNullOrEmpty(result.Language) || string.IsNullOrEmpty(result.Title) || string.IsNullOrEmpty(result.Duration) || string.IsNullOrEmpty(result.ReleaseYear.ToString()))
                    throw new Exception("Invalid Payload");
            }

            return latest_metadata;


        }
        private static decimal ComputeAverageWatch(int count, decimal time) {

            var average = (time / count) / 1000;
            return average;
             
        }
        public IEnumerable<MovieStatsResponse> GetStatsResponses(string metadataFilePath, string statsFilePath)
        {

            var metadata = MovieIntegration.ReadMetaDataCSV(metadataFilePath);
            var stats = MovieIntegration.ReadStats(statsFilePath);
            var results = metadata.Select(s =>s.MovieId).Distinct().ToList(); // movieIds
            List<MovieStatsResponse> statsResponse = new List<MovieStatsResponse>();
            MovieStatsResponse movieStatsResponse = new MovieStatsResponse();


            if (results.Count() == 0)
                Console.WriteLine(""); // break the code here


            foreach (var result in results)
            {

                var sum = stats.ToList().Where(c => c.MovieId == result).Sum(o=> (decimal) o.WatchDurationMs);
                var count = stats.ToList().Where(c => c.MovieId == result).Count();
                var movie = metadata.GroupBy(e => e.MovieId).Select(g => g.First()).ToList().Where(c=>c.MovieId == result);
                var averageWatch = ComputeAverageWatch(count, sum);


                movieStatsResponse.MovieId = movie.ToList()[0].MovieId;
                movieStatsResponse.Title = movie.ToList()[0].Title;
                movieStatsResponse.AverageWatchDurationS = Math.Round(averageWatch);
                movieStatsResponse.Watches = count;
                movieStatsResponse.ReleaseYear = movie.ToList()[0].ReleaseYear;

                statsResponse.Add(movieStatsResponse);
                movieStatsResponse = new MovieStatsResponse();

            }

            return statsResponse.OrderByDescending(e=>e.Watches);

        }

    }
}
