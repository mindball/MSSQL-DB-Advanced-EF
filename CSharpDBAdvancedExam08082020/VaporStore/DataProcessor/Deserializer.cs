namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

	using Newtonsoft.Json;

    using Data;
    using Models;
    using Services;
    using Services.Contracts;
    using System.Linq;
    using Microsoft.Extensions.Primitives;
    using System.Runtime.CompilerServices;

    public static class Deserializer
	{
		private const string InvalidDataMsg = "Invalid Data";
		private static string ValidDataMsg = "Imported {0} with {1} cards";

		public static string ImportGames(VaporStoreContext context, string jsonString)
		{
            var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            var jsonGame = Newtonsoft.Json.JsonConvert
				.DeserializeObject<JsonGame[]>(jsonString);

			IVaportService vaportService = new VaportService(context);
				foreach (var game in jsonGame)
				{
					string msg = vaportService.CreateGame(
						game.Name, game.Price, game.ReleaseDate, game.Developer, game.Genre, game.Tags
						);

					Console.WriteLine(msg);
				}			

			return null;
		}

		public static string ImportUsers(VaporStoreContext context, string jsonString)
		{
			var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings()
			{
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
			};

			var jsonUser = Newtonsoft.Json.JsonConvert
				.DeserializeObject<JsonUserAndCard[]>(jsonString);

			IVaportService vaportService = new VaportService(context);
			string msg = null;
			bool isValidUserAndCard = false;			

			foreach (var user in jsonUser)
			{
				var existCards = user.Cards.Any();

				foreach (var card in user.Cards)
                {
					
					isValidUserAndCard = vaportService.IsValidCreateUserAndCard(
						user.FullName, user.Username, user.Email, user.Age,
						existCards, card.Number, card.CVC, card.Type);	
				}

				var successfullyAddedCardsCount = context.Cards.
					Where(u => u.User.Username == user.Username)
					.Count();

				msg = isValidUserAndCard ? 
					string.Format(ValidDataMsg, user.Username, successfullyAddedCardsCount) 
					: InvalidDataMsg;

                Console.WriteLine(msg);
			}

			return null;
		}

		public static string ImportPurchases(VaporStoreContext context, string xmlString)
		{
			throw new NotImplementedException();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}