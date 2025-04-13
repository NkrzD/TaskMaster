using TaskMaster.Data;
using TaskMaster.Models;
using TaskMaster.Helpers;

namespace Taskmaster.Views
{
    public partial class InscriptionPage : ContentPage
    {
        private readonly AppDbContext _dbContext;

        public InscriptionPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnInscriptionClicked(object sender, EventArgs e)
        {
            var utilisateur = new Utilisateur
            {
                Nom = NomEntry.Text?.Trim() ?? "",
                Prenom = PrenomEntry.Text?.Trim() ?? "",
                Email = EmailEntry.Text?.Trim() ?? "",
                MotDePasse = PasswordHelper.Hash(MotDePasseEntry.Text ?? "")
            };

            // Vérifier unicité de l’email
            if (_dbContext.Utilisateurs.Any(u => u.Email == utilisateur.Email))
            {
                await DisplayAlert("Erreur", "Un compte existe déjà avec cet email.", "OK");
                return;
            }

            _dbContext.Utilisateurs.Add(utilisateur);
            await _dbContext.SaveChangesAsync();

            await DisplayAlert("Succès", "Compte créé avec succès !", "OK");

            // Rediriger vers la page de connexion
            await Navigation.PushAsync(new ConnexionPage(_dbContext));
        }
    }
}
