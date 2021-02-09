using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using MovieSolution.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace MovieSolution
{
    public class MovieIntegration
    {

        private static TextFieldParser ReadCSV(string filePath)
        {
            TextFieldParser parser = new TextFieldParser(new StreamReader(filePath));
            
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            return parser;

        }
        public static List<Movie> ReadMetaDataCSV(string filePath)
        {

            int index = 0;
            string[] values;
            var parser = ReadCSV(filePath);
            Movie movie = new Movie();
            List<Movie> movieList = new List<Movie>();

            while (!parser.EndOfData)
            {
                values = parser.ReadFields();
                if (index > 0)
                {

                    movie.MovieId = int.Parse(values[1]);
                    movie.Title = values[2];
                    movie.Language = values[3];
                    movie.Duration = values[4];
                    movie.ReleaseYear = int.Parse(values[5]);
                    movie.Id = int.Parse(values[0]);


                    movieList.Add(movie);
                    movie = new Movie();


                }

                index++;

            }

            parser.Close();
            return movieList;


        }
        public static List<Stats> ReadStats(string filePath)
        {

            int index = 0;
            string[] values;
            var parser = ReadCSV(filePath);
            Stats stat = new Stats();
            List<Stats> statList = new List<Stats>();

            while (!parser.EndOfData)
            {
                values = parser.ReadFields();
                if (index > 0)
                {

                    stat.MovieId = int.Parse(values[0]);
                    stat.WatchDurationMs = int.Parse(values[1]);
                    statList.Add(stat);
                    stat = new Stats();

                }

                index++;

            }

            parser.Close();
            return statList;
        }

    }

}
