using System.Windows.Input;
using Xamarin.Forms;
using Shop.Services;
using Shop.Models;
using System.Collections.ObjectModel;
using System;

namespace Shop.ViewModels
{
    public class PanierViewModel : BaseViewModel
    {
        private ObservableCollection<ArticlePanier> _articlesPanier;
        private ArticlePanier  _selectedItem;

        public ArticlePanier SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ObservableCollection<ArticlePanier> Articles
        {
            get { return _articlesPanier; }
            set
            {
                _articlesPanier = value;
                OnPropertyChanged(nameof(Articles));
            }
        }
        private Panier _panier;

        public Panier Panier
        {
            get { return _panier; }
            set { SetProperty(ref _panier, value); }
        }

        public ICommand RetirerArticleCommand { get; }
        public ICommand PasserCommandeCommand { get; }
        public ICommand ViderPanierCommand { get; }

        public PanierViewModel()
        {
            _articlesPanier = new ObservableCollection<ArticlePanier>(Panier.Articles);
            Console.WriteLine(Articles[0]);
            RetirerArticleCommand = new Command<int>(RetirerArticle);
            PasserCommandeCommand = new Command(PasserCommande);
            ViderPanierCommand = new Command(ViderPanier);
            IncrementQuantityCommand = new Command<int>(IncrementQuantity);
            DecrementQuantityCommand = new Command<int>(DecrementQuantity);
            CalculerTotalCommand = new Command(CalculerTotal);

        }

        private void RetirerArticle(int idProduit)
        {
            App.shoppingCart.RetirerArticle(idProduit);
            RefreshPanier();
        }

        private void PasserCommande()
        {
            ViderPanier();
        }

        private void ViderPanier()
        {
            Console.WriteLine("ViderPanier method called."); // Add debugging output
            Articles = new ObservableCollection<ArticlePanier>();

        }
        // Commandes liées aux méthodes du Panier
        public ICommand IncrementQuantityCommand { get; }
        public ICommand DecrementQuantityCommand { get; }
        public ICommand CalculerTotalCommand { get; }

        // Méthodes appelées par les commandes
        // Méthodes appelées par les commandes
        private void IncrementQuantity(int idProduit)
        {
            Panier.IncrementQuantity(idProduit);
            RefreshPanier();
        }

        private void DecrementQuantity(int idProduit)
        {
            Panier.DecrementQuantity(idProduit);
            RefreshPanier();
        }

        private void CalculerTotal()
        {
            var total = Panier.CalculerTotal();
            Console.WriteLine($"Total du panier: {total:C}");
        }
        // Actualise la liste d'articles après chaque modification
        private void RefreshPanier()
        {
            Articles.Clear();
            foreach (var article in Panier.Articles)
            {
                Articles.Add(article);
            }

            // Actualisez d'autres propriétés liées à l'interface utilisateur si nécessaire
            OnPropertyChanged(nameof(Articles));
        }
    }
}
