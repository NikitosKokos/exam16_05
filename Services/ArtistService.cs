public interface IArtistService{
   List<Artist> GetArtistsByPopularity(int popularity);
   bool UpdatePopularity(int artistId, int newPopularity);
   float AveragePopularity();
   bool AddPerformance(Performance performance);
   Performance GetPerformanceById(int id);
   Artist GetArtistById(int id);
   bool DeletePerformance(int performanceId);

   bool ExportPerformances();
}
public class ArtistService : IArtistService
{
   private IArtistRepository _artistRepository;
   private IPerformanceRepository _performanceRepository;
   private ICSVRepostitory _CSVRepostitory;

   public ArtistService(IArtistRepository artistRepository,IPerformanceRepository performanceRepository, ICSVRepostitory CSVRepostitory){
      _artistRepository = artistRepository;
      _performanceRepository = performanceRepository;
      _CSVRepostitory = CSVRepostitory;
   }

   public List<Artist> GetArtistsByPopularity(int popularity){
      if(popularity < 0 || popularity > 10){
         return null;
      }
      List<Artist> artists = _artistRepository.GetArtistsByPopularity(popularity);

      return artists;
   }

   public bool UpdatePopularity(int artistId, int newPopularity){
      Artist artist = _artistRepository.GetArtistById(artistId);
      if(artist == null){
         return false;
      }
      if(newPopularity > 10 || newPopularity < 0){
         throw new InvalidPopularityException("newPopularity should be between 0 and 10!");
      }

      bool isUpdated = _artistRepository.UpdatePopularity(artist, newPopularity);

      return isUpdated;
   }

   public float AveragePopularity(){
      return _artistRepository.AveragePopularity();
   }

   public bool AddPerformance(Performance performance){
      return _performanceRepository.AddPerformance(performance);
   }

   public bool DeletePerformance(int performanceId){
      return _performanceRepository.DeletePerformance(performanceId);
   }

   public Performance GetPerformanceById(int id){
      return _performanceRepository.GetPerformanceById(id);
   }

   public Artist GetArtistById(int id){
      return _artistRepository.GetArtistById(id);
   }

   public bool ExportPerformances(){
      List<Performance> performances = _performanceRepository.GetPerformances();
      List<Artist> artists = _artistRepository.GetArtists();

      return _CSVRepostitory.ExportToCSV(performances, artists);
   }
}