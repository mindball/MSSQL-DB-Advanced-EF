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

	using AutoMapper;
    using System.Text;

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
			var games = vaportService.Search(Mapper)
						.Where(x => x.Players > 0)
						.OrderByDescending(x => x.Players);

            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.Id}");
                Console.WriteLine($"Title: {game.Title}");
                Console.WriteLine($"Developer: {game.Developer}");

                StringBuilder str = new StringBuilder();
                str.Append("\"");

                foreach (var tag in game.Tags)
                {
                    str.Append(tag.Name);
                    str.Append(", ");
                }
                string tags = str.ToString().TrimEnd(',') + "\"";

                Console.WriteLine(tags);

                Console.WriteLine($"Players: {game.Tags.Count}");
            }

            return null;
		}

		public static string ExportUserPurchasesByType(VaporStoreContext context, string storeType)
		{
			throw new NotImplementedException();
		}
	}
}