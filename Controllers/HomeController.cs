using Microsoft.AspNetCore.Mvc;
using Polizia_Municipale.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Polizia_Municipale.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "Server=VINCENZO\\SQLEXPRESS; Initial Catalog=Polizia_Municipale; Integrated Security=true; TrustServerCertificate=True";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Nominativo> anagrafe = new List<Nominativo>();

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand("select * from ANAGRAFICA", conn);
                SqlDataReader reader = select.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Nominativo persona = new Nominativo();

                        persona.Id = (int)reader["id"];
                        persona.Nome = (string)reader["Nome"];
                        persona.Cognome = (string)reader["Cognome"];
                        persona.Indirizzo = (string)reader["Indirizzo"];
                        persona.Città = (string)reader["Città"];
                        persona.CAP = reader["CAP"].ToString();
                        persona.CodiceFiscale = (string)reader["CodiceFiscale"];

                        anagrafe.Add(persona);
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

            return View(anagrafe);
        }

        [HttpPost]
        public IActionResult Add(string first_name, string last_name, string address, string cap, string city, string cf)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            int codice;
            try
            {
                conn.Open();
                SqlCommand insert = new SqlCommand("insert into ANAGRAFICA (Nome, Cognome, Indirizzo, Città, CAP, CodiceFiscale)" +
                     "values (@name, @surname, @address, @city, @cap, @cf)", conn);
                insert.Parameters.AddWithValue("@name", first_name);
                insert.Parameters.AddWithValue("@surname", last_name);
                insert.Parameters.AddWithValue("@address", address);
                insert.Parameters.AddWithValue("@city", city);
                insert.Parameters.AddWithValue("@cap", cap);
                insert.Parameters.AddWithValue("@cf", cf);
                insert.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Verbale(int? id, string DataViolazione, string address, double Importo, int DecurtamentoPunti, string TipoViolazione, int IdNominativo, string fullName)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand insert = new SqlCommand("insert into Verbali " +
                     "(DataViolazione, IndirizzoViolazione, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, TipoViolazione, Nominativo, NominativoAgente)" +
                     "values (@DataViolazione, @IndirizzoViolazione, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @TipoViolazione, @Nominativo, @NominativoAgente)", conn);
                insert.Parameters.AddWithValue("@DataViolazione", DataViolazione);
                insert.Parameters.AddWithValue("@IndirizzoViolazione", address);
                insert.Parameters.AddWithValue("@DataTrascrizioneVerbale", DateTime.Now);
                insert.Parameters.AddWithValue("@Importo", Importo);
                insert.Parameters.AddWithValue("@DecurtamentoPunti", DecurtamentoPunti);
                insert.Parameters.AddWithValue("@TipoViolazione", TipoViolazione);
                insert.Parameters.AddWithValue("@Nominativo", IdNominativo);
                insert.Parameters.AddWithValue("@NominativoAgente", fullName);

                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
