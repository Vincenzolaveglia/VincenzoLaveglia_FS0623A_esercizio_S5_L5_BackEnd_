namespace Polizia_Municipale.Models
{
    public class JoinModel
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public double Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public DateTime DataViolazione { get; set; }
    }
}
