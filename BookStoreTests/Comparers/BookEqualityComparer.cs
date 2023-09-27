using BookStore.Models;
using System.Diagnostics.CodeAnalysis;

namespace BookStoreTests.Comparers
{
    public class BookEqualityComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            return x.Id == y.Id && x.Title == y.Title && x.Author.Id == y.Author.Id && x.Genre.Id == y.Genre.Id && x.Price == y.Price && x.QuantityAvailable == y.QuantityAvailable;
        }

        public int GetHashCode([DisallowNull] Book obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
