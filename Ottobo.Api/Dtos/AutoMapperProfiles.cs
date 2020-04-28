using AutoMapper;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            StockTypeMappings();
            OrderTypeMappings();
            StockMappings();
            OrderMapping();
            OrderDetailMapping();
            AccountMapping();

        }

        private void StockTypeMappings()
        {

            CreateMap<StockType, StockTypeDto>().ReverseMap();
            CreateMap<StockType, StockTypeCreationDto>().ReverseMap();
            CreateMap<StockType, StockTypeFilterDto>().ReverseMap();
            CreateMap<StockType, StockTypePatchDto>().ReverseMap();
        }

        private void OrderTypeMappings()
        {

            CreateMap<OrderType, OrderTypeDto>().ReverseMap();
            CreateMap<OrderType, OrderTypeCreationDto>().ReverseMap();
            CreateMap<OrderType, OrderTypeFilterDto>().ReverseMap();
            CreateMap<OrderType, OrderTypePatchDto>().ReverseMap();
        }

        private void StockMappings()
        {

            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<Stock, StockCreationDto>().ReverseMap();
            CreateMap<Stock, StockFilterDto>().ReverseMap();
            CreateMap<Stock, StockPatchDto>().ReverseMap();
            
        }

        private void OrderMapping()
        {

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreationDto>().ReverseMap();
            CreateMap<Order, OrderFilterDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }

        private void OrderDetailMapping()
        {

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreationDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailFilterDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailPatchDto>().ReverseMap();
        }

        private void AccountMapping()
        {

            //CreateMap<ApplicationUser, UserDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDto>()
              .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
              .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));

        }

    }
}