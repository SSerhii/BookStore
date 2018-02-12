using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BookStore.Core.ViewModel;
using System.ComponentModel;

namespace BookStore.Forms
{
    public partial class MainPage : ContentPage
    {
        public MainPage(BookStoreViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Initialize();

        }
    }
}
