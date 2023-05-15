namespace API_Polizas
{
    public class DBSettings : IDBSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDBSettings
    {
        string Username { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string DatabaseName { get; set; }
    }

}
