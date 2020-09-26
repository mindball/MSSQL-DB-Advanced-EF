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

    public static class Deserializer
	{
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
			throw new NotImplementedException();
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