using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Shop;
using Shop.Models;
using Shop.Views;
using Xamarin.Forms;
namespace Shop.ViewModels
{


    public class AdminCategoriesPageViewModel : BaseViewModel
    {
        private ObservableCollection<Categorie> _categories;
        private Categorie _selectedItem;
        public List<string> Icons;
    public Categorie SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public Command<Categorie> DeleteCategoryCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command EditCategoryCommand { get; }
        public Command ShowProductsCommand { get; }
        public AdminCategoriesPageViewModel()
        {
            Icons = new List<string>
        {
            "clothes.png",
            "shoes.jpeg",
           "acc.jpeg" ,
        };
            OnAppearring();
            DeleteCategoryCommand = new Command<Categorie>(OnDeleteCategory);
            AddCategoryCommand = new Command(OnAddCategory);
            EditCategoryCommand = new Command<Categorie>(OnEditCategory);
            ShowProductsCommand = new Command<Categorie>(OnShowProducts);

        }
      
            private void LoadCategories()
        {
            List<Categorie> categories = App.mydataBase.ObtenirCategories();

          

            Categories = new ObservableCollection<Categorie>(categories);
        }

        public void OnAppearring()
        {
            LoadCategories();
        }

        public string GetIconPathForCategory(string categoryName)
        {
            // Logic to generate the icon path based on the category name
            // For example, you can use a switch statement or a dictionary to map category names to icon paths
            switch (categoryName)
            {
                case "Clothes":
                    return "clothes.png";
                case "Shoes":
                    return "shoes.jpeg";
                case "Accessories":
                    return "acc.jpeg";
                // Add more cases as needed
                default:
                    return "default_icon.png"; // Provide a default icon path
            }
        }

        private async void OnDeleteCategory(Categorie selectedCategory)
        {
            if (selectedCategory != null)
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Delete category", $"Are you sure you want to delete the category {selectedCategory.Nom} ?", "Yes", "No");

                if (result)
                {
                    App.mydataBase.SupprimerCategorie(selectedCategory.Id);
                    LoadCategories();

                }
            }
           
        }
        private async void OnShowProducts(Categorie selectedCategory)
        {
            Console.WriteLine(selectedCategory.Nom);

            if (selectedCategory != null)
            {
                // Navigate to the Products page and pass the selected category
                await Application.Current.MainPage.Navigation.PushAsync(new Products(selectedCategory));
            }
        }
       
      
    private async void OnAddCategory()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditCategory(null));
        }
        private async void OnEditCategory(Categorie selectedCategory)
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditCategory(selectedCategory));
        }


    }

}