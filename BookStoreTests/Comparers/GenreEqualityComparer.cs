using BookStore.Models;

namespace BookStoreTests.Comparers
{
    public class GenreEqualityComparer : IEqualityComparer<Genre>
    {
        public bool Equals(Genre x, Genre y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public int GetHashCode(Genre obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
