using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Utilities
{
    public class UtilititesStartup
    {
        public static string Cadena { get; set; }

        public static void CargarDatosIniciales(IConfiguration configuration)
        {
            var baseDatos = configuration.GetConnectionString("DataSource");
            var catalog = configuration.GetConnectionString("Catalog");
            var user = configuration.GetConnectionString("User");
            var pass = configuration.GetConnectionString("Pass");

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = baseDatos,
                InitialCatalog = catalog,
                UserID = user,
                Password = pass,
                TrustServerCertificate = true
            };

            Cadena = sqlConnectionStringBuilder.ConnectionString.ToString();
        }
    }
}
