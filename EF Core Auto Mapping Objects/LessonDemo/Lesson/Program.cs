namespace Lesson
{
    using System;
    using System.Collections.Generic;

    using DataModel;
    using ModelViewController;
    using ModelViewController.DataTransferObects;

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

            //Console application in now is View models(view models may be a browse to visuale data)

            var service = new ArtistsService(new MusicXContext());
        }

        public static void PrintArtists(IEnumerable<ArtistWithCount> artistWithCount)
        {
            foreach (var art in artistWithCount)
            {
                Console.WriteLine($"{art.Name} - {art.Count}");
            }
        }
    }
}
