namespace BookStore.Core.Model
{
    static class BookExtensions
    {
        public static string CreateSortingByAuthorString(this Book book)
        {
            return string.Format($"{book.Author} {book.Title}");
        }

        public static string CreateSortingByTitleString(this Book book)
        {
            return string.Format($"{book.Title} {book.Author}");
        }

        public static string GetPrimaryTxtFileName(this Book book)
        {
            return string.Format($"{book.Title} by {book.Author}.txt");
        }
    }
}
