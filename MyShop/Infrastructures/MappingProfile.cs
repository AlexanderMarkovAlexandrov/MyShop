namespace MyShop.Infrastructures
{
    using AutoMapper;
    using MyShop.Data.Models;
    using MyShop.Models.Goods;
    using MyShop.Services.Goods.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<GoodsDetailsServiceModel, GoodsFormModel>();
            this.CreateMap<GoodsServiceModel, Goods>()
                .ReverseMap();
            this.CreateMap<Goods, GoodsDetailsServiceModel>()
                .ForMember(c=> c.Merchant , cfg=> cfg.MapFrom(c=>c.Merchant.Name))
                .ForMember(c=> c.PhoneNumber, cfg=> cfg.MapFrom(c=>c.Merchant.PhoneNumber))
                .ForMember(c=> c.UserId, cfg=> cfg.MapFrom(c=>c.Merchant.UserId));
        }
    }
}
