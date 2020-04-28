using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Ottobo.Helpers;

namespace Ottobo.Api.Dtos
{
    public class StockPatchDto : IPatchDto
    {

        public long LocationId { get; set; }

        public string SkuCode { get; set; }
        public string Barcode { get; set; }

        public string LocationNumber { get; set; }

        public int Quantity { get; set; }

        public int StockTypeId { get; set; }

        public DateTime LastMovementDate { get; set; }

       public string LocationLevel { get; set; }

    }
}