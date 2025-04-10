﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using TaskManagerApp.Data;

namespace Taskmaster
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            // Appel à la méthode pour tester la connexion au démarrage
            CheckDatabaseConnection();
        }

        // Méthode pour tester la connexion à la base de données
        private async void CheckDatabaseConnection()
        {
            try
            {
                using (var dbContext = new AppDbContext(new DbContextOptions<AppDbContext>()))
                {
                    // Test de la connexion
                    if (await dbContext.Database.CanConnectAsync())
                    {
                        // Mise à jour du texte du label si la connexion est réussie
                        StatusLabel.Text = "Connexion à la base de données réussie !";
                        StatusLabel.TextColor = Microsoft.Maui.Graphics.Colors.Green;
                    }
                    else
                    {
                        // Si la connexion échoue
                        StatusLabel.Text = "Échec de la connexion à la base de données.";
                        StatusLabel.TextColor = Microsoft.Maui.Graphics.Colors.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs et affichage dans le label
                StatusLabel.Text = $"Erreur de connexion : {ex.Message}";
                StatusLabel.TextColor = Microsoft.Maui.Graphics.Colors.Red;
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
