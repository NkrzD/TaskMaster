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

            _ = _viewModel.LoadTaches(); // Chargement initial des tâches

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
                Statut = (Statut)StatutPicker.SelectedIndex,
                Priorite = (Priorite)PrioritePicker.SelectedIndex,
                Categorie = "Général",
                AuteurId = AppSession.CurrentUser.Id
            };


            _dbContext.Taches.Add(nouvelleTache);
            await _dbContext.SaveChangesAsync();

            await _viewModel.LoadTaches();

            TitreEntry.Text = "";
            DescriptionEditor.Text = "";
        }

        private async void OnSupprimerClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var tache = (Tache)button.CommandParameter;

            var confirmation = await DisplayAlert("Confirmation", "Supprimer cette tâche ?", "Oui", "Non");
            if (!confirmation) return;

            _dbContext.Taches.Remove(tache);
            await _dbContext.SaveChangesAsync();
            await _viewModel.LoadTaches();
        }

        private async void OnModifierClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var tache = (Tache)button.CommandParameter;

            string nouveauTitre = await DisplayPromptAsync("Modifier", "Titre :", initialValue: tache.Titre);
            string nouvelleDescription = await DisplayPromptAsync("Modifier", "Description :", initialValue: tache.Description);

            string[] statuts = Enum.GetNames(typeof(Statut));
            string nouveauStatut = await DisplayActionSheet("Choisir un nouveau statut", "Annuler", null, statuts);

            string[] priorites = Enum.GetNames(typeof(Priorite));
            string nouvellePriorite = await DisplayActionSheet("Choisir une priorité", "Annuler", null, priorites);

            if (!string.IsNullOrWhiteSpace(nouveauTitre) &&
                !string.IsNullOrWhiteSpace(nouveauStatut) &&
                !string.IsNullOrWhiteSpace(nouvellePriorite))
            {
                tache.Titre = nouveauTitre;
                tache.Description = nouvelleDescription;
                tache.Statut = Enum.Parse<Statut>(nouveauStatut);
                tache.Priorite = Enum.Parse<Priorite>(nouvellePriorite);

                _dbContext.Taches.Update(tache);
                await _dbContext.SaveChangesAsync();
            }
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
