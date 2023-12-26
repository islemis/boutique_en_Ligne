using Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class UserProductsPage : BaseViewModel
    {
        private Produit _selectedItem;
        private Categorie selectedCategory;
        private ObservableCollection<Produit> _products;

        public ObservableCollection<Produit> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        public Produit SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public  void OnAppearing()
        {
            LoadProductsForSelectedCategory();
        }

        public UserProductsPage( Categorie SelectedItem)
        {
            selectedCategory = SelectedItem;


            LoadProductsForSelectedCategory();
            AjouterAuPanierCommand = new Command<Produit>(AjouterAuPanier);

        }
        public UserProductsPage()
        {

        }
        private void LoadProductsForSelectedCategory()
        {
            if (selectedCategory != null)
            {
                // Retrieve products based on the selected category ID
                List<Produit> products = App.mydataBase.ObtenirProduitsParCategorie(selectedCategory);

                // Update the collection of products in your ViewModel
                Products = new ObservableCollection<Produit>(products);

                // Add some debug output
                Debug.WriteLine($"Loaded {Products.Count} products for category {selectedCategory.Nom}");

            }
            OnPropertyChanged(nameof(Products));

        }
        public Command AjouterAuPanierCommand { get; }

        private void AjouterAuPanier(Produit p)
        {
            if (p != null)
            {
                App.ShoppingCart.AjouterArticle(p.Id, p.Nom, p.Prix, 1);
                AfficherMessage("Produit ajouté au panier");
            }
            else
            {
                AfficherMessage("Erreur : Impossible d'ajouter le produit au panier");
            }
        }

        private void AfficherMessage(string message)
        {
            // Use Xamarin.Forms' DisplayAlert to show a pop-up message
            Application.Current.MainPage.DisplayAlert("Information", message, "OK");
        }



    }
}
