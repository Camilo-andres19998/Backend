
using Backend.Models;


namespace Backend.Services
{
    public interface IUsersService
    {
       public Task<List<Usuario>> GetUsuarios();
    }


    public class UsuariosServices : IUsersService {

        public Task<List<Usuario>> GetUsuarios()
        {
          throw new NotImplementedException();
        }
        

        }
    }
    
    

