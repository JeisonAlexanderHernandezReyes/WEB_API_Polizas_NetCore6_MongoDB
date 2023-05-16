using API_Polizas.Models;

namespace API_Polizas.Interface
{
    public interface ITransaccionService
    {
        Automotor Create(Automotor automotor);
        AutomotorDto GetAutomotorDto(string id);
        Automotor Get(string id);
        Automotor AddPolizas(string automotorId, List<Poliza> newPolicies);
        List<Poliza> GetPolizasByPlaca(string placa);
        Poliza GetPolizaById(string polizaId);
        List<AutomotorDto> GetAllAutomotores();
    }
}