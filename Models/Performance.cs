public class Performance
{
   public int PerformanceId { get; set; }
   public int ArtistId { get; set; }
   public string Location { get; set; }
   public DateTime Date { get; set; }
   public DateTime Time { get; set; }

   public override string ToString()
   {
      return $"\n\x1b[36mPerformanceId:\x1b[0m {PerformanceId}, \x1b[36mArtistId:\x1b[0m {ArtistId}, \x1b[36mLocation:\x1b[0m {Location}, \x1b[36mDate:\x1b[0m {Date.ToShortDateString()}, \x1b[36mTime:\x1b[0m {Time.ToString("H:mm")}";
   }
}