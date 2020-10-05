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
			this.CreateMap<Game, GamesViewModel>()
				.ForMember(gvn => gvn.Title,
				g => g.MapFrom(y => y.Name))
                .ForMember(t => t.Tags,
                g => g.MapFrom(gt => gt.GamesTags))
                .ForMember(gvn => gvn.Players,
				g => g.MapFrom(y => y.Purchases.Count))
				.ForMember(d => d.Developer,
				dv => dv.MapFrom(v => v.Developer.Name));

			this.CreateMap<GameTags, TagViewModel>()
				.ForMember(t => t.Name,
				g => g.MapFrom(y => y.Tag.Name));
		}
	}
}