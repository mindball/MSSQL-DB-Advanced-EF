namespace Lesson
{
    using DataModel;
    using ModelViewController;
    using ModelViewController.DataTransferObects;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            /* MVC -> 
                    M -> Model : Data(Create clean model classes and easily bind them to your 
                                    database.)
                    V -> view model (View is a user interface)
                    C -> Controller:
                        * Accepts input and converts it to commands for the model or view.
                        * The controller responds to the user input and performs 
                                interactions on the data model objects. 
                                The controller receives the input, optionally validates 
                                it and then passes the input to the model.
                         * Service : Between the controller and the model sometimes goes a 
                                       layer which is called a service. It fetches data from the 
                                       model and lets the controller use the fetched data. 
                                       This layer allows to separate data storage (model), 
                                       data fetching (service) and data manipulation (controller).
                                       Since this layer is not part of the original MVC concept, 
                                       it is optional in most cases but can be useful for code 
                                       management and reusability purposes in some cases
            */
            //MVC models
            //var service = new ArtistsService(new MusicXContext());
            //var artists = service.GetAllWithCount();

            //Fast way to change interface visualization ot data
            //View model example 2
            //PrintArtistsAsJson(artists);

            //View model example 1
            //PrintArtists(artists);


            //AutoMapper example           
            //Where is power of automapper:
            // First lets take a look without mapper:
            //ExampleWithoutAutoMapper();

            ExampleWithAutoMapper();

        }

        

        //View model example 1
        public static void PrintArtists(IEnumerable<ArtistWithCount> artistWithCount)
        {
            foreach (var art in artistWithCount)
            {
                Console.WriteLine($"{art.Name} - {art.Count}");
            }
        }

        //View model example 2
        public static void PrintArtistsAsJson(IEnumerable<ArtistWithCount> artistWithCount)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artistWithCount, Formatting.Indented));
        }
       

        public static void ExampleWithoutAutoMapper()
        {
            var service = new ArtistServiceWithoutMapper(new MusicXContext());
            var artists = service.GetAllWithCountDTOWithSmallProperties();
            PrintArtistsAsJsonExampleWithoutMapper(artists);

            var serviceBig = new ArtistServiceWithoutMapper(new MusicXContext());
            var artistsBig = serviceBig.GetAllWithCountDTOWithBigProperties();
            PrintArtistsAsJsonExampleWithoutMapper(artistsBig);
        }

        public static void PrintArtistsAsJsonExampleWithoutMapper(IEnumerable<ArtistWithCountWithSmallProperties> artistWithCount)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artistWithCount, Formatting.Indented));
        }

        public static void PrintArtistsAsJsonExampleWithoutMapper(IEnumerable<ArtistWithCountWithBigProperties> artistWithCount)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artistWithCount, Formatting.Indented));
        }

        public static void ExampleWithAutoMapper()
        {
            var service = new ArtistWithCountWithAutoMapper(new MusicXContext());
            var artist = service.GetAllWithCountDTOWithBigProperties();

            PrintArtistsAsJsonExampleWithMapper(artist);
        }

        private static void PrintArtistsAsJsonExampleWithMapper(IEnumerable<ArtistWithCountWithBigProperties> artist)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artist, Formatting.Indented));
        }
    }
}
