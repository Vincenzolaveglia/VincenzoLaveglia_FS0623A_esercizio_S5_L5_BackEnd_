namespace Polizia_Municipale.Models
{
    public class Verbale
    {
        public int Id { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public double Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public string TipoViolazione { get; set; }
        public int IdNominativo { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string NominativoAgente { get; set; }
    }
}
