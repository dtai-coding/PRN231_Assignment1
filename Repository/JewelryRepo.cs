﻿using BOs;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JewelryRepo : IJewelryRepo
    {
        public async Task<SilverJewelry> AddSilverAsync(SilverJewelry silverJewelry)
        {
            return await jewelryDAO.Instance.AddSilverAsync(silverJewelry);
        }

        public async Task<SilverJewelry> DeleteJewelry(string id)
        {
            return await jewelryDAO.Instance.DeleteJewelry(id);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await jewelryDAO.Instance.GetCategories();
        }

        public async Task<List<SilverJewelry>> GetJewelries()
        {
            return await jewelryDAO.Instance.GetJewelries();
        }

        public async Task<SilverJewelry> GetJewelry(string id)
        {
            return await jewelryDAO.Instance.GetJewelry(id);
        }

        public async Task<SilverJewelry> UpdateSilverAsync(SilverJewelry silverJewelry)
        {
            return await jewelryDAO.Instance.UpdateSilverAsync(silverJewelry);
        }
    }
}