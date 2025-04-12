using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Titre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? Echeance { get; set; }

        public Statut Statut { get; set; }
        public Priorite Priorite { get; set; }
        public string Categorie { get; set; } = default!;

        public int AuteurId { get; set; }
        public Utilisateur Auteur { get; set; } = default!;

        public int? RealisateurId { get; set; }
        public Utilisateur? Realisateur { get; set; }

        public List<SousTache> SousTaches { get; set; } = new();
        public List<Commentaire> Commentaires { get; set; } = new();
        public List<TacheEtiquette> TacheEtiquettes { get; set; } = new();
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
