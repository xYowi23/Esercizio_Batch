using Batch.Models;
using Batch.Repo;
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
    }
}