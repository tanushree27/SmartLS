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
using Nest;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Resultpage.xaml
    /// </summary>
    public partial class Resultpage : Page
    {
        private readonly ElasticClient elasticClient;
        private string query;
   
        public T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }
       
        public Resultpage(string q1)
        {

            InitializeComponent();

            Results[] ans = InitializeArray<Results>(10) ;
            query = "";


        var node = new Uri("http://localhost:9200");
            ConnectionSettings settings = new ConnectionSettings(node);
            elasticClient = new ElasticClient(settings);
            char [] query1 = q1.ToCharArray();
            char[] que = new char [query1.Length];

            int k = 0, fg = 0;
            for (int j = 0; j != query1.Length; j++)
            {
                if ((query1[j] >= 'a' && query1[j] <= 'z') || (query1[j] >= 'A' && query1[j] <= 'Z'))
                {
                    if (fg == 1)
                    {
                        que[k] = ' ';
                        k++;
                        fg = 0;
                    }
                    que[k] = query1[j];
                    k++;
                }
                else if (k > 0)
                    fg = 1;
                    
            }
            //que[k] = '\0';
            
            
            string query2 = new string(que);

            query = query2.Substring(0, k);

            queryBlock.Text = "Query: " + query;
           // MessageBox.Show(query.Length + query);

            var response = elasticClient.Search<documents>(s => s
                .Index("project")
                .Type("doc")
                .Query(q => q.QueryString(qs => qs.Query(query+"*"))));
            
            int i = 0;
        
                foreach (var hit in response.Hits)
                {
                    ans[i].Fill(hit.Source.file.fileName.ToString(), hit.Source.file.url.ToString(), hit.Source.file.extension.ToString(), (hit.Source.file.filesize / 1000));
                    mainStack.Children.Add(ans[i].st);
                    i++;
                    if (i == 10)
                         break;
                }
                count.Text = "(Results : " + i.ToString() + ")";
            if (i == 0)
            {
                Results a = new Results();
                a.Fill("NO RESULTS FOUND", "", "", 0);
                mainStack.Children.Add(a.st);
            }

        }    
    }

   // class Result
    //{
    //}
}


