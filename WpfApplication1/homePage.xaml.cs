using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;
using Nest;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for homePage.xaml
    /// </summary>
    public partial class homePage : System.Windows.Controls.Page
    {
        public homePage()
        {
            InitializeComponent();
            
        }


      //  private readonly IElasticClient client;

        private void openfile()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Pictures|*.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.dib;*.png"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                querryBox.Text = filename;
                querryBox.IsEnabled = false;
                ocrResultButton.IsEnabled = true;

            }
        }


        private string resultByImg()
        {
            string query;
            try
            {
                using (var engine = new TesseractEngine(@"../../tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(querryBox.Text))
                    {
                        using (var page = engine.Process(img))
                        {
                            query = page.GetText(); //Using tesseract to extract text from image
                            
                        }
                    }
                }

                return query;
  
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                return "";
            }
        }

        private void queryresult_Click(object sender, RoutedEventArgs e)
        {
            string query = querryBox.Text;
           
            try
            {
                if (querryBox.IsEnabled == true)
                    this.NavigationService.Navigate(new Resultpage(query));
                else
                {
                    query = resultByImg();  //Tesseract's extracted text passed as query
                    this.NavigationService.Navigate(new Resultpage(query));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void typeButton_Click(object sender, RoutedEventArgs e)  //Switch between text and image query
        {
            if (querryBox.IsEnabled == true)
            {
                openfile();
            }
            else
            {
                image.Source = null;
                querryBox.Text = "";
                querryBox.IsEnabled = true;
                ocrResultButton.IsEnabled = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
         
           querryBox.Text = "";
           image.Source = null;
        }

        private void ocrResultButton_Click(object sender, RoutedEventArgs e) //only tesseract's result
        {
            this.NavigationService.Navigate (new ocrPage(resultByImg()));
        }
    }
}

