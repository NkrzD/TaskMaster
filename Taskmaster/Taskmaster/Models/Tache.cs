using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskMaster.Models
{
    public class Tache : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _titre = default!;
        public string Titre
        {
            get => _titre;
            set => SetProperty(ref _titre, value);
        }

        private string _description = default!;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime _dateCreation = DateTime.Now;
        public DateTime DateCreation
        {
            get => _dateCreation;
            set => SetProperty(ref _dateCreation, value);
        }

        private DateTime? _echeance;
        public DateTime? Echeance
        {
            get => _echeance;
            set => SetProperty(ref _echeance, value);
        }

        private Statut _statut;
        public Statut Statut
        {
            get => _statut;
            set => SetProperty(ref _statut, value);
        }

        private Priorite _priorite;
        public Priorite Priorite
        {
            get => _priorite;
            set => SetProperty(ref _priorite, value);
        }

        private string _categorie = default!;
        public string Categorie
        {
            get => _categorie;
            set => SetProperty(ref _categorie, value);
        }

        private int _auteurId;
        public int AuteurId
        {
            get => _auteurId;
            set => SetProperty(ref _auteurId, value);
        }

        private Utilisateur _auteur = default!;
        public Utilisateur Auteur
        {
            get => _auteur;
            set => SetProperty(ref _auteur, value);
        }

        private int? _realisateurId;
        public int? RealisateurId
        {
            get => _realisateurId;
            set => SetProperty(ref _realisateurId, value);
        }

        private Utilisateur? _realisateur;
        public Utilisateur? Realisateur
        {
            get => _realisateur;
            set => SetProperty(ref _realisateur, value);
        }

        public List<SousTache> SousTaches { get; set; } = new();
        public List<Commentaire> Commentaires { get; set; } = new();
        public List<TacheEtiquette> TacheEtiquettes { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum Statut
    {
        Afaire,
        Encours,
        Terminee,
        Annulee
    }

    public enum Priorite
    {
        Basse,
        Moyenne,
        Haute,
        Critique
    }
}
