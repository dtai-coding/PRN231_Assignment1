using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IJewelryRepo
    {
        Task<List<SilverJewelry>> GetJewelries();

        Task<SilverJewelry> GetJewelry(string id);

        Task<SilverJewelry> AddSilverAsync(SilverJewelry silverJewelry);


        Task<SilverJewelry> UpdateSilverAsync(SilverJewelry silverJewelry);

        Task<SilverJewelry> DeleteJewelry(string id);


        Task<List<Category>> GetCategories();
    }
}
