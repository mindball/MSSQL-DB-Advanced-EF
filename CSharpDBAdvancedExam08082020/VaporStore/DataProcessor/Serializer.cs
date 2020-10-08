namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using AutoMapper;

    using Data;
    using Newtonsoft.Json;
    using Services;
    using Services.Contracts;
    using VaporStore.Models.Enums;
    using VaporStore.Services.Models.ExportUserPurchasesByType;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreContext context, string[] genreNames)
		{
			IConfigurationProvider Config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<VaporStoreProfile>();
			});

			IMapper Mapper = Config.CreateMapper();

			IVaportService vaportService = new VaportService(context);

			//using profile
			var genres = vaportService.SearchGenres(Mapper)
				.Where(g => genreNames.Contains(g.Genre))
				.Select(g => new
				{
					Id = g.Id,
					Genre = g.Genre,
					Games = g.Games
						.Where(ga => ga.Players > 0)
						.OrderByDescending(ga => ga.Players),
					TotalPlayer = g.Games.Sum(a => a.Players)
				})				
				.ToList();

			//using complex linq query author solution:
			var complexLinqQuery = LinqQuery(context, genreNames);

			var jsonSettings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore
			};

			//string jsonProfile = JsonConvert.SerializeObject(genres, Formatting.Indented);

			string jsonLinqQuery = JsonConvert.SerializeObject(complexLinqQuery, Formatting.Indented);

			//return jsonProfile;

			return jsonLinqQuery;
		}

        public static string ExportUserPurchasesByType(VaporStoreContext context, string storeType)
		{
			var serializer = new XmlSerializer(typeof(List<UserViewModel>), new XmlRootAttribute("Users"));

			var perchaseType = Enum.Parse<PurchaseType>(storeType);

			var data = context.Users
				.ToArray()
				.Where(u => u.Cards.Any(c => c.Purchases.Any()))
				.Select(u => new UserViewModel
				{
					Username = u.Username,
					Purchases = u.Cards.SelectMany(c => c.Purchases.Where(p => p.Type == perchaseType)
						.Select(p => new PurchaseViewModel
						{
							Card = c.Number,
							Cvc = c.Cvc,
							Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
							Game = new PurchaseGameViewModel
							{
								Title = p.Game.Name,
								Genre = p.Game.Genre.Name,
								Price = p.Game.Price,
							},
						})).OrderBy(p => p.Date).ToArray(),
					TotalSpent = u.Cards.Sum(c => c.Purchases.Where(p => p.Type == perchaseType).Sum(p => p.Game.Price))
				})
				.Where(u => u.Purchases.Length > 0)
				.OrderByDescending(u => u.TotalSpent)
				.ThenBy(u => u.Username)
				.ToList();

			var xmlSerializer = new XmlSerializer(typeof(List<UserViewModel>),
											new XmlRootAttribute("Users"));

			var sb = new StringBuilder();
			var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

			using (var writer = new StringWriter(sb))
			{
				xmlSerializer.Serialize(writer, data, namespaces);
			}

			return sb.ToString().TrimEnd();
		}

        private static object LinqQuery(VaporStoreContext context, 
			string[] genreNames)
        {
			var linqQuery = context
				.Genres
				.ToArray()
				.Where(g => genreNames.Contains(g.Name))
				.Select(g => new
				{
					Id = g.Id,
					Genre = g.Name,
					Games = g.Games
						.Where(ga => ga.Purchases.Any())
						.Select(ga => new
						{
							Id = ga.Id,
							Title = ga.Name,
							Developer = ga.Developer.Name,
							Tags = String.Join(", ", ga.GamesTags
								.Select(gt => gt.Tag.Name)
								.ToArray()),
							Players = ga.Purchases.Count
						})
						.OrderByDescending(ga => ga.Players)
						.ThenBy(ga => ga.Id)
						.ToArray(),
					TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
				})
				.OrderByDescending(g => g.TotalPlayers)
				.ThenBy(g => g.Id)
				.ToArray();

			return linqQuery;
		}
	}
}