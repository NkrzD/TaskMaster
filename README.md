Pour la récupération du projet il faut cloner le repo git https://github.com/NkrzD/TaskMaster.git

Une fois le dossier récupéré ouvrir le .SLN

Ne pas oublier de généré la solution une fois les package installé (ef core et pomelo pour mysql)
Clique droit sur le projet "taskmaster" -> déployer

Pour créer la base de données utiliser Xampp en local avec mysql et phpmyadmin

Appelé la base de données "taskmongodb" (la chaine de connexion étant la suivante dans le code : "Server=localhost;Port=3306;Database=taskmanagerdb;User=root;Password=;"
Par défaut phpmyadmin n'applique aucun mot de passe et l'utilisateur est root le port 3306 également est utilisé.

Pour faire la migration de la base effectué ces commandes dans l'ordre :

dotnet ef migrations add InitialCreate --project . --startup-project . --framework net9.0

dotnet ef database update --project . --startup-project . --framework net9.0

Si migration déjà présente faire :

dotnet ef migrations remove --project . --startup-project . --framework net9.0

