using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using Newtonsoft.Json;
using System.Text;
using PRN231PE_FA23_665511_taipdse172357_fe.DTO;

namespace PRN231PE_FA23_665511_taipdse172357_fe.Pages.SilverJewelryPages
{
    public class EditModel : PageModel
    {
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        [BindProperty]
        public SilverJewelryDTO Jewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"https://localhost:7034/api/Category");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(content);
                }

                var fillData = await httpClient.GetAsync($"https://localhost:7034/api/SilverJewelries/{id}");

                if (fillData.IsSuccessStatusCode)
                {
                    var content = await fillData.Content.ReadAsStringAsync();
                    Jewelry = JsonConvert.DeserializeObject<SilverJewelryDTO>(content);
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(Jewelry);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"https://localhost:7034/api/SilverJewelries/{Jewelry.SilverJewelryId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    TempData["Message"] = "Update successfully!";
                    return RedirectToPage("/SilverJewelryPages/Index");
                }
                else
                {
                    TempData["Message"] = "Update failed!";
                    return Page();
                }
            }


        }
    }
}
