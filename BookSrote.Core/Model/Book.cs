using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookStore.Core.Model
{
    class Book
    {
        [JsonConstructor]
        public Book(string title, string author, string editor, int year, List<string> pages)
        {
            Title = title;
            Author = author;
            Editor = editor;
            Year = year;
            Pages = pages;
        }

        public string Title { get; }

        public string Author { get; }

        public string Editor { get; }

        public int Year { get; }

        public List<string> Pages { get; }

        public override string ToString()
        {
            return string.Format($"{Title} by {Author} ({Editor}, {Year})");
        }
    }
}
