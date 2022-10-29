namespace Backend.Models
{
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string username { get; set; }

        public string nombre { get; set; }

        public string passrowd { get; set; }


       // public Tareas tarea { get; set; }

        public string rol { get; set; }

        public static List<Usuario> DB()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    idUsuario = "1",
                    username = "Camilo0098",
                    nombre = "Camilo",
                    passrowd = "123.",
                    rol = "empleado"
                },

                 new Usuario
                {
                    idUsuario = "1",
                    username = "Daniel-98",
                     nombre = "Daniel",
                    passrowd = "123.",
                    rol = "empleado"
                },

                   new Usuario
                {
                    idUsuario = "2",
                    username = "Benjamin-98",
                    nombre = "Benjamin",
                    passrowd = "123.",
                    rol = "asesor"
                },


                     new Usuario
                {
                     idUsuario = "3",
                     username = "Alejandro-98",
                     nombre = "Alejandro",
                     passrowd = "123.",
                     rol = "administrador"
                },


            };

            return list;



        }
    }

}
