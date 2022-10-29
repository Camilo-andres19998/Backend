namespace Backend.Models
{
    public class Usuario:UserDTO
    {
        public string idUsuario { get; set; }
        public string username { get; set; } = string.Empty;

        public string nombre { get; set; }

        public byte[] passrowdHash { get; set; }


        public byte[] passrowdSalt { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

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
                    Password ="123",
                    rol = "empleado"
                },

                 new Usuario
                {
                   idUsuario = "2",
                    username = "Benjamin-98",
                    nombre = "Benjamin",
                    Password ="123",
                    rol = "asesor"
                },

                   new Usuario
                {
                    idUsuario = "2",
                    username = "Daniel-98",
                    nombre = "Daniel",
                    Password ="1234",
                    rol = "asesor"
                },


                     new Usuario
                {
                     idUsuario = "3",
                     username = "Alejandro-98",
                     nombre = "Alejandro",
                     Password ="1235",
                     rol = "administrador"
                },


            };

            return list;



        }
    }

}



        
    


