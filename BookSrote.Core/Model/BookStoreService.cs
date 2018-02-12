using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace BookStore.Core.Model
{
    class BookStoreService
    {
        public event EventHandler SortedBooksSetted;

        public BookStoreService()
        {
            CreateJsonBooksFile(CreateBooksCollection());
        }

        private List<Book> _booksCollection;
        public List<Book> BooksCollection
        {
            get
            {
                return _booksCollection;
            }
            set
            {
                _booksCollection = value;
                InvokeSortedBooksSetted();
            }
        }

        private SortedBooks _sortedBooks;
        public SortedBooks SortedBooks
        {
            get => _sortedBooks;
            private set
            {
                _sortedBooks = value;
            }
        }

        private List<Book> CreateBooksCollection()
        {
            var booksCollection = new List<Book>();
            var rnd = new Random();
            var autorsNameCollection = new List<string> { "Jacob", "Michael", "Joshua", "Matthew", "Christopher", "Andrew", "Daniel",
                "Ethan", "Joseph", "William", "Anthony", "Nicholas", "David", "Alexander", "Ryan", "Tyler", "James",
                "John", "Jonathan", "Brandon", "Christian", "Dylan", "Zachary", "Noah", "Samuel", "Benjamin", "Nathan" };
            var booksTitleCollection = new List<string> { "Things Fall Apart", "Night", "Elie Wiesel", "The Kite Runner", "The Stranger",
                "Cry, the Beloved Country", "The Metamorphosis", "Siddhartha", "The Joy Luck Club", "Oedipus the King",
                "The Odyssey", "A Doll’s House", "All Quiet on the Western Front", "One Hundred Years of Solitude",
                "Candide", "Antigone", "Crime and Punishment", "The Inferno", "The Iliad", "Life of Pi", "Cyrano de Bergerac",
                "Les Miserables", "Don Quixote", "Like Water for Chocolate", "Bless Me, Ultima", "Madame Bovary",
                "Mythology", "Hiroshima", "The Count of Monte Cristo", "Thousand Splendid Suns", "The Poisonwood Bible" };
            var pagesCollection = new List<string>
            { "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkfg",
                 "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                  "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                   "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                    "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                     "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                      "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                       "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                        "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                         "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                          "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                           "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                            "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh",
                             "hfasdfghjkloiuytrewsdwertyuiopdfghjkxcvbndfghjerghjkghjkkjh" };

            for (int i = 0; i < 18; i++)
            {
                booksCollection.Add(new Book(booksTitleCollection[rnd.Next(booksTitleCollection.Count)],
                                                autorsNameCollection[rnd.Next(autorsNameCollection.Count)],
                                                string.Format("Editor{0}", i),
                                                rnd.Next(DateTime.Now.Year),
                                                pagesCollection));
            }
            return booksCollection;
        }

        private void CreateJsonBooksFile(List<Book> booksCollection)
        {
            if (GlobalDependencies.DataController.FileExists("Books.json"))
                GlobalDependencies.DataController.DeleteFile("Books.json");

            using (var stream = GlobalDependencies.DataController.GetStream("Books.json"))
            using (var sw = new StreamWriter(stream))
            {
                var text = JsonConvert.SerializeObject(booksCollection);
                sw.Write(text);
            }
        }

        public async Task GetBooksCollection()
        {
            using(var stream = GlobalDependencies.DataController.GetStream("Books.json"))
            using (var jsonFile = new StreamReader(stream))
            {
                await Task.Delay(5000);
                BooksCollection = JsonConvert.DeserializeObject<List<Book>>(await jsonFile.ReadToEndAsync());
            }
        }

        public void InitializeSortedBooks()
        {
            SortedBooks = new SortedBooks(BooksCollection);
        }

        private void InvokeSortedBooksSetted()
        {
            var sortetBooksSetted = SortedBooksSetted;
            if (sortetBooksSetted != null)
                sortetBooksSetted.Invoke(this, null);
        }

        private Task SaveBookAsync(Book book, Stream stream)
        {
            return Task.Run(() =>
            {
                using (var bookFile = new StreamWriter(stream))
                {
                    var bookMetaData = string.Join(Environment.NewLine,
                                                        $"Title of the book: {book.Title}",
                                                        $"Author of the book: {book.Author}",
                                                        $"Book publishe: {book.Editor}",
                                                        $"Year of publication: {book.Year}\n");
                    bookFile.WriteLine(bookMetaData);
                    foreach (string s in book.Pages)
                        bookFile.WriteLine(s);
                }
            });
        }

        public List<string> GetBookSortNames()
        {
            if (SortedBooks != null)
                return SortedBooks.SortingNames;
            return null;
        }

        public List<string> GetSelectedBooksCollection(int selectedSortIndex)
        {
            var selectedBooksCollection = from b in SortedBooks.BookSortedSets.ElementAt(selectedSortIndex)
                                          select b.ToString();
            return selectedBooksCollection.ToList();
        }

        public Book GetBookFromSortedBooks(int selectedSortIndex, int selectedBookIndex)
        {
            return SortedBooks.BookSortedSets.ElementAt(selectedSortIndex).ElementAt(selectedBookIndex);
        }

        private string GetLegalTxtFileName(string primaryFileName)
        {
            return new string(primaryFileName.ToCharArray().Where(x => !Path.GetInvalidPathChars().Contains(x)).ToArray());
        }

        public async Task DownloadSelectedBook(int selectedSortIndex, int selectedBookIndex)
        {
            var book = GetBookFromSortedBooks(selectedSortIndex, selectedBookIndex);
            var primaryTxtFileName = book.GetPrimaryTxtFileName();

            if (!GlobalDependencies.DataController.DirectoryExists("Downloads"))
            {
                GlobalDependencies.DataController.CreateFolder("Downloads");
            }

            if (GlobalDependencies.DataController.FileExists(Path.Combine("Downloads", GetLegalTxtFileName(primaryTxtFileName))))
            {
                await GlobalDependencies.UserInteraction.ShowMessageAsync("File already exists");
                return;
            }
            var stream = GlobalDependencies.DataController.GetStream(Path.Combine("Downloads", GetLegalTxtFileName(primaryTxtFileName)));
            await SaveBookAsync(book, stream);
            await GlobalDependencies.UserInteraction.ShowMessageAsync(
                string.Format($"Book \"{book.ToString()}\" was loaded to \n {GetLegalTxtFileName(primaryTxtFileName)}"));
        }
    }
}
