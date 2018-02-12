using System;
using System.Drawing;
using System.Windows.Forms;
using BookStore.Core.ViewModel;
using BookStore.Core;
using System.ComponentModel;

namespace BookStore.WindowsForms
{
    public partial class BookStoreView : Form
    {
        private BookStoreViewModel _viewModel;

        public BookStoreView(BookStoreViewModel viewModel)
        {
            InitializeComponent();
            this.Enabled = false;
            this.BackColor = Color.Pink;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _viewModel.Initialize();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _viewModel.DownloadSelectedBook();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.SelectedBooksSortIndex = comboBox1.SelectedIndex;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.SelectedBookIndex = comboBox2.SelectedIndex;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.SortedBookSetted):
                    OnSortedbookSetted();
                    break;
                case nameof(_viewModel.SelectedBooksCollection):
                    OnSelectedBooksCollectionChanged();
                    break;
             }

        }

        private void OnSelectedBooksCollectionChanged()
        {
            
            this.Enabled = false;
            this.BackColor = Color.Pink;
            _viewModel.SelectedBookIndex = -1;
            comboBox2.Items.Clear();
            foreach (string s in _viewModel.SelectedBooksCollection)
                comboBox2.Items.Add(s);
            this.BackColor = SystemColors.Control;
            this.Enabled = true;
        }

        private void OnSortedbookSetted()
        {
            comboBox1.Items.AddRange(_viewModel.BookSortNames.ToArray());

            this.BackColor = SystemColors.Control;
            this.Enabled = true;
        }
    }
}
