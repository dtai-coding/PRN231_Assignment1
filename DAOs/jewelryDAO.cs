using BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class jewelryDAO
    {
        private static jewelryDAO instance = null;
        private readonly SilverJewelry2023DbContext context;

        private jewelryDAO()
        {
            context = new SilverJewelry2023DbContext();
        }

        public static jewelryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new jewelryDAO();
                }
                return instance;
            }

        }

        public async Task<List<SilverJewelry>> GetJewelries()
        {
            var jewelry = await context.SilverJewelries.Include(c => c.Category).ToListAsync();
            return jewelry;
        }

        public async Task<SilverJewelry> GetJewelry(string id)
        {
            var jewelry = await context.SilverJewelries.Include(c => c.Category).FirstOrDefaultAsync(j => j.SilverJewelryId.Equals(id));
            return jewelry;
        }

        //private string GenerateId()
        //{
        //    var id = new Random().Next(0,999);
        //    return id.ToString();
        //}

        public async Task<SilverJewelry> AddSilverAsync(SilverJewelry silverJewelry)
        {
            try
            {
                context.SilverJewelries.AddAsync(silverJewelry);
                await context.SaveChangesAsync();
                return silverJewelry;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        public async Task<SilverJewelry> UpdateSilverAsync(SilverJewelry silverJewelry)
        {
            var oldJewelry = await context.SilverJewelries.FirstOrDefaultAsync(j => j.SilverJewelryId.Equals(silverJewelry.SilverJewelryId));
            if (oldJewelry == null)
            {
                throw new Exception("jewelry not found");
            }

            oldJewelry.SilverJewelryId = silverJewelry.SilverJewelryId;
            oldJewelry.SilverJewelryName = silverJewelry.SilverJewelryName;
            oldJewelry.SilverJewelryDescription = silverJewelry.SilverJewelryDescription;
            oldJewelry.MetalWeight = silverJewelry.MetalWeight;
            oldJewelry.Price = silverJewelry.Price;
            oldJewelry.ProductionYear = silverJewelry.ProductionYear;
            oldJewelry.CreatedDate = silverJewelry.CreatedDate;
            oldJewelry.CategoryId = silverJewelry.CategoryId;

            context.Update(oldJewelry);
            await context.SaveChangesAsync();
            return oldJewelry;
        }

        public async Task<SilverJewelry> DeleteJewelry(string id)
        {
            var jewelry = await context.SilverJewelries.FirstOrDefaultAsync(j => j.SilverJewelryId.Equals(id));
            if (jewelry == null)
            {
                throw new Exception("jewelry not found");
            }

            context.SilverJewelries.Remove(jewelry);
            await context.SaveChangesAsync();
            return jewelry;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }
    }
}
