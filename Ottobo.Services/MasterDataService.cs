using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Services
{
    public class MasterDataService : ServiceBase<MasterData>
    {
        public MasterDataService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties) 
            : base(logger, unitOfWork, includeProperties)
        {
        }

        public List<MasterData> Filter(string skuCode, string barcode, string skuName,
            string orderingField, DataSortType dataSortType, int page, int recordsPerPage)
        {
            IQueryable<MasterData> masterDatasQueryable = this.GetQueryable();


            if (skuCode != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.SkuCode.Contains(skuCode));
            }

            if (barcode != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.Barcode.Contains(barcode));
            }

            if (skuName != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.SkuName.Contains(skuName));
            }

            return this.Read(
                page,
                recordsPerPage,
                masterDatasQueryable,
                orderingField,
                dataSortType);
        }
    }
}