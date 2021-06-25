using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
namespace DAL
{
    public class ConnectionManager
    {
        internal DbConnection _conexion;
        public ConnectionManager(string connectionString, string proveedor)
        {
            DbProviderFactory factoria = DbProviderFactories.GetFactory(proveedor);
            _conexion = factoria.CreateConnection();
               
            _conexion.ConnectionString = connectionString;
          
        }
        public void Open()
        {
            _conexion.Open();
        }
        public void Close()
        {
            _conexion.Close();
        }
    }
}

