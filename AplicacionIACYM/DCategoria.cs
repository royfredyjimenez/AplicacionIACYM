using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DCategoria
    {
        #region Declaracion de variables
        private int _CategoryId;
        private string _CategoryName;
        private string _Description;
        private byte[] _Picture;
        private string _TextoBuscar;

        public int CategoryId
        {
            get
            {
                return _CategoryId;
            }

            set
            {
                _CategoryId = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return _CategoryName;
            }

            set
            {
                _CategoryName = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public byte[] Picture
        {
            get
            {
                return _Picture;
            }

            set
            {
                _Picture = value;
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
        #endregion
        #region Constructores
        public DCategoria()
        {


        }

        public DCategoria(int CategoryId, string CategoryName, string Description, byte[] Picture , string TextoBuscar)
        {
            this.CategoryId    = CategoryId;
            this.CategoryName  = CategoryName;
            this.Description   = Description;
            this.Picture       = Picture;
            this.TextoBuscar   = TextoBuscar;

        }


        #endregion

        #region CRUD de datos

        public string Insertar(DCategoria Categoria)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "INSERT INTO [dbo].[Categories] (CategoryName , Description , Picture)";
                cadenasql = cadenasql + " VALUES ('" + Categoria.CategoryName + "', ";
                cadenasql = cadenasql + "' " + Categoria.Description + "',";
                cadenasql = cadenasql + "' " + Categoria.Picture + "') ";


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

        //Método Editar
        public string Editar(DCategoria Categoria)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "UPDATE [dbo].[Categories] SET CategoryName = '" + Categoria.CategoryName + "',";
                cadenasql = cadenasql + "Description = '" + Categoria.Description + "',";
                cadenasql = cadenasql + "Picture = '" + Categoria.Picture + "'";
                cadenasql = cadenasql + "WHERE CategoryID =  " + Categoria.CategoryId + "";

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
        public string Eliminar(DCategoria Categoria)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "DELETE FROM [dbo].[Categories] WHERE CategoryID =" + Categoria.CategoryId + "";
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

        #region Anexos

        //Método Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("[dbo].[Categories]");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                string cadenasql = string.Empty;
                cadenasql = "SELECT TOP 100 CategoryID , CategoryName , Description , Picture FROM [dbo].[Categories] ORDER BY CategoryName ASC";

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
        #endregion

        public DataTable BuscarCategoria(DCategoria Categoria)
        {
            DataTable DtResultado = new DataTable("[dbo].[Categories]");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
                string cadenasql = string.Empty;
                cadenasql = "SELECT CategoryID , CategoryName, Description , Picture FROM [dbo].[Categories] ";
                cadenasql = cadenasql + "WHERE CategoryName LIKE '" + Categoria.TextoBuscar + "%'";


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


    }


}


