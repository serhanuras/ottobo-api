using AutoMapper;
using Ottobo.Api.DTOs;
using Ottobo.Entities;

namespace Ottobo.Api.Helpers
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

            CreateMap<StockType, StockTypeDTO>();
            CreateMap<StockTypeDTO, StockType>();
            CreateMap<StockType, StockTypeCreationDTO>().ReverseMap();
        }

        private void OrderTypeMappings()
        {

            CreateMap<OrderType, OrderTypeDTO>().ReverseMap();
            CreateMap<OrderType, OrderTypeCreationDTO>().ReverseMap();
        }

        private void StockMappings()
        {

            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<Stock, StockCreationDTO>().ReverseMap();
            CreateMap<Stock, StockFilterDTO>().ReverseMap();
            CreateMap<Stock, StockPatchDTO>().ReverseMap();

            //  CreateMap<Stock, StockDTO>()
            //     .ForMember(c => c.StockType, option => option.Ignore());
        }

        private void OrderMapping()
        {

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreationDTO>().ReverseMap();
            CreateMap<Order, OrderFilterDTO>().ReverseMap();
            CreateMap<Order, OrderPatchDTO>().ReverseMap();
        }

        private void OrderDetailMapping()
        {

            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreationDTO>().ReverseMap();
        }

        private void AccountMapping()
        {

            //CreateMap<ApplicationUser, UserDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>()
              .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
              .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));

        }

    }
}