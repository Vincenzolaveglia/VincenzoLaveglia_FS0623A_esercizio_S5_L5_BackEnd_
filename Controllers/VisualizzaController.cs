using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Polizia_Municipale.Models;

namespace Polizia_Municipale.Controllers
{
    public class VisualizzaController : Controller
    {
        private string connectionString = "Server=VINCENZO\\SQLEXPRESS; Initial Catalog=Polizia_Municipale; Integrated Security=true; TrustServerCertificate=True";

        public IActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand("select Anagrafica.Nome, Anagrafica.Cognome, * from Verbali " +
                    "inner join Anagrafica on Verbali.Nominativo = Anagrafica.id", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.Id = (int)reader["id"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = (string)reader["IndirizzoViolazione"];
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];
                        verbale.TipoViolazione = (string)reader["TipoViolazione"];
                        verbale.IdNominativo = (int)reader["Nominativo"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];

                        verbale.NominativoAgente = (string)reader["NominativoAgente"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(verbali);
        }
        public IActionResult GreaterThan10Points()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<JoinModel> verbali = new List<JoinModel>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, DataViolazione, Importo, DecurtamentoPunti from Verbali " +
                     $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                     $"where DecurtamentoPunti > 10", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        JoinModel verbale = new JoinModel();

                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(verbali);
        }

        public IActionResult GreaterThan400Eur()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, * from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"where Importo > 400", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.Id = (int)reader["id"];
                        verbale.DataViolazione = (DateTime)reader["DataViolazione"];
                        verbale.IndirizzoViolazione = (string)reader["IndirizzoViolazione"];
                        verbale.DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"];
                        verbale.Importo = double.Parse(reader["Importo"].ToString());
                        verbale.DecurtamentoPunti = (int)reader["DecurtamentoPunti"];
                        verbale.TipoViolazione = (string)reader["TipoViolazione"];
                        verbale.IdNominativo = (int)reader["Nominativo"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.NominativoAgente = (string)reader["NominativoAgente"];


                        verbali.Add(verbale);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(verbali);
        }

        public IActionResult ByPerson()
        {

            SqlConnection conn = new SqlConnection(connectionString);
            List<VerbaliPerPersona> verbali = new List<VerbaliPerPersona>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, count(*) as TotaleVerbali from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id group by Nome, Cognome ", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VerbaliPerPersona verbale = new VerbaliPerPersona();
                        verbale.TotaleVerbali = (int)reader["TotaleVerbali"];
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];

                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(verbali);
        }

        public IActionResult PointsByPerson()
        {

            SqlConnection conn = new SqlConnection(connectionString);
            List<PuntiPerPersona> verbali = new List<PuntiPerPersona>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"select Nome, Cognome, sum(DecurtamentoPunti) as PuntiDecurtati from Verbali " +
                    $"inner join Anagrafica on Verbali.Nominativo = Anagrafica.id " +
                    $"group by Nome, Cognome ", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PuntiPerPersona verbale = new PuntiPerPersona();
                        verbale.Nome = (string)reader["Nome"];
                        verbale.Cognome = (string)reader["Cognome"];
                        verbale.PuntiDecurtati = (int)reader["PuntiDecurtati"];
                        verbali.Add(verbale);
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(verbali);
        }

    }
}
