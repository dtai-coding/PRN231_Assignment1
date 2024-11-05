using BOs;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JewelryService : IJewelryService
    {
        private readonly IJewelryRepo _jewelryRepo;
        public JewelryService(IJewelryRepo jewelryRepo)
        {
            _jewelryRepo = jewelryRepo;
        }

        public async Task<SilverJewelry> AddSilverAsync(SilverJewelry silverJewelry)
        {
            return await _jewelryRepo.AddSilverAsync(silverJewelry);
        }

        public async Task<SilverJewelry> DeleteJewelry(string id)
        {
            return await _jewelryRepo.DeleteJewelry(id);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _jewelryRepo.GetCategories();
        }

        public async Task<List<SilverJewelry>> GetJewelries()
        {
            return await _jewelryRepo.GetJewelries();
        }

        public async Task<SilverJewelry> GetJewelry(string id)
        {
            return await _jewelryRepo.GetJewelry(id);
        }

        public async Task<SilverJewelry> UpdateSilverAsync(SilverJewelry silverJewelry)
        {
            return await _jewelryRepo.UpdateSilverAsync(silverJewelry);
        }
    }
}
