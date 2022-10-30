
using Backend.Models;


namespace Backend.Services
{
    public interface IUsuariosService
    {
       public Task<List<Usuario>> GetUsuarios();
    }


    public class UsuariosServices : IUsuariosService {

        public Task<List<Usuario>> GetUsuarios()
        {
          throw new NotImplementedException();
        }
        

        }
    }
    
    

