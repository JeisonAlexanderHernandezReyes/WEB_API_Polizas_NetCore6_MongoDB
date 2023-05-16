namespace API_Polizas.Models.Dto
{
    public class PolizaInfoDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Cobertura { get; set; }
        public double ValorMaxCobertura { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
    }
}