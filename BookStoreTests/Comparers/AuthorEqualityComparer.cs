using BookStore.Models;

namespace BookStoreTests.Comparers
{
    public class AuthorEqualityComparer : IEqualityComparer<Author>
    {
        public bool Equals(Author x, Author y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && AreBookListsEqual(x.Books, y.Books);
        }

        public int GetHashCode(Author obj)
        {
            return obj.Id.GetHashCode();
        }

        private bool AreBookListsEqual(ICollection<Book> list1, ICollection<Book> list2)
        {
            if (list1 == null && list2 == null) return true;
            if (list1 == null || list2 == null) return false;
            if (list1.Count != list2.Count) return false;

            return list1.All(book1 => list2.Any(book2 => new BookEqualityComparer().Equals(book1, book2)));
        }
    }
}
