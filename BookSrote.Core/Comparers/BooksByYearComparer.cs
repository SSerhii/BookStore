using System.Collections.Generic;
using BookStore.Core.Model;

namespace BookStore.Core.Comparers
{
    class BooksByYearComparer : IComparer<Book>
    {
        public int Compare(Book firstBook, Book secondBook)
        {
            var comparisonResult = firstBook.Year.CompareTo(secondBook.Year);

            if (comparisonResult != 0)
                return comparisonResult;

            return string.Compare(firstBook.CreateSortingByTitleString(), secondBook.CreateSortingByTitleString());
        }
    }
}
