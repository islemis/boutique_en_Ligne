using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public AdminCategoriesPageViewModel()
        {
            DeleteCategoryCommand = new Command<Categorie>(OnDeleteCategory);
            AddCategoryCommand = new Command(OnAddCategory);
            EditCategoryCommand = new Command(OnEditCategory);
        }

        private void LoadCategories()
        {
            List<Categorie> categories = App.mydataBase.ObtenirCategories();

            // Verify that 'Nom' property is correctly set in each Categorie object
            foreach (var categorie in categories)
            {
                Console.WriteLine($"Category Name: {categorie.Nom}");
            }

            Categories = new ObservableCollection<Categorie>(categories);
        }

        public void OnAppearring()
        {
            LoadCategories();
        }

        private async void OnDeleteCategory(Categorie selectedCategory)
        {
            if (selectedCategory != null)
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Supprimer la catégorie", $"Êtes-vous sûr de vouloir supprimer la catégorie {selectedCategory.Nom} ?", "Oui", "Non");

                if (result)
                {
                    App.mydataBase.SupprimerCategorie(selectedCategory.Id);
                    LoadCategories();

                    await Application.Current.MainPage.DisplayAlert("Catégorie supprimée", "La catégorie a été supprimée avec succès.", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Sélectionnez une catégorie", "Veuillez sélectionner une catégorie avant de supprimer.", "OK");
            }
        }


        private async void OnAddCategory()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditCategory(null));
        }
        private async void OnEditCategory()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditCategory(SelectedItem));
        }
        

    }

}
