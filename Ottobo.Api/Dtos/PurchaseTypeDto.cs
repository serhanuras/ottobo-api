using System;

namespace Ottobo.Api.Dtos
{
    public class PurchaseTypeDto : IDto
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
