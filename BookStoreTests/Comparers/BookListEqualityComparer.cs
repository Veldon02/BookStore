using BookStore.Models;
using System.Diagnostics.CodeAnalysis;

namespace BookStoreTests.Comparers
{
    public class BookListEqualityComparer : IEqualityComparer<List<Book>>
    {
        public bool Equals(List<Book> x, List<Book> y)
        {
            return x.Count == y.Count && x.All(y.Contains);
        }

        public int GetHashCode([DisallowNull] List<Book> obj)
        {
            return obj.GetHashCode();
        }
    }
}
