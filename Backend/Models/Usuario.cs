namespace Backend.Models
{
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string usuario { get; set; }

        public string passrowd { get; set; }

        public string rol { get; set; }

        public static List<Usuario> DB()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    idUsuario = "1",
                    usuario = "Camilo",
                    passrowd = "123.",
                    rol = "empleado"
                },

                 new Usuario
                {
                    idUsuario = "1",
                    usuario = "Daniel",
                    passrowd = "123.",
                    rol = "empleado"
                },

                   new Usuario
                {
                    idUsuario = "2",
                    usuario = "Benjamin",
                    passrowd = "123.",
                    rol = "asesor"
                },


                     new Usuario
                {
                    idUsuario = "3",
                    usuario = "Alejandro",
                    passrowd = "123.",
                    rol = "administrador"
                },


            };

            return list;



        }
    }

}
