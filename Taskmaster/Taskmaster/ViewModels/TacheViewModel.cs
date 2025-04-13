using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var tachesFromDb = await _dbContext.Taches.ToListAsync();
            Taches.Clear();
            foreach (var tache in tachesFromDb)
                Taches.Add(tache);
        }
    }
}
