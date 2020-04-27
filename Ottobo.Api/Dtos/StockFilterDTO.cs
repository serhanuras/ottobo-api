

using System;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class StockFilterDto
    {

        public int Page { get; set; } = 1;
        
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDto Pagination
        {
            get { return new PaginationDto() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }

        public long LocationId { get; set; }

        public string SkuCode { get; set; }

        public string Barcode { get; set; }

        public string LocationNumber { get; set; }

        public int StockTypeId { get; set; }

        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}
