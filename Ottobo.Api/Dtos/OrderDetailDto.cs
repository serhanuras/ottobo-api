using System;
using System.Collections;
using System.Collections.Generic;
using Ottobo.Entities;

namespace Ottobo.Api.Dtos
{
    public class OrderDetailDto: IDto
    {

        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public int PickedQuantity { get; set; }
        
        public string SkuCode { get; set; }
        
        public string SkuName { get; set; }
        
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

        public string PurchaseType { get; set; }
        
        public string ImageUrl { get; set; }

        //STOCK TYPE
        public string StockType { get; set; }
        
        //LOCATION 
        public string Location { get; set; }
        
        

    }
}
