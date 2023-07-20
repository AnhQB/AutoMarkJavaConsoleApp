using AutoMapper;
using project.DTO;
using project.Models;

namespace project.Repositories
{
    public class AdminRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public AdminRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public List<Admin> GetAdmins()
        {
           
            return context.Admins.ToList();
            
        }



        public Admin GetAdmin(int id)
        {
           
            return context.Admins.FirstOrDefault(m => m.AdminId == id);
            
        }

        public void Add(Admin admin)
        {
            
            context.Admins.Add(admin);
            context.SaveChanges();
            
        }

        public void Update(Admin admin)
        {
            
            var auData = context.Admins.FirstOrDefault(item => item.AdminId == admin.AdminId);
            if (auData == null)
                throw new Exception("Not found Admin to update");

            auData.Username = admin.Username;
            auData.Password = admin.Password;

            context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            
            var author = context.Admins.FirstOrDefault(item => item.AdminId == id);
            if (author == null)
                throw new Exception("Not found Admin");

            //context.Database.ExecuteSqlRaw("Delete from BookAuthor where book_id =" + id);
            context.Admins.Remove(author);
            context.SaveChanges();
            
        }
    }
}
