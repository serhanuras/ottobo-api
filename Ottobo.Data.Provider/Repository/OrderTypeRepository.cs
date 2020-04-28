using Microsoft.EntityFrameworkCore;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Data.Provider.Repository
{
    public class OrderTypeRepository:Repository<OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(DbContext context) : base(context)
        {
        }
    }
}