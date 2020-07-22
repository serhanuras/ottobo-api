using System;
using Newtonsoft.Json;

namespace Ottobo.Api.Dtos
{
    public class RobotOrderDetailDto
    {
        [JsonProperty(PropertyName = "orderId")]
        public Guid OrderId { get; set; }

        [JsonProperty(PropertyName = "basketId")]
        public int BasketId { get; set; }
        
        [JsonProperty(PropertyName = "orderCode")]
        public string OrderCode { get; set; }
        
        [JsonProperty(PropertyName = "orderName")]
        public string OrderName { get; set; }
        
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        
        [JsonProperty(PropertyName = "barcode")]
        public string Barcode { get; set; }
        
        [JsonProperty(PropertyName = "trackId")]
        public int TrackId { get; set; }
        
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
       
    }
}