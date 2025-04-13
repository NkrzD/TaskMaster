using TaskMaster.Data;
using TaskMaster.Helpers;
using TaskMaster.Models;
using TaskMaster;


namespace Taskmaster.Views
{
    public partial class ConnexionPage : ContentPage
    {
        private readonly AppDbContext _dbContext;

        public ConnexionPage(AppDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnConnexionClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim();
            var motDePasse = MotDePasseEntry.Text ?? "";

            var utilisateur = _dbContext.Utilisateurs
                .FirstOrDefault(u => u.Email == email);

            if (utilisateur == null || utilisateur.MotDePasse != PasswordHelper.Hash(motDePasse))
            {
                await DisplayAlert("Erreur", "Email ou mot de passe incorrect.", "OK");
                return;
            }

            // Stocke l'utilisateur connecté
            AppSession.CurrentUser = utilisateur;

            await DisplayAlert("Bienvenue", $"Bonjour {utilisateur.Prenom} !", "OK");

            // Redirige vers la page principale
            await Navigation.PushAsync(new MainPage(_dbContext));
        }

        private async void OnInscriptionRedirectClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InscriptionPage(_dbContext));
        }

    }
}
