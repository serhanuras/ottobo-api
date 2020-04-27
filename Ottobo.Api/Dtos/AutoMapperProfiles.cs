using AutoMapper;
using Ottobo.Api.Dtos;
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

            CreateMap<StockType, StockTypeDto>();
            CreateMap<StockTypeDto, StockType>();
            CreateMap<StockType, StockTypeCreationDto>().ReverseMap();
        }

        private void OrderTypeMappings()
        {

            CreateMap<OrderType, OrderTypeDto>().ReverseMap();
            CreateMap<OrderType, OrderTypeCreationDto>().ReverseMap();
        }

        private void StockMappings()
        {

            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<Stock, StockCreationDto>().ReverseMap();
            CreateMap<Stock, StockFilterDto>().ReverseMap();
            CreateMap<Stock, StockPatchDto>().ReverseMap();

            //  CreateMap<Stock, StockDTO>()
            //     .ForMember(c => c.StockType, option => option.Ignore());
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