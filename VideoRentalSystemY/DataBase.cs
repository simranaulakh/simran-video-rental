using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystemY
{
    public class DataBase
    {
        //global declaration of the variable 
       public   SqlConnection connection;
        public String connection_String = "Data Source=LAPTOP-UNJTSKGF\\SQLEXPRESS;Initial Catalog=VideoRentalSystemY;Integrated Security=True";
        public SqlCommand command;
        public SqlDataReader Datareader;
    }
}
