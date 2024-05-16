public class Artist
{
   public int ArtistId{get;set;}
   public string Name{get;set;}
   public string Genre{get;set;}
   public string Country{get;set;}
   public string ActiveYears{get;set;}
   public int Popularity{get;set;}

   public override string ToString()
   {
      return $"\n\x1b[36mArtistId:\x1b[0m {ArtistId}, \x1b[36mName:\x1b[0m {Name}, \x1b[36mGenre:\x1b[0m {Genre}, \x1b[36mCountry:\x1b[0m {Country}, \x1b[36mActiveYears:\x1b[0m {ActiveYears}, \x1b[36mPopularity:\x1b[0m {Popularity}";
   }
}