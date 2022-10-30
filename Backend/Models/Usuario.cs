namespace Backend.Models
{
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string username { get; set; } = string.Empty;

        public string nombre { get; set; }

        public string passwd { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public byte[] PassrowdHash { get; set; } 

        public UserDTO UserDTO { get; set; }


        public byte[] PassrowdSalt { get; set; } 

        // public Tareas tarea { get; set; }

        public string rol { get; set; }

        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }



        public static List<Usuario> DB()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    idUsuario = "1",
                    username = "Camilo0098",
                    nombre = "Camilo",
                   
                    rol = "empleado"
                },

                 new Usuario
                {
                   idUsuario = "2",
                    username = "Benjamin-98",
                    nombre = "Benjamin",
                   
                    rol = "asesor"
                },

                   new Usuario
                {
                    idUsuario ="3",
                    username = "Daniel-98",
                    nombre = "Daniel",
                
                    rol = "asesor"
                },


                     new Usuario
                {
                     idUsuario = "4",
                     username = "Alejandro-98",
                     nombre = "Alejandro",
                   
                     rol = "administrador"
                },


            };

            return list;



        }
    }

}
