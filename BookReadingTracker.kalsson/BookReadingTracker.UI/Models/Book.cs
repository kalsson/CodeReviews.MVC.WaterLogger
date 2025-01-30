using System.ComponentModel.DataAnnotations;

namespace BookReadingTracker.UI.Models;

public class Book
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required.")]
    [StringLength(50, ErrorMessage = "Author name must not exceed 50 characters.")]
    public string Author { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Pages Read must be zero or a positive number.")]
    public int PagesRead { get; set; }

    [Required(ErrorMessage = "Total Pages is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Pages must be greater than 0.")]
    public int TotalPages { get; set; }

}