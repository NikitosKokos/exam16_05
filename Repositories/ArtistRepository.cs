using MySql.Data.MySqlClient;

public interface IArtistRepository {
   List<Artist> GetArtistsByPopularity(int popularity);
   bool UpdatePopularity(Artist artist, int newPopularity);
   Artist GetArtistById(int id);
   float AveragePopularity();
   List<Artist> GetArtists();
}
public class ArtistRepository : IArtistRepository
{
   public List<Artist> GetArtistsByPopularity(int popularity){
      List<Artist> artists = new List<Artist>();
      try{
         
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM tblartist where Popularity > @Popularity order by Name asc", mySqlConnection);
            command.Parameters.AddWithValue("@Popularity", popularity);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()){
               Artist newArtist = new Artist();
               newArtist.ArtistId = reader.GetInt32("ArtistId");
               newArtist.Name = reader.GetString("Name");
               newArtist.Genre = reader.GetString("Genre");
               newArtist.Country = reader.GetString("Country");
               newArtist.ActiveYears = reader.GetString("ActiveYears");
               newArtist.Popularity = reader.GetInt32("Popularity");

               artists.Add(newArtist);
            }
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
      }

      return artists;
   }

   public List<Artist> GetArtists(){
      List<Artist> artists = new List<Artist>();
      try{
         
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM tblartist", mySqlConnection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()){
               Artist newArtist = new Artist();
               newArtist.ArtistId = reader.GetInt32("ArtistId");
               newArtist.Name = reader.GetString("Name");
               newArtist.Genre = reader.GetString("Genre");
               newArtist.Country = reader.GetString("Country");
               newArtist.ActiveYears = reader.GetString("ActiveYears");
               newArtist.Popularity = reader.GetInt32("Popularity");

               artists.Add(newArtist);
            }
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
      }

      return artists;
   }

   public Artist GetArtistById(int id){
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM tblartist where ArtistID = @ArtistId", mySqlConnection);
            command.Parameters.AddWithValue("@ArtistId", id);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()){
               Artist newArtist = new Artist();
               newArtist.ArtistId = reader.GetInt32("ArtistId");
               newArtist.Name = reader.GetString("Name");
               newArtist.Genre = reader.GetString("Genre");
               newArtist.Country = reader.GetString("Country");
               newArtist.ActiveYears = reader.GetString("ActiveYears");
               newArtist.Popularity = reader.GetInt32("Popularity");

               return newArtist;
            }
            // If no record found with the specified ID, return null
            return null;
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
         return null;
      }
   }

   public bool UpdatePopularity(Artist artist, int newPopularity){
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("UPDATE tblartist SET Name = @Name, Genre = @Genre, Country = @Country, ActiveYears = @ActiveYears, Popularity = @Popularity where ArtistID = @ArtistId; SELECT ArtistID FROM tblartist WHERE ArtistID = @ArtistId;", mySqlConnection);
            command.Parameters.AddWithValue("@ArtistId", artist.ArtistId);
            command.Parameters.AddWithValue("@Name", artist.Name);
            command.Parameters.AddWithValue("@Genre", artist.Genre);
            command.Parameters.AddWithValue("@Country", artist.Country);
            command.Parameters.AddWithValue("@ActiveYears", artist.ActiveYears);
            command.Parameters.AddWithValue("@Popularity", newPopularity);

            // ExecuteScalar is used to retrieve the ID of the updated record
            int id = Convert.ToInt32(command.ExecuteScalar());

            // If the ID returned is the same as the ID of the person being updated,
            // update was successful, so return the updated person object
            if (id == artist.ArtistId)
               return true;
            else
               return false; // Return null if the update failed
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
         return false;
      }
   }

   public float AveragePopularity(){
      float averagePopularity = -1;
      try{
         using(MySqlConnection mySqlConnection = CreateConnection()){
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT avg(Popularity) as 'averagePopularity' FROM tblartist", mySqlConnection);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()){
               averagePopularity = reader.GetFloat("averagePopularity");
            }
            
            return averagePopularity;
         }

      }catch(Exception ex){
         Console.WriteLine(ex);
         return averagePopularity;
      }
   }

   private MySqlConnection CreateConnection(){
      string connectionString = "server=localhost;database=examadvsoftware;user=root;pwd=root";
      MySqlConnection connection = new MySqlConnection(connectionString);
      return connection;
   }
}