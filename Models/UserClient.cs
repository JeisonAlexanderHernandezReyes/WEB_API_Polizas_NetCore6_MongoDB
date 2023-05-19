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
                    usuario = "emp1@polizas.com",
                    password = "Abc.123",
                    rol = "EMP"
                },
                new UserClient
                {
                    idUsuario = "2",
                    usuario = "adm1@polizas.com",
                    password = "Xyz.123",
                    rol = "ADM"
                },
                new UserClient
                {
                    idUsuario = "3",
                    usuario = "emp3@polizas.com",
                    password = "Cba.123",
                    rol = "EMP"
                }
                ,new UserClient
                {
                    idUsuario = "4",
                    usuario = "adm4@polizas.com",
                    password = "Zyx.123",
                    rol = "ADM"
                }
            };
            return list;
        }
    }
}
