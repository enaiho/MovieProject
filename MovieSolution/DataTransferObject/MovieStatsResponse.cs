namespace MovieSolution.DataTransferObject
{
    public class MovieStatsResponse
    {

        public int MovieId { get; set; }
        public string Title { get; set; }
        public decimal AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }

    }
}
