using BookReadingTracker.UI.Models;
using Microsoft.Data.Sqlite;

namespace BookReadingTracker.UI.DataAccess;

public class BookRepository
{
    private readonly string? _connectionString;

        public BookRepository(string? connectionString)
        {
            _connectionString = connectionString;
        }

        /// Retrieves all books from the data source.
        /// <returns>
        /// A list of books, where each book includes details such as id, title, author, pages read, and total pages.
        /// </returns>
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM book_reading_tracker";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                books.Add(new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    PagesRead = reader.GetInt32(3),
                    TotalPages = reader.GetInt32(4)
                });
            }

            return books;
        }

        /// Retrieves details of a specific book identified by its ID.
        /// <param name="id">
        /// The unique identifier of the book to retrieve.
        /// </param>
        /// <returns>
        /// The book matching the provided ID, including details such as id, title, author, pages read, and total pages.
        /// Returns null if no book is found with the given ID.
        /// </returns>
        public Book? GetBookById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM book_reading_tracker WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    PagesRead = reader.GetInt32(3),
                    TotalPages = reader.GetInt32(4)
                };
            }

            return null;
        }

        /// Adds a new book to the data source.
        /// <param name="book">
        /// The book to be added, including details such as title, author, pages read, and total pages.
        /// </param>
        public void AddBook(Book book)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO book_reading_tracker (Title, Author, PagesRead, TotalPages)
                VALUES (@title, @author, @pagesRead, @totalPages)";
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@pagesRead", book.PagesRead);
            command.Parameters.AddWithValue("@totalPages", book.TotalPages);

            command.ExecuteNonQuery();
        }

        /// Updates the details of an existing book in the data source.
        /// <param name="book">
        /// The book object containing updated details such as id, title, author, pages read, and total pages.
        /// </param>
        public void UpdateBook(Book book)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE book_reading_tracker
                SET Title = @title, Author = @author,
                    PagesRead = @pagesRead, TotalPages = @totalPages
                WHERE Id = @id";
            command.Parameters.AddWithValue("@id", book.Id);
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@pagesRead", book.PagesRead);
            command.Parameters.AddWithValue("@totalPages", book.TotalPages);

            command.ExecuteNonQuery();
        }

        /// Deletes a book from the data source identified by its ID.
        /// <param name="id">
        /// The unique identifier of the book to be removed.
        /// </param>
        public void DeleteBook(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM book_reading_tracker WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

}