using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;
using PRN231PE_FA23_665511_taipdse172357_fe.DTO;
using Newtonsoft.Json;
using System.Text;

namespace PRN231PE_FA23_665511_taipdse172357_fe.Pages.SilverJewelryPages
{
    public class CreateModel : PageModel
    {
        public IList<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public async Task<IActionResult> OnGet()
        {
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

                return Page();
            }
        }

        [BindProperty]
        public SilverJewelryDTO Jewelry { get; set; } = default!;

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

                var response = await httpClient.PostAsync("https://localhost:7034/api/SilverJewelries", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Create successfully!";
                    return RedirectToPage("/SilverJewelryPages/Index");
                }
                else
                {
                    TempData["Message"] = "Create failed!";
                    return await OnGet();
                }

            }
        }
    }
}
