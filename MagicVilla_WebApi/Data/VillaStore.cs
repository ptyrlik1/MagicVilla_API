using MagicVilla_WebApi.Models.DTO;

namespace MagicVilla_WebApi.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> vilaList = new List<VillaDTO>{
            
            new VillaDTO {Id=1,Name="Pool View",Sqft=100,Occupancy =4},
            new VillaDTO {Id=2,Name="Beach View",Sqft=110,Occupancy=3}
            };
        
    }
}
