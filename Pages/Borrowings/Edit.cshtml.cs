using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ormos_beatrice_Lab2.Data;
using Ormos_beatrice_Lab2.Models;

namespace Ormos_beatrice_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Ormos_beatrice_Lab2.Data.Ormos_beatrice_Lab2Context _context;

        public EditModel(Ormos_beatrice_Lab2.Data.Ormos_beatrice_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Borrowing = _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                    .ThenInclude(b => b.Author)
                .FirstOrDefault(m => m.ID == id);

            if (Borrowing == null)
            {
                return NotFound();
            }

            // Populăm ViewBag pentru a afișa numele membrului și detalii despre carte
            var memberList = _context.Member.Select(x => new
            {
                x.ID,
                FullName = x.FirstName + " " + x.LastName
            }).ToList();

            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    BookDetails = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
                }).ToList();

            ViewData["MemberID"] = new SelectList(memberList, "ID", "FullName", Borrowing.MemberID);
            ViewData["BookID"] = new SelectList(bookList, "ID", "BookDetails", Borrowing.BookID);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
