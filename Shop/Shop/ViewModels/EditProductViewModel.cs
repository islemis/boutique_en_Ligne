using Shop.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    // ViewModel for UpdateProductPage
    public class EditProductViewModel : BaseViewModel
    {
        private Produit _product;

        public Produit Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        private ObservableCollection<Categorie> _categories;

        public ObservableCollection<Categorie> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Categorie _selectedCategory;

        public Categorie SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public ICommand UpdateCommand { get; }

        public EditProductViewModel(Produit product)
        {
            Product = product;
            LoadCategories();

            // Initialize your commands
            UpdateCommand = new Command(OnUpdateCommand);
        }

        private void LoadCategories()
        {
            // Load categories from the database
            Categories = new ObservableCollection<Categorie>(App.mydataBase.ObtenirCategories());

            // Set the selected category
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == Product.IdCategorie);
        }

        private void OnUpdateCommand()
        {
            try
            {
                // Update product details
                Product.IdCategorie = SelectedCategory.Id;

                // Save changes to the database
                App.mydataBase.ModifierProduit(Product);

                // Navigate back to the previous page
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

}
