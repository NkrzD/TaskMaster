using TaskMaster.Data;
using Taskmaster.Views;

namespace Taskmaster
{
    public partial class App : Application
    {
        public App(AppDbContext dbContext)
        {
            InitializeComponent();

            // Afficher la page de connexion au démarrage
            MainPage = new NavigationPage(new ConnexionPage(dbContext));
        }
    }
}
