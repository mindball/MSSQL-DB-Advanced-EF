namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Data;
    using Newtonsoft.Json;
    using Services;
    using Services.Contracts;

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
			throw new NotImplementedException();
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