namespace API_Polizas.Models
{
    public class UserClient
    {
        public string idUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string rol { get; set; }

        public static List<UserClient> dbUsersJwt()
        {
            var list = new List<UserClient>()
            {
                new UserClient
                {
                    idUsuario = "1",
                    usuario = "Empleado_1",
                    password = "Abc.123",
                    rol = "EMP"
                },
                new UserClient
                {
                    idUsuario = "2",
                    usuario = "ADM_2",
                    password = "Xyz.123",
                    rol = "ADM"
                },
                new UserClient
                {
                    idUsuario = "3",
                    usuario = "Empleado_3",
                    password = "Cba.123",
                    rol = "EMP"
                }
                ,new UserClient
                {
                    idUsuario = "4",
                    usuario = "AMD_4",
                    password = "Zyx.123",
                    rol = "ADM"
                }
            };
            return list;
        }
    }
}
