namespace VaporStore
{
	using System;
    using System.Linq;
    using AutoMapper;

	using Models;
	using Models.Enums;
	using Services.Models;

	public class VaporStoreProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public VaporStoreProfile()
		{
			//Games
			this.CreateMap<Game, GamesViewModel>()
				.ForMember(gvn => gvn.Title,
				g => g.MapFrom(y => y.Name))
                .ForMember(t => t.Tags,
                //g => g.MapFrom(gt => gt.GamesTags))
				g => g.MapFrom(gt => string.Join(", ", gt.GamesTags
												.Select(a => a.Tag.Name).ToArray())))
                .ForMember(gvn => gvn.Players,
				g => g.MapFrom(y => y.Purchases.Count))
				.ForMember(d => d.Developer,
				dv => dv.MapFrom(v => v.Developer.Name));

			//Tags 
			this.CreateMap<GameTags, TagViewModel>()
				.ForMember(t => t.Name,
				g => g.MapFrom(y => y.Tag.Name));

			//Genre
			this.CreateMap<Genre, GenreViewModel>()
				.ForMember(gvr => gvr.Genre,
				g => g.MapFrom(n => n.Name));
				//.ForMember(gvr => gvr.PlayersCount,
				//g => g.MapFrom(n => n.Games.Sum(ga => ga.Purchases.Count)));

		}
	}
}