using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class OrderDetailService:ServiceBase<OrderDetail>
    {
        
        
        public OrderDetailService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties) 
            : base(logger, unitOfWork, includeProperties)
        {
        }
        
        public List<OrderDetail> GetByTaskId(Guid robotTaskId)
        {
            return  this.Read(x => x.RobotTaskId == robotTaskId);
        } 
        
        public List<OrderDetail> GetByLocationId(Guid robotTaskId, Guid locationId)
        {
            return  this.Read(x => x.RobotTaskId == robotTaskId && x.Stock.LocationId == locationId);
        }  
        
       
        
        
    }
}