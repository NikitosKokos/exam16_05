public interface ICSVRepostitory{
   bool ExportToCSV(List<Performance> performances, List<Artist> artists);
}
public class CSVRepostitory : ICSVRepostitory
{
   private string _savePath = "./Files";

   public bool ExportToCSV(List<Performance> performances, List<Artist> artists){
      try{
         string filePath = $"{Path.Combine(_savePath, "performances")}.csv";

         using (StreamWriter writer = new StreamWriter(filePath))
         {
            writer.WriteLine("Artist Name, Location, Date, Time,");
            foreach(var performance in performances){
               string artistName = artists.Where(a => a.ArtistId == performance.ArtistId).First().Name;
               string line = $"{artistName},{performance.Location},{performance.Date.ToShortDateString()},{performance.Time.ToString("H:mm")}";
               writer.WriteLine(line);
            }
         }
         return true;
      }catch(Exception ex){
         Console.WriteLine(ex);
         return false;
      }
   }
}