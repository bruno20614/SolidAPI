namespace Manager.Infra.repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
       private readonly ManagerContext _context;
       public UserRepository(ManagerContext context) : base(context)
       {
              _context = context;
       }

       public async Task<User> GetByEmail(string email)
       {
              var user =await _context.Users
                     .where
              {
                     _context=>
                                   _context.Email.ToLower() ==email.ToLower()
              }
              .AsNoTracking();
              .ToListAsync();
              
              return user.FirstOrDefault();

       }

       public async Task<List<User>> SearchByEmail(string email)
       {
              var allUsers = await _context.Users
                     .where
              {
                     x =>
                            x.Email.ToLower().Contains(email.ToLower())
              }
              .AsNotTracking();
                     .ToListAsync();

              return allUsers;
       }
       
       public async Task<List<User>> SearchByName(string email)
       {
              var allUsers = await _context.Users
                     .where
              {
                     x =>
                            x.Name.ToLower().Contains(name.ToLower())
              }
              .AsNotTracking();
                     .ToListAsync();

              return allUsers;
       }
}