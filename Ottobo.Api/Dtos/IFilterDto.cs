namespace Ottobo.Api.Dtos
{
    public interface IFilterDto
    {

        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; }

    }
}