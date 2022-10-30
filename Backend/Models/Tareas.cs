namespace Backend.Models
{
    public class Tareas : Usuario
    {
        public string id { get; set; }
        public string nombre_tarea { get; set; }
        public string descripcion { get; set; }

        public string estado { get; set; }

        public string fecha_creacion { get; set; }
        public string fecha_actualizacion { get; set; }


    }
}
