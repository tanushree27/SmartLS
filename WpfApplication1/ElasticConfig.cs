using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Nest;

namespace WpfApplication1
{
    public static class ElasticConfig
    {
        public static string IndexName
        {
            get { return "stackoverflow";  }
        }

        public static string ElastisearchUrl
        {
            get { return "http://localhost:9200"; }
        }

        public static IElasticClient GetClient()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("stackoverflow");
            return new ElasticClient(settings);
        }
    }
}
