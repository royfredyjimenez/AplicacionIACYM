using AccesoDatos;
using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManejoDatos
{
    public class MD_Empleados
    {

        AccesoDatos.Conexion oAccesDatos = new AccesoDatos.Conexion();

        public int GuardarEmpleado(EntidadUsuario  oEmpleado)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append("Employees( ");
            sb.Append("LastName,");
            sb.Append("FirstName,");
            sb.Append("Address,");
            sb.Append("Country ");
            sb.Append(" ) VALUES ( ");
            sb.Append("'" + oEmpleado.LastName  + "'");
            sb.Append("'" + oEmpleado.FirstName + "'");
            sb.Append("'" + oEmpleado.Address+ "'");
            sb.Append("'" + oEmpleado.Country + "'");
            sb.Append(")");



            try
            {
                oAccesDatos.Open();
                int guardo = oAccesDatos.ExecuteNonQuery( CommandType.Text , sb.ToString());
                return guardo;



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                oAccesDatos.Close();

            }


        }



    }
}
