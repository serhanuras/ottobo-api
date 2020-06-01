using System;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Dtos 
{
    public class MasterDataCreationDto : ICreationDto
    {
        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string SkuCode { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string SkuName { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        [StringLength(100)]
        public string Barcode { get; set; }

        public int UnitPack { get; set; }

        public int UnitCase { get; set; }

        public int UnitPalet { get; set; }

        public bool IsPackaged { get; set; }

        public bool IsCased { get; set; }

        public decimal CaseWidth { get; set; }

        public decimal CaseHeight { get; set; }

        public decimal CaseDepth { get; set; }

        public decimal CaseM3 { get; set; }

        public bool IsSignedOn { get; set; }

        public decimal PackageHeight { get; set; }
        
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The field with name {0} is required.")]
        public Guid PurchaseTypeId { get; set; }
       
    }
}