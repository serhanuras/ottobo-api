using Microsoft.AspNetCore.Mvc;

namespace Ottobo.Api.Dtos
{
    public class MasterDataFilterDto : IFilterDto
    {
     
        public string SkuCode { get; set; }
        
        public string SkuName { get; set; }
        
        public string Barcode { get; set; }
        
        public string OrderingField { get; set; }
        
        public bool AscendingOrder { get; set; } = true;
    }
}