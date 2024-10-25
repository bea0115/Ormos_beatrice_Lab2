using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ormos_beatrice_Lab2.Data;
using Ormos_beatrice_Lab2.Models;

namespace Ormos_beatrice_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Ormos_beatrice_Lab2.Data.Ormos_beatrice_Lab2Context _context;

        public DetailsModel(Ormos_beatrice_Lab2.Data.Ormos_beatrice_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                 .Include(b => b.BookCategories) 
                     .ThenInclude(bc => bc.Category) 
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
