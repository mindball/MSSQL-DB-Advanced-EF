namespace VaporStore
{
	using System;
    using System.Linq;
    using AutoMapper;

	using Models;
	using Models.Enums;	
    using VaporStore.Services.Models.ExportAllGamesByGenres;
    using VaporStore.Services.Models.ExportUserPurchasesByType;

    public class VaporStoreProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public VaporStoreProfile()
		{
			/* ExportAllGamesByGenres
			 * GenreViewModel
			 *		GamesViewModel
			 *			TagViewModel
			**/

			//GenreViewModel
			this.CreateMap<Genre, GenreViewModel>()
				.ForMember(gvr => gvr.Genre,
				g => g.MapFrom(n => n.Name));
			//.ForMember(gvr => gvr.PlayersCount,
			//g => g.MapFrom(n => n.Games.Sum(ga => ga.Purchases.Count)));

			//GamesViewModel
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

			//TagViewModel 
			this.CreateMap<GameTags, TagViewModel>()
				.ForMember(t => t.Name,
				g => g.MapFrom(y => y.Tag.Name));


			/* ExportUserPurchasesByType
			 * UserViewModel
			 *		PurchaseViewModel
			 *			PurchaseGameViewModel
			**/

			//UserViewModel
			//this.CreateMap<User, UserViewModel>()
			//	.ForMember(uvm => uvm.,
			//	u => u.MapFrom(up => up.Cards.Select(c => c.Purchases.Any())));

			//this.CreateMap<Card, PurchaseViewModel>()
			//	.ForMember(pvm => pvm.Card,
			//	p => p.MapFrom(pc => pc.Number))
			//	.ForMember(pvm => pvm.Cvc,
			//	p => p.MapFrom(pc => pc.Cvc))
			//	.ForMember(d => d.Date,
			//	p => p.MapFrom(pc => pc.));

			this.CreateMap<Purchase, PurchaseViewModel>()
				.ForMember(pvm => pvm.Card,
				c => c.MapFrom(cp => cp.Card.Number))
				.ForMember(pvm => pvm.Cvc,
				c => c.MapFrom(cp => cp.Card.Cvc));

			this.CreateMap<Game, PurchaseGameViewModel>()
				.ForMember(pgvm => pgvm.Title,
				g => g.MapFrom(ga => ga.Name))
				.ForMember(pgvm => pgvm.Genre,
				g => g.MapFrom(ga => ga.Genre.Name));
		}
	}
}