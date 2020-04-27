using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Entities
{
    public class Stock
    {

        [Key]
        public long Id { get; set; }

        public long LocationId { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string SkuCode { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string Barcode { get; set; }

        public string LocationNumber { get; set; }

        public int Quantity { get; set; }

        public StockType StockType { get; set; }

        public DateTime LastMovementDate { get; set; }

       public string LocationLevel { get; set; }

    }
}
