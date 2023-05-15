using API_Polizas.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Authentication;

namespace API_Polizas.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClienteService(MongoDbContext context)
        {
            _clientes = context.Clientes;
        }

        public Cliente Get(ObjectId id) => _clientes.Find<Cliente>(cliente => cliente.Id == id).FirstOrDefault();
    }

}
