using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
   class DATABASE1
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=ADCLG1; Initial Catalog=Vaschishina_MSServer; Integrated Security=True;Encrypt=True;TrustServerCertificate=True;
");
        public void openConnection()
        {
            sqlConnection.Open();
        }
        public void closeConnection()
        {
            sqlConnection.Close();
        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
    
}
