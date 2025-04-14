using TaskMaster.Data;
using TaskMaster.ViewModels;
using TaskMaster.Models;
using TaskMaster;

namespace Taskmaster
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDbContext _dbContext;
        private readonly TacheViewModel _viewModel;

        public MainPage(AppDbContext dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;
            _viewModel = new TacheViewModel(_dbContext);

            BindingContext = _viewModel;

            CheckDatabaseConnection();
        }

        private async void OnAddTacheClicked(object sender, EventArgs e)
        {
            if (AppSession.CurrentUser == null)
            {
                await DisplayAlert("Erreur", "Aucun utilisateur connecté.", "OK");
                return;
            }

            var titre = TitreEntry.Text?.Trim();
            var description = DescriptionEditor.Text?.Trim();

            if (string.IsNullOrWhiteSpace(titre))
            {
                await DisplayAlert("Erreur", "Le titre est obligatoire.", "OK");
                return;
            }

            var nouvelleTache = new Tache
            {
                Titre = titre,
                Description = description ?? "",
                DateCreation = DateTime.Now,
                Statut = Statut.Afaire,
                Priorite = Priorite.Moyenne,
                Categorie = "Général",
                AuteurId = AppSession.CurrentUser.Id
            };

            _dbContext.Taches.Add(nouvelleTache);
            await _dbContext.SaveChangesAsync();

            await _viewModel.LoadTaches();

            TitreEntry.Text = "";
            DescriptionEditor.Text = "";
        }


        private async void CheckDatabaseConnection()
        {
            try
            {
                if (await _dbContext.Database.CanConnectAsync())
                {
                    StatusLabel.Text = "Connexion à la base de données réussie !";
                    StatusLabel.TextColor = Colors.Green;
                }
                else
                {
                    StatusLabel.Text = "Échec de la connexion à la base de données.";
                    StatusLabel.TextColor = Colors.Red;
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Erreur de connexion : {ex.Message}";
                StatusLabel.TextColor = Colors.Red;
            }
        }
    }
}
