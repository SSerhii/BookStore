using System;
using System.Collections.Generic;
using BookStore.Core.Model;
using BookStore.Core.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStore.Core.ViewModel
{
    public class BookStoreViewModel : BaseViewModel, IDisposable
    {
        private BookStoreService _bookStoreService;

        public BookStoreViewModel()
        {
            _selectedBooksSortIndex = -1;
            _selectedBookIndex = -1;
            _bookStoreService = new BookStoreService();
            DownloadSelectedBookCommand = new DelegateCommand(() => DownloadSelectedBook());
            _bookStoreService.SortedBooksSetted += OnSortedBooksSetted;
        }

        public DelegateCommand DownloadSelectedBookCommand { get; } 

        private bool _sortedBookSetted = false;
        public bool SortedBookSetted
        {
            get => _sortedBookSetted;
            private set
            {
                BookSortNames = GetBookSortNames();
                SetProperty(ref _sortedBookSetted, value);
            }
        }

        private int _selectedBooksSortIndex;
        public int SelectedBooksSortIndex
        {
            get => _selectedBooksSortIndex;
            set
            {
                if (value != _selectedBooksSortIndex)
                    SelectedBooksCollection = GetSelectedBooksCollection(value);
                SetProperty(ref _selectedBooksSortIndex, value);
            }
        }

        private int _selectedBookIndex;
        public int SelectedBookIndex
        {
            get => _selectedBookIndex;
            set => _selectedBookIndex = value;
        }

        private List<string> _bookSortNames;
        public List<string> BookSortNames
        {
            get => _bookSortNames;
            private set => SetProperty(ref _bookSortNames, value);
        }

        private List<string> _selectedBooksCollection;
        public List<string> SelectedBooksCollection
        {
            get => _selectedBooksCollection;
            private set => SetProperty(ref _selectedBooksCollection, value);
        }

        public Task Initialize()
        {
            return _bookStoreService.GetBooksCollection();
        }

        public void OnSortedBooksSetted(object sendler, EventArgs e)
        {
            _bookStoreService.InitializeSortedBooks();
            SortedBookSetted = true;
        }

        public List<string> GetBookSortNames()
        {
            return _bookStoreService.GetBookSortNames();
        }

        public List<string> GetSelectedBooksCollection(int selectedBooksSortIndex)
        {
            return _bookStoreService.GetSelectedBooksCollection(selectedBooksSortIndex);
        }



        public Task DownloadSelectedBook()
        {
            if (_selectedBookIndex == -1)
                return GlobalDependencies.UserInteraction.ShowMessageAsync("Book is not selected");
            else
            return _bookStoreService.DownloadSelectedBook(_selectedBooksSortIndex, _selectedBookIndex);
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
                return;

            _bookStoreService.SortedBooksSetted -= OnSortedBooksSetted;

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
