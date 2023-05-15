using API_Polizas.Models;
using MongoDB.Driver;
using System.Security.Authentication;
using System.Web;

namespace API_Polizas
{
    public class MongoDbContext
    {
        public IMongoCollection<Automotor> Automotores { get; set; }
        public IMongoCollection<Cliente> Clientes { get; set; }

        public MongoDbContext(IDBSettings dBSettings)
        {
            var connectionString = $"mongodb+srv://{dBSettings.Username}:{dBSettings.Password}@{dBSettings.Server}/?retryWrites=true&w=majority";   
            Console.WriteLine(connectionString);
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);
            var database = client.GetDatabase(dBSettings.DatabaseName);
            Automotores = database.GetCollection<Automotor>("Automotores");
            Clientes = database.GetCollection<Cliente>("Clientes");
        }
    }
}
