using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = default!;
        public string Prenom { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string MotDePasse { get; set; } = default!;


        public List<Tache> TachesCreees { get; set; } = new();
        public List<Tache> TachesAssignees { get; set; } = new();
        public List<Commentaire> Commentaires { get; set; } = new();
    }
}
