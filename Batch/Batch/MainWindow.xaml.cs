using Batch.Models;
using Batch.Repo;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using Microsoft.VisualBasic;

namespace Batch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Instance delle mie classi 
       
        private List<Persona> GetPersonas()
        {
            return PersonaRepo.GetInstance().GetAll();
        }

        private List<CodiceFiscale> GetCodFiscales()
        {
            return CodFisRepo.GetInstance().GetAll();
        }
        #endregion
        private void BtnMostra_Click(object sender, RoutedEventArgs e)
        {
            dbPersona.ItemsSource = PersonaRepo.GetInstance().GetAll();
        }

        private void BtnScrivi_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:\\Users\\Utente\\Desktop\\Persona.csv";
            //string? path = Path.GetDirectoryName(typeof(MainWindow) .Assembly.Location);

            var persone = PersonaRepo.GetInstance().GetAll();
            string contenuto = JsonSerializer.Serialize(persone);  

            try
            {

                if (path is not null)
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(contenuto);
                        sw.Close();
                    }
                MessageBox.Show("operazione eseguita con successo");
                
            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message);

            }
            



        }

        private void BtnMostraCodFis_Click(object sender, RoutedEventArgs e)
        {
            dbCodFis.ItemsSource = CodFisRepo.GetInstance().GetAll();
        }

        private void BtnScriviCodFis_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:\\Users\\Utente\\Desktop\\CodFiscale.csv";
            //string? path = Path.GetDirectoryName(typeof(MainWindow) .Assembly.Location);

            var codFis = CodFisRepo.GetInstance().GetAll();
            string contenuto = JsonSerializer.Serialize(codFis);

            try
            {

                if (path is not null)
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(contenuto);
                        sw.Close();
                    }
                MessageBox.Show("operazione eseguita con successo");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void BtnTrasferimento_Click(object sender, RoutedEventArgs e)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("TaskBatch");
            var collection = database.GetCollection<BsonDocument>("Persona");


            string Personapath = "C:\\Users\\Utente\\Desktop\\Persona.csv";
            string CodFispath = "C:\\Users\\Utente\\Desktop\\CodFiscale.csv";
          


            try
            {
                var jsonPersone = File.ReadAllText(Personapath);
                var persone = JsonConvert.DeserializeObject<List<Persona>>(jsonPersone);

                var jsonCodici = File.ReadAllText(CodFispath);
                var codiciFiscali = JsonConvert.DeserializeObject<List<CodiceFiscale>>(jsonCodici);

                if (persone is not null && codiciFiscali is not null)
                foreach ( var persona in persone)
                {
                    var codice = codiciFiscali.FirstOrDefault(c => c.PersonaRiff == persona.IdPersona);

                        var document = new BsonDocument
                        {
                             { "nome", persona.Nome },
                             { "cognome", persona.Cognome },
                             { "email", persona.Email },
                              { "telefono", persona.Telefono },
                             { "cod_fis", new BsonDocument
                              {
                                { "codice", codice?.CodiceFis ?? string.Empty },
                                { "data_emis", codice?.DataEmis.ToString("yyyy-MM-dd") ?? string.Empty }, // Converti DateOnly a string
                                { "data_scad", codice?.DataScad.ToString("yyyy-MM-dd") ?? string.Empty }  
                              }
                             }
                        };
                        collection.InsertOne(document);
                }

                     MessageBox.Show("Dati inseriti correttamente!");

            }
            catch(Exception ex)
                 {
                    MessageBox.Show(ex.Message);
                 }
        }
    }
}