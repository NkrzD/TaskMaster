using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class SousTache
    {
        public int Id { get; set; }
        public string Titre { get; set; } = default!;
        public Statut Statut { get; set; }
        public DateTime? Echeance { get; set; }

        public int TacheId { get; set; }
        public Tache Tache { get; set; } = default!;
    }
}