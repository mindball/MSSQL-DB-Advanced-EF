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

			var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings()
			{
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
			};

			string json = JsonConvert.SerializeObject(genres, Formatting.Indented);

			return json;
		}

		public static string ExportUserPurchasesByType(VaporStoreContext context, string storeType)
		{
			throw new NotImplementedException();
		}
	}
}