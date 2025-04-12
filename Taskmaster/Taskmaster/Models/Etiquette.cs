using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class Etiquette
    {
        public int Id { get; set; }
        public string Nom { get; set; } = default!;

        public List<TacheEtiquette> TacheEtiquettes { get; set; } = new();
    }

    public class TacheEtiquette
    {
        public int TacheId { get; set; }
        public Tache Tache { get; set; } = default!;

        public int EtiquetteId { get; set; }
        public Etiquette Etiquette { get; set; } = default!;
    }
}
