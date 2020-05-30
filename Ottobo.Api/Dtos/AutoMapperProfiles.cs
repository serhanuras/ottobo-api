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
            PurchaseTypeMapping();
            MasterDataMapping();
            LocationMapping();
            RobotMapping();
            RobotTaskMapping();
            TaskOrderMapping();

        }
        
        private void MasterDataMapping()
        {

            CreateMap<MasterData, MasterDataDto>().ReverseMap();
            CreateMap<MasterData, MasterDataCreationDto>().ReverseMap();
            CreateMap<MasterData, MasterDataFilterDto>().ReverseMap();
            CreateMap<MasterData, MasterDataPatchDto>().ReverseMap();
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
        
        private void PurchaseTypeMapping()
        {

            CreateMap<PurchaseType, PurchaseTypeDto>().ReverseMap();
            CreateMap<PurchaseType, PurchaseTypeCreationDto>().ReverseMap();
            CreateMap<PurchaseType, PurchaseTypeFilterDto>().ReverseMap();
            CreateMap<PurchaseType, PurchaseTypePatchDto>().ReverseMap();
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

        private void LocationMapping()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationCreationDto>().ReverseMap();
            CreateMap<Location, LocationFilterDto>().ReverseMap();
            CreateMap<Location, LocationPatchDto>().ReverseMap();
        }
        
        private void RobotMapping()
        {
            CreateMap<Robot, RobotDto>().ReverseMap();
            CreateMap<Robot, RobotCreationDto>().ReverseMap();
            CreateMap<Robot, RobotFilterDto>().ReverseMap();
            CreateMap<Robot, RobotPatchDto>().ReverseMap();
        }
        
        private void RobotTaskMapping()
        {
            CreateMap<RobotTask, RobotTaskDto>().ReverseMap();
            CreateMap<RobotTask, RobotTaskCreationDto>().ReverseMap();
            CreateMap<RobotTask, RobotTaskFilterDto>().ReverseMap();
            CreateMap<RobotTask, RobotTaskPatchDto>().ReverseMap();
        }
        
        private void TaskOrderMapping()
        {
            CreateMap<TaskOrder, TaskOrderDto>().ReverseMap();
            CreateMap<TaskOrder, TaskOrderCreationDto>().ReverseMap();
            CreateMap<TaskOrder, TaskOrderFilterDto>().ReverseMap();
            CreateMap<TaskOrder, TaskOrderPatchDto>().ReverseMap();
        }

    }
}