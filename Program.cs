IArtistRepository artistRepository= new ArtistRepository();
IPerformanceRepository performanceRepository= new PerformanceRepository();
ICSVRepostitory CSVRepostitory= new CSVRepostitory();
IArtistService artistService = new ArtistService(artistRepository,performanceRepository,CSVRepostitory);


bool isSelected = false;
while(!isSelected){
   Console.Clear();
   Console.WriteLine("\u001B[1mExam App\u001b[0m");
   Console.WriteLine("1. Show all artists with a popularity higher than a value you specify, in alphabetical order");
   Console.WriteLine("2. Update the artist's popularity");
   Console.WriteLine("3. Average popularity responses");
   Console.WriteLine("4. Add a new performance");
   Console.WriteLine("5. Delete a performance");
   Console.WriteLine("6. Export all performances to a CSV file");
   Console.WriteLine("7. Exit");
   int option = int.Parse(Console.ReadLine());
   ConsoleKeyInfo key;
   switch (option)
   {
      case 1:
         Console.Clear();
         Console.WriteLine("Enter popularity between 0 and 10:");
         int popularity = int.Parse(Console.ReadLine());
         
         List<Artist> artists = artistService.GetArtistsByPopularity(popularity);

         if(artists == null){
            Console.WriteLine("\x1b[31mNot able to find artists\x1b[0m");
         }else{
            if(artists.Count == 0){
               Console.WriteLine("\x1b[31mNo artists were found\x1b[0m");
            }else{
               foreach (Artist artist in artists){
                  Console.WriteLine(artist);
               }
            }
         }
         

         Console.WriteLine("\nPress \x1b[4m\x1b[36mEnter\x1b[0m to go to the main menu");
         key = Console.ReadKey(true);

         switch (key.Key)
         {
            case ConsoleKey.Enter:
               break;
         }
         break;
      case 2:
         Console.Clear();
         Console.WriteLine("Enter an artist's id:");
         int artistId = int.Parse(Console.ReadLine());
         Console.WriteLine("Enter a new popularity :");
         int newPopularity = int.Parse(Console.ReadLine());

         try{
            bool isUpdated = artistService.UpdatePopularity(artistId, newPopularity);

            if(isUpdated){
               Console.WriteLine("\x1b[32mUpdated Successfully!\x1b[0m");
            }else{
               Console.WriteLine("\x1b[31mUpdated NOT Successfully!\x1b[0m");
            }
         }catch(InvalidPopularityException ex){
            Console.WriteLine(ex);
         }
         Task.Delay(1600).Wait();
         break;
      case 3:
         Console.Clear();
         float avgPopularity = artistService.AveragePopularity();

         if(avgPopularity != -1){
            Console.WriteLine($"\x1b[32mAverage Popularity: {avgPopularity}\x1b[0m");
         }else{
            Console.WriteLine("\x1b[31mSomething went wrong, could not calculate Average Popularity\x1b[0m");
         }
         Task.Delay(1600).Wait();

         break;
      case 4:
         Console.Clear();
         Console.WriteLine("Enter an artist's id:");
         int performanceArtistId = int.Parse(Console.ReadLine());
         Console.WriteLine("Enter a Location:");
         string location = Console.ReadLine();
         Console.WriteLine("Enter a Timeslot:");
         DateTime timeslot = DateTime.Parse(Console.ReadLine());
         Console.WriteLine("Enter a Date:");
         DateTime date = DateTime.Parse(Console.ReadLine());

         Artist currentArtist = artistService.GetArtistById(performanceArtistId);

         if(currentArtist == null){
            Console.WriteLine("\x1b[31mArtist doesn't exist!\x1b[0m");
            Task.Delay(1600).Wait();
            break;
         }

         Performance newPerformance = new Performance();
         newPerformance.ArtistId = performanceArtistId;
         newPerformance.Location = location;
         newPerformance.Time = timeslot;
         newPerformance.Date = date;

         bool isAdded = artistService.AddPerformance(newPerformance);

         if(isAdded){
            Console.WriteLine("\x1b[32mAdded Successfully!\x1b[0m");
         }else{
            Console.WriteLine("\x1b[31mAdded NOT Successfully!\x1b[0m");
         }
         Task.Delay(1600).Wait();

         break;
      case 5:
         Console.Clear();
         Console.WriteLine("Enter an Performance's id:");
         int deletePerformanceId = int.Parse(Console.ReadLine());

         Performance performanceToDelete = artistService.GetPerformanceById(deletePerformanceId);

         if(performanceToDelete == null){
            Console.WriteLine("\x1b[31mPerformance doesn't exist!\x1b[0m");
            Task.Delay(1600).Wait();
            break;
         }

         Console.WriteLine(performanceToDelete);

         Console.WriteLine("Are you sure? (Y - yes, N - no)");
         string isSure = Console.ReadLine();
         
         if(isSure == "Y"){
            bool isDeleted = artistService.DeletePerformance(deletePerformanceId);

            if(isDeleted){
               Console.WriteLine("\x1b[32mDeleted Successfully!\x1b[0m");
            }else{
               Console.WriteLine("\x1b[31mDeleted NOT Successfully!\x1b[0m");
            }
            Task.Delay(1600).Wait();
         }
         
         break;
      case 6:
         Console.Clear();
         bool isExported = artistService.ExportPerformances();

         if(isExported){
            Console.WriteLine("\x1b[32mExported Successfully!\x1b[0m");
         }else{
            Console.WriteLine("\x1b[31mExported NOT Successfully!\x1b[0m");
         }
         Task.Delay(1600).Wait();
         break;
      case 7:
         isSelected = true;
         Console.Clear();
         Environment.Exit(0);
         break;
      default:
         break;
   }
}