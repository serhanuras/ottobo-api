using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ottobo.Entities
{
    public class PurchaseType:EntityBase
    {

        public string Name { get; set; }
        
        public List<MasterData> MasterDataList { get; set; }


    }
}
