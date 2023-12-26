using Shop.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public class BoutiqueDataBase
    {
        private readonly SQLiteConnection _baseDeDonnees;

        public BoutiqueDataBase(string cheminBaseDeDonnees)
        {
            _baseDeDonnees = new SQLiteConnection(cheminBaseDeDonnees);
            _baseDeDonnees.CreateTable<Categorie>();
            _baseDeDonnees.CreateTable<Produit>();
            _baseDeDonnees.CreateTable<LigneCommande>();
            _baseDeDonnees.CreateTable<Commande>();

        }



        // Public method to get the instance of the database

        // Opérations sur les catégories
        public List<Categorie> ObtenirCategories()
        {
            return _baseDeDonnees.Table<Categorie>().ToList();
        }
        // Opérations sur les produits par catégorie
        public List<Produit> ObtenirProduitsParCategorie(Categorie c)
        {
            var produits = _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == c.Id).ToList();
            return produits;
        }

        public void AjouterCategorie(Categorie categorie)
        {
            _baseDeDonnees.Insert(categorie);
        }

        public void ModifierCategorie(Categorie categorie)
        {
            _baseDeDonnees.Update(categorie);
        }

        public void SupprimerCategorie(int idCategorie)
        {
            _baseDeDonnees.Delete<Categorie>(idCategorie);
        }

        // Opérations sur les produits
        public List<Produit> ObtenirProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToList();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.Insert(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            _baseDeDonnees.Update(produit);
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.Delete<Produit>(idProduit);
        }

        // Opérations sur les lignes de commande
        public void AjouterLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Insert(ligneCommande);
        }
        public List<LigneCommande> ObtenirLignesCommande(int idCommande)
        {
            return _baseDeDonnees.Table<LigneCommande>().Where(l => l.Id == idCommande).ToList();
        }
        public List<LigneCommande> ObtenirToutesLesLignesCommande()
        {
            return _baseDeDonnees.Table<LigneCommande>().ToList();
        }

        public void ModifierLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Update(ligneCommande);
        }

        public void SupprimerLigneCommande(int idLigneCommande)
        {
            _baseDeDonnees.Delete<LigneCommande>(idLigneCommande);
        }

        // Opérations sur les commandes
        public void ModifierCommande(Commande commande)
        {
            _baseDeDonnees.Update(commande);
        }

        public void SupprimerCommande(int idCommande)
        {
            // Supprimer d'abord les lignes de commande associées
            var lignesCommande = ObtenirLignesCommande(idCommande);
            foreach (var ligneCommande in lignesCommande)
            {
                _baseDeDonnees.Delete<LigneCommande>(ligneCommande.Id);
            }

            // Ensuite, supprimer la commande elle-même
            _baseDeDonnees.Delete<Commande>(idCommande);
        }

        public List<Commande> ObtenirToutesLesCommandes()
        {
            return _baseDeDonnees.Table<Commande>().ToList();
        }



        // Opérations sur les commandes
        public void AjouterCommande(Commande commande)
        {
            _baseDeDonnees.Insert(commande);
        }



    }
}
