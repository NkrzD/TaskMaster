// AppSession.cs
using TaskMaster.Models;

namespace TaskMaster
{
    public static class AppSession
    {
        public static Utilisateur? CurrentUser { get; set; }
    }
}
