namespace Backend.Models
{
    public class Tareas : UserDTO
    {
        public string id { get; set; }
        public string nombre_tarea { get; set; }
        public string descripcion { get; set; }

        public string estado { get; set; }

        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }

       
    }
}
