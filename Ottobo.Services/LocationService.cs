using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class LocationService : ServiceBase<Location>
    {
        private readonly OrderDetailService _orderDetailService;

        public LocationService(ILogger logger, IUnitOfWork unitOfWork, OrderDetailService orderDetailService, string includeProperties) : base(
            logger, unitOfWork, includeProperties)
        {
            _orderDetailService = orderDetailService;
           
        }
        
        
        
        
        public List<Location> GetLocationsByTaskId(Guid robotTaskId)
        {
            List<OrderDetail> orderDetails =
                _orderDetailService.GetByTaskId(robotTaskId);
            
            
            var locations = new List<Location>();

            foreach (var orderDetail in orderDetails)
            {
                if (!locations.Contains(orderDetail.Stock.Location))
                    locations.Add(orderDetail.Stock.Location);
            }
            
            locations = locations.GroupBy(o => new { o.Id })
                .Select(o => o.FirstOrDefault()).OrderBy(x => x.CreatedOn).ToList();

            return locations;

        }  
        
        


        public Location GetNextLocation(Guid robotTaskId, Guid? currentLocationId)
        {

            var locations = this.GetLocationsByTaskId(robotTaskId);
                
            Location nextLocation = null;
            
            if (currentLocationId == null)
            {
                nextLocation = locations.FirstOrDefault();
            }

            if (nextLocation == null)
            {
                bool isCurrentLocationFound = false;


                foreach (var location in locations)
                {
                    if (isCurrentLocationFound)
                    {
                        nextLocation =location;
                        break;
                    }

                    if (location.Id == currentLocationId)
                    {
                        isCurrentLocationFound = true;
                    }
                }
            }

            return nextLocation;
        }
    }
    
    
}