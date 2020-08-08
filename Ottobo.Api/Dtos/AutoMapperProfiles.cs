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
        }

        private void MasterDataMapping()
        {
            CreateMap<MasterData, MasterDataDto>()
                .ForMember(l => l.PurchaseType, options => options.MapFrom(x => x.PurchaseType.Name))
                .ReverseMap();
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
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.OrderType, options => options.MapFrom(x => x.OrderType.Name))
                .ReverseMap();
            CreateMap<Order, OrderCreationDto>().ReverseMap();
            CreateMap<Order, OrderFilterDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }

        private void OrderDetailMapping()
        {
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(orderDetailDto => orderDetailDto.SkuCode,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.SkuCode))
                .ForMember(orderDetailDto => orderDetailDto.SkuName,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.SkuName))
                .ForMember(orderDetailDto => orderDetailDto.Barcode,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.Barcode))
                .ForMember(orderDetailDto => orderDetailDto.UnitPack,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.UnitPack))
                .ForMember(orderDetailDto => orderDetailDto.UnitCase,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.UnitCase))
                .ForMember(orderDetailDto => orderDetailDto.UnitPalet,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.UnitPalet))
                .ForMember(orderDetailDto => orderDetailDto.IsPackaged,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.IsPackaged))
                .ForMember(orderDetailDto => orderDetailDto.IsCased,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.IsCased))
                .ForMember(orderDetailDto => orderDetailDto.CaseWidth,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.CaseWidth))
                .ForMember(orderDetailDto => orderDetailDto.CaseHeight,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.CaseHeight))
                .ForMember(orderDetailDto => orderDetailDto.CaseDepth,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.CaseDepth))
                .ForMember(orderDetailDto => orderDetailDto.CaseM3,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.CaseM3))
                .ForMember(orderDetailDto => orderDetailDto.IsSignedOn,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.IsSignedOn))
                .ForMember(orderDetailDto => orderDetailDto.PackageHeight,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.PackageHeight))
                .ForMember(orderDetailDto => orderDetailDto.PurchaseType,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.PurchaseType))
                .ForMember(orderDetailDto => orderDetailDto.ImageUrl,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.MasterData.ImageUrl))
                .ForMember(orderDetailDto => orderDetailDto.StockType,
                    options => options.MapFrom(orderDetail => orderDetail.Stock.StockType.Name))
                .ForMember(orderDetailDto => orderDetailDto.Location,
                    options => options.MapFrom(orderDetail =>
                        $"{orderDetail.Stock.Location.Name} [x:{orderDetail.Stock.Location.XCoordinate},y:{orderDetail.Stock.Location.YCoordinate}]"));

            CreateMap<OrderDetail, OrderDetailCreationDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailFilterDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailPatchDto>().ReverseMap();
            CreateMap<OrderDetail, LocationDto>()
                .ForMember(l => l.Id, options => options.MapFrom(x => x.Stock.Location.Id))
                .ForMember(l => l.Name, options => options.MapFrom(x => x.Stock.Location.Name))
                .ForMember(l => l.Theate, options => options.MapFrom(x => x.Stock.Location.Theate))
                .ForMember(l => l.MapId, options => options.MapFrom(x => x.Stock.Location.MapId))
                .ForMember(l => l.XCoordinate, options => options.MapFrom(x => x.Stock.Location.XCoordinate))
                .ForMember(l => l.YCoordinate, options => options.MapFrom(x => x.Stock.Location.YCoordinate));

            CreateMap<OrderDetail, RobotOrderDetailDto>()
                .ForMember(r => r.Barcode, options => options.MapFrom(o => o.Stock.MasterData.Barcode))
                .ForMember(r => r.BasketId, options => options.MapFrom(o => o.BasketId))
                .ForMember(r => r.ImageUrl, options => options.MapFrom(o => o.Stock.MasterData.ImageUrl))
                .ForMember(r => r.OrderCode, options => options.MapFrom(o => o.Stock.MasterData.SkuCode))
                .ForMember(r => r.OrderName, options => options.MapFrom(o => o.Stock.MasterData.SkuName))
                .ForMember(r => r.OrderId, options => options.MapFrom(o => o.Id))
                .ForMember(r => r.Quantity, options => options.MapFrom(o => o.Quantity))
                .ForMember(r => r.TrackId, options => options.MapFrom(o => o.BasketId));
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
    }
}