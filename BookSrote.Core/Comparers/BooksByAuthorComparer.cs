using System;
using System.Collections.Generic;
using BookStore.Core.Model;

namespace BookStore.Core.Comparers
{
    class BooksByAuthorComparer : IComparer<Book>
    {
        public int Compare(Book firstBook, Book secondBook)
        {
            var comparisonResult = string.Compare(firstBook.CreateSortingByAuthorString(), secondBook.CreateSortingByAuthorString());

            if (comparisonResult != 0)
                return comparisonResult;

            return firstBook.Year.CompareTo(secondBook.Year);
        }
    }
}
