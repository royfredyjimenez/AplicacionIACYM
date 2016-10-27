using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;



namespace AccesoDatos
{
    public class Conexion
    {
       public static  string cadena = AplicacionIACYM.Properties.Settings.Default.cn;

    }
}
