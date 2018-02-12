using System.Collections.Generic;
using BookStore.Core.Comparers;

namespace BookStore.Core.Model
{
    class SortedBooks
    {
        private readonly List<Book> _booksCollection;

        public SortedBooks(List<Book> booksCollection)
        {
            _booksCollection = booksCollection;
            SortingNames = new List<string> { "Books Sorted By Author", "Books Sorted By Title", "Books Sorted By Years" };
            BookSortedSets = new List<SortedSet<Book>>
            {
                new SortedSet<Book>(_booksCollection, new BooksByAuthorComparer()),
                new SortedSet<Book>(_booksCollection, new BooksByTitleComparer()),
                new SortedSet<Book>(_booksCollection, new BooksByYearComparer())
            };
        }
        private List<string> _sortingNames;
        public List<string> SortingNames
        {
            get
            {
                return _sortingNames;
            }
            private set
            {
                _sortingNames = value;
            }
        }

        private List<SortedSet<Book>> _bookSortedSets;
        public List<SortedSet<Book>> BookSortedSets
        {
            get
            {
                return _bookSortedSets;
            }
            private set
            {
                _bookSortedSets = value;
            }
        }
    }
}
