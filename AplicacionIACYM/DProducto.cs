using System;
using System.Data;
using System.Data.SqlClient;


namespace AccesoDatos
{
    public class DProducto
    {
        #region Atributos
        private int _ProductID; //Codigo del producto
        private string _ProductName;  // Nombre del producto
        private int _SupplierID; //Codigo del proveedor
        private int _CategoryID;   // Codigo de la Categoria
        private string _QuantityPerUnit;
        private int _UnitPrice;
        private int _UnitsInStock;
        private int _UnitsOnOrder;
        private string _TextoBuscar;


        public string ProductName
        {
            get
            {
                return _ProductName;
            }

            set
            {
                _ProductName = value;
            }
        }

        public int CategoryID
        {
            get
            {
                return _CategoryID;
            }

            set
            {
                _CategoryID = value;
            }
        }

        public string QuantityPerUnit
        {
            get
            {
                return _QuantityPerUnit;
            }

            set
            {
                _QuantityPerUnit = value;
            }
        }



        public int UnitsInStock
        {
            get
            {
                return _UnitsInStock;
            }

            set
            {
                _UnitsInStock = value;
            }
        }

        public int UnitsOnOrder
        {
            get
            {
                return _UnitsOnOrder;
            }

            set
            {
                _UnitsOnOrder = value;
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

        public int ProductID
        {
            get
            {
                return _ProductID;
            }

            set
            {
                _ProductID = value;
            }
        }

        public int UnitPrice
        {
            get
            {
                return _UnitPrice;
            }

            set
            {
                _UnitPrice = value;
            }
        }

        public int SupplierID
        {
            get
            {
                return _SupplierID;
            }

            set
            {
                _SupplierID = value;
            }
        }

        #endregion

        #region Constructores
        //Constructores
        public DProducto()
        {

        }

        public DProducto(int ProductID , string ProductName , int SupplierID , int CategoryID , string QuantityPerUnit , int UnitPrice , int UnitsInStock  , int UnitsOnOrder , string TextoBuscar)
        {
            this.ProductID        = ProductID;
            this.ProductName      = ProductName;
            this.SupplierID       = SupplierID;
            this.CategoryID       = CategoryID;
            this.QuantityPerUnit  = QuantityPerUnit;
            this.UnitPrice        = UnitPrice;
            this.UnitsInStock     = UnitsInStock;
            this.UnitsOnOrder     = UnitsOnOrder;
            this.TextoBuscar      = TextoBuscar;
        }

        #endregion

        #region CRUD de Datos
        public string Insertar(DProducto Producto)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "INSERT INTO [dbo].[Products] ( ProductName , SupplierID , CategoryID , QuantityPerUnit ,UnitPrice , UnitsInStock ,UnitsOnOrder) ";
                cadenasql = cadenasql + " VALUES  ('" + Producto.ProductName + "', ";
                cadenasql = cadenasql + ""+ Producto.SupplierID + ",";
                cadenasql = cadenasql + ""+ Producto.CategoryID + ",";
                cadenasql = cadenasql + ""+ Producto.QuantityPerUnit + ",";
                cadenasql = cadenasql + ""+ Producto.UnitPrice + ",";
                cadenasql = cadenasql + ""+ Producto.UnitsOnOrder + ")";

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
        
        //Método Eliminar
        public string Eliminar(DProducto Producto)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                SqlCon.Open();

                string cadenasql = string.Empty;
                cadenasql = "DELETE FROM [dbo].[Products] WHERE ProductID =" + Producto.ProductID + "";
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

        //Método Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("[dbo].[Products]");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;
                string cadenasql = string.Empty;
                cadenasql = " SELECT TOP 100 ProductID , ProductName , SupplierID , CategoryID , QuantityPerUnit , UnitPrice , UnitsInStock , UnitsOnOrder FROM[dbo].[Products] Where Discontinued = 0 ORDER BY ProductID";

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


 



    }
}
