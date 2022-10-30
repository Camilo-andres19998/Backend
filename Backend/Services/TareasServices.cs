
using Backend.Models;


namespace Backend.Services
{
    public interface ITareasServices
    {
       public Task<List<Tareas>> GetTareas();
    }


    public class TareasServices : ITareasServices
    {

        public Task<List<Tareas>> GetTareas()
        {
          throw new NotImplementedException();
        }
        

        }
    }
    
    

