// AppSession.cs
using TaskMaster.Models;

namespace TaskManagerApp.Views
{
    public static class AppSession
    {
        public static Utilisateur? CurrentUser { get; set; }
    }
}
