using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARAALVAREZLOPEZ.Helpers
{
    public class HelperConfiguration
    {
        public static string GetConnectionString()
        {
            //True significa que cuando cambie le fichero json lo vuelvo a cargar
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("json1.json", true, true);
            IConfigurationRoot config = builder.Build();
            string connectionString = config["SqlEx"];
            return connectionString;
        }
    }
}
