using MySql.Data.MySqlClient;

public interface IPerformanceRepository{
   bool AddPerformance(Performance performance);
   Performance GetPerformanceById(int id);
   bool DeletePerformance(int performanceId);
   List<Performance> GetPerformances();
}

public class PerformanceRepository : IPerformanceRepository{
   public bool AddPerformance(Performance performance){
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();

            MySqlCommand command = new MySqlCommand("INSERT INTO tblperformance(ArtistId,Location,Date,Time) VALUES(@ArtistId,@Location,@Date,@Time)",mySqlConnection);
            command.Parameters.AddWithValue("@ArtistId", performance.ArtistId);
            command.Parameters.AddWithValue("@Location", performance.Location);
            command.Parameters.AddWithValue("@Date", performance.Date.ToShortDateString());
            command.Parameters.AddWithValue("@Time", performance.Time.ToString("H:mm"));
            command.ExecuteNonQuery();

         }
         return true;
      }catch(Exception ex){
         Console.WriteLine(ex);
         return false;
      }
   }

   public bool DeletePerformance(int performanceId){
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();

            MySqlCommand command = new MySqlCommand("DELETE FROM tblperformance WHERE PerformanceId = @PerformanceId",mySqlConnection);
            command.Parameters.AddWithValue("@PerformanceId", performanceId);
            command.ExecuteNonQuery();

         }
         return true;
      }catch(Exception ex){
         Console.WriteLine(ex);
         return false;
      }
   }

   public Performance GetPerformanceById(int id){
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM tblperformance where PerformanceId = @PerformanceId", mySqlConnection);
            command.Parameters.AddWithValue("@PerformanceId", id);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()){
               Performance newPerformance = new Performance();
               newPerformance.PerformanceId = reader.GetInt32("PerformanceId");
               newPerformance.ArtistId = reader.GetInt32("ArtistId");
               newPerformance.Location = reader.GetString("Location");
               newPerformance.Date = DateTime.Parse(reader.GetString("Date"));
               newPerformance.Time = DateTime.Parse(reader.GetString("Time"));

               return newPerformance;
            }
            // If no record found with the specified ID, return null
            return null;
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
         return null;
      }
   }

   public List<Performance> GetPerformances(){
      List<Performance> performances = new List<Performance>();
      try{
         
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM tblperformance", mySqlConnection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()){
               Performance newPerformance = new Performance();
               newPerformance.PerformanceId = reader.GetInt32("PerformanceId");
               newPerformance.ArtistId = reader.GetInt32("ArtistId");
               newPerformance.Location = reader.GetString("Location");
               newPerformance.Date = DateTime.Parse(reader.GetString("Date"));
               newPerformance.Time = DateTime.Parse(reader.GetString("Time"));

               performances.Add(newPerformance);
            }
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
      }

      return performances;
   }

   private MySqlConnection CreateConnection(){
      string connectionString = "server=localhost;database=examadvsoftware;user=root;pwd=root";
      MySqlConnection connection = new MySqlConnection(connectionString);
      return connection;
   }
}