﻿using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCategoriesPage : ContentPage
    {
        readonly AdminCategoriesPageViewModel _viewModel;

        public AdminCategoriesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AdminCategoriesPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearring();
        }
    }
}