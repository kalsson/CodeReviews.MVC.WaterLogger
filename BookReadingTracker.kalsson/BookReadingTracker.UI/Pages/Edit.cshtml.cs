using BookReadingTracker.UI.DataAccess;
using BookReadingTracker.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookReadingTracker.UI.Pages
{
    public class EditModel : PageModel
    {
        private readonly BookRepository _bookRepository;

        public EditModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            try
            {
                Book = _bookRepository.GetBookById(id);

                if (Book == null)
                {
                    return RedirectToPage("/Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _bookRepository.UpdateBook(Book);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                ModelState.AddModelError(string.Empty, "An error occurred while updating the book. Please try again.");

                return Page();
            }
        }
    }
}
