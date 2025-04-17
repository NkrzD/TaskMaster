using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Views;
using TaskMaster.Data;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class TacheViewModel
    {
        private readonly AppDbContext _dbContext;

        public ObservableCollection<Tache> Taches { get; set; } = new();

        public TacheViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _ = LoadTaches(); // Appel asynchrone non bloquant
        }

        public async Task LoadTaches()
        {
            if (AppSession.CurrentUser == null) return;

            Taches.Clear();

            // Charger les tâches de l'utilisateur et inclure les commentaires associés
            var tachesUtilisateur = await _dbContext.Taches
                .Where(t => t.AuteurId == AppSession.CurrentUser.Id)
                .Include(t => t.Commentaires)  // Inclure les commentaires pour chaque tâche
                .ToListAsync();

            foreach (var tache in tachesUtilisateur)
            {
                // Ajoute la tâche avec ses commentaires
                Taches.Add(tache);
            }
        }


    }
}
