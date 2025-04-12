using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;

namespace TaskMaster.Models
{
    public class Commentaire
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Now;

        public int AuteurId { get; set; }
        public Utilisateur Auteur { get; set; } = default!;

        public int TacheId { get; set; }
        public Tache Tache { get; set; } = default!;
    }
}