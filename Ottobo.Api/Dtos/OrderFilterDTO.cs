

using System;
using System.ComponentModel.DataAnnotations;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderFilterDto
    {

        public int Page { get; set; } = 1;
        
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDto Pagination
        {
            get { return new PaginationDto() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }


        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }


        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;

    }
}
