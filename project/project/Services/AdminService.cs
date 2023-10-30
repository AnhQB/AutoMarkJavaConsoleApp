using project.DTO;
using project.Models;
using project.Repositories;

namespace project.Services
{
    public class AdminService
    {
        private AdminRepository repository;

        private AdminService()
        {
            repository = new AdminRepository();
        }

        private static readonly AdminService _singleton = new AdminService();

        public static AdminService GetSingleton()
        {
            return _singleton;
        }

        public List<Admin> GetAdmins()
        {
            return repository.GetAdmins();
        }

        public Admin GetAdmin(int id)
        {
            return repository.GetAdmin(id);
        }

        public Admin GetAdmin(LoginDTO login)
        {
            return repository.GetAdmin(login);
        }

        public void Update(Admin admin)
        {
            repository.Update(admin);
        }

        public void Add(Admin admin)
        {
            repository.Add(admin);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
