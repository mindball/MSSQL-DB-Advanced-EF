namespace VaporStore.DataProcessor
{
    using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Xml.Linq;

	using Data;
    using Services;
    using Services.Contracts;

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
			var xmlPurchase = XDocument.Parse(xmlString);
			var rootElements = xmlPurchase.Root.Elements();

			IVaportService vaportService = new VaportService(context);

			foreach (var element in rootElements)
            {
				var gameTitle = element.Attribute("title").Value;
				var purchaseType = element.Element("Type").Value;
				var productKey = element.Element("Key").Value;
				var cardNumber = element.Element("Card").Value;
				var date = element.Element("Date").Value;

				string msg = vaportService.CreatePurchase(gameTitle, purchaseType, productKey, cardNumber, date);
                Console.WriteLine(msg);
			}

			return null;
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}