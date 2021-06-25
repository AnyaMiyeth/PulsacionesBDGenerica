using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PulsacionBD
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GetProviderFactoryClasses();
            CrearDbConnection();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());

        }
        public static DbConnection CrearDbConnection()
        {
            DbConnection con = null;
            string cadenaCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string proveedor = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
            // Crear DbProviderFactory y DbConnection
            if (cadenaCon != null)
            {
                try
                {


                    DbProviderFactory factoria =
                    DbProviderFactories.GetFactory(proveedor);
                    con = factoria.CreateConnection();
                    con.ConnectionString = cadenaCon;
                    con.Open();
                    Console.WriteLine(con.State);
                }
                catch (Exception ex)
                {
                    con = null;
                    Console.WriteLine(ex.Message);
                }
            }
            return con;
        }
        public static DataTable GetProviderFactoryClasses()
        {
            // Retrieve the installed providers and factories.
            DataTable table = DbProviderFactories.GetFactoryClasses();

            // Display each row and column value.
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(row[column]);
                }
            }
            return table;
        }
    }
}
