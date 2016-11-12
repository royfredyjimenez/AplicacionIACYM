using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace AccesoDatos
{
    public class DEmpleados
    {

        #region Declaracion de Variables
        private int _EmployeeID;
        private string _LastName;
        private string _FirstName;
        private DateTime _BirthDate;
        private string _Address;
        private string _Country;
        private byte[] _Imagen;
        private string _Accesso;
        private string _Password;
        private string _Usuario;
        private string _TextoBuscar;

        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Country
        {
            get { return _Country; }

            set
            {
                _Country = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
            }
        }

        public string Accesso
        {
            get
            {
                return _Accesso;
            }

            set
            {
                _Accesso = value;
            }
        }

        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return _BirthDate;
            }

            set
            {
                _BirthDate = value;
            }
        }

        public byte[] Imagen
        {
            get
            {
                return _Imagen;
            }

            set
            {
                _Imagen = value;
            }
        }


        #endregion

        #region Constructores
        public DEmpleados()
        {

        }

        public DEmpleados(int EmployeeID, string LastName, string FirstName, DateTime BirthDate, string Address, string Country, byte[] Imagen, string Accesso, string Usuario, string Password, string TextoBuscar)
        {
            this.EmployeeID = EmployeeID;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.BirthDate = BirthDate;
            this.Address = Address;
            this.Country = Country;
            this.Imagen = Imagen;
            this.Accesso = Accesso;
            this.Usuario = Usuario;
            this.Password = Password;
            this.TextoBuscar = TextoBuscar;

        }

        #endregion

        #region CRUD de Datos
        //Método Insertar

        public string Insertar(DEmpleados Empleados)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
                //Año - mes - dia
                string cadenasql = string.Empty;
                cadenasql = "INSERT INTO [dbo].[Employees] ( LastName , Firstname , BirthDate , Address , Country , Photo , accesso , usuario , password) ";
                cadenasql = cadenasql + " VALUES  ('"+ Empleados.LastName + "', ";
                cadenasql = cadenasql + "' " + Empleados.FirstName + "',";
                cadenasql = cadenasql + "CAST('" + Empleados.BirthDate.ToShortDateString() + "' AS DATETIME),";
                cadenasql = cadenasql + "'" + Empleados.Address + "' ,";
                cadenasql = cadenasql + "'" + Empleados.Country + "', ";
                cadenasql = cadenasql + "'" + Empleados.Imagen + "', ";
                cadenasql = cadenasql + "'" + Empleados.Accesso + "', ";
                cadenasql = cadenasql + "'" + Empleados.Usuario + "', ";
                cadenasql = cadenasql + "'" + Empleados.Password + "') ";

                //Establecer el Comando
                SqlCommand comando = SqlCon.CreateCommand();

                comando.CommandText = cadenasql;

                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Actualizo el Registro";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            //Ejecutamos nuestro comando

            return rpta;

        }

        //Método Editar
        public string Editar(DEmpleados Empleados)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "UPDATE [dbo].[Employees] SET LastName  = '" + Empleados.LastName + "',";
                cadenasql = cadenasql + "FirstName = '" + Empleados.FirstName + "',";
                cadenasql = cadenasql + "BirthDate = '" + Empleados.BirthDate.ToShortDateString() + "',";
                cadenasql = cadenasql + "Address = '" + Empleados.Address + "',";
                cadenasql = cadenasql + "Country = '" + Empleados.Country + "',";
                cadenasql = cadenasql + "Photo = '" + Empleados.Imagen + "',";
                cadenasql = cadenasql + "accesso = '" + Empleados.Accesso + "',";
                cadenasql = cadenasql + "password = '" + Empleados.Password + "'";
                cadenasql = cadenasql + "WHERE EmployeeID =  " + Empleados.EmployeeID + "";

                //Establecer el Comando
                SqlCommand comando = SqlCon.CreateCommand();

                comando.CommandText = cadenasql;


                //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Actualizo el Registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }

        //Método Eliminar
        public string Eliminar(DEmpleados Empleados)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "DELETE FROM [dbo].[Employees] WHERE EmployeeID =" + Empleados.EmployeeID + "";
                //Establecer el Comando

                SqlCommand comando = SqlCon.CreateCommand();
                comando.CommandText = cadenasql;

               //Ejecutamos nuestro comando

                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }


        #endregion

        #region Busqueda de Datos
        //Método BuscarApellidos

        public DataTable BuscarApellidos(DEmpleados Empleados)
        {
            DataTable DtResultado = new DataTable("[dbo].[Employees]");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
                string cadenasql = string.Empty;
                cadenasql = "SELECT EmployeeID , LastName , FirstName , Title , BirthDate, Address , City , Region , Country , Photo , accesso , usuario, password FROM [dbo].[Employees] ";
                cadenasql = cadenasql + "WHERE LastName LIKE '" + Empleados.TextoBuscar + "%'";


                //Establecer el Comando

                SqlCommand comando = new SqlCommand();

                comando.Connection = SqlCon;
                comando.CommandText = cadenasql;
                SqlDataAdapter SqlDat = new SqlDataAdapter(comando);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();

            }
            return DtResultado;


        }

        //Método BuscarPais
        public DataTable BuscarPais(DEmpleados Empleados)
        {
            DataTable DtResultado = new DataTable("[dbo].[Employees]");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
                string cadenasql = string.Empty;
                cadenasql = "SELECT EmployeeID , LastName , FirstName , Title , Address , City , Region , Country , Photo , accesso , usuario, password FROM [dbo].[Employees] ";
                cadenasql = cadenasql + "WHERE Country LIKE '" + Empleados.TextoBuscar + "%'";


                //Establecer el Comando

                SqlCommand comando = new SqlCommand();

                comando.Connection = SqlCon;
                comando.CommandText = cadenasql;
                SqlDataAdapter SqlDat = new SqlDataAdapter(comando);
                SqlDat.Fill(DtResultado);


            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();

            }
            return DtResultado;

        }


        #endregion

        #region Anexos
        //Método Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("[dbo].[Employees]");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                string cadenasql = string.Empty;
                cadenasql = "SELECT TOP 100 EmployeeID , LastName , FirstName , Title ,BirthDate,  Address , City , Region , Country , Photo , accesso , usuario , password FROM [dbo].[Employees] ORDER BY LastName ASC";

                SqlCommand comando = new SqlCommand();
                comando.Connection = SqlCon;
                comando.CommandText = cadenasql;
                //   comando.ExecuteReader();
                SqlDataAdapter SqlDat = new SqlDataAdapter(comando);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }

        public DataTable Login(DEmpleados Empleados)
        {

            DataTable DtResultado = new DataTable("[dbo].[Employees]");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
                string cadenasql = string.Empty;
                cadenasql = "SELECT LastName , Firstname , Accesso , Usuario , Password FROM [dbo].[Employees] ";
                cadenasql = cadenasql + "WHERE Usuario   = '" + Empleados.Usuario + "'";
                cadenasql = cadenasql + " AND Password  = '" + Empleados.Password + "'";

                //Establecer el Comando

                SqlCommand comando = new SqlCommand();

                comando.Connection = SqlCon;
                comando.CommandText = cadenasql;
                SqlDataAdapter SqlDat = new SqlDataAdapter(comando);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {

                DtResultado = null;
            }
            finally
            {

                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();

            }
            return DtResultado;
        }


        #endregion


    }


}
