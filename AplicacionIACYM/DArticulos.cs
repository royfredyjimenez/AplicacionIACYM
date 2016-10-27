using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace AplicacionIACYM
{
   public class DArticulos
    {
        #region Atributos encapsulados
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


        //Constructores
        public DArticulos()
        {

        }

        public DArticulos(int ProductID , string ProductName , int SupplierID , int CategoryID , string QuantityPerUnit , int UnitPrice , int UnitsInStock  , int UnitsOnOrder , string TextoBuscar)
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

        public string Insertar(DArticulos Articulos)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = AccesoDatos.Conexion.cadena;

                SqlCon.Open();
 
                string cadenasql = string.Empty;
                cadenasql = "INSERT INTO [dbo].[Products] ( ProductName , SupplierID , CategoryID , QuantityPerUnit ,UnitPrice , UnitsInStock ,UnitsOnOrder) ";
                cadenasql = cadenasql + " VALUES  ('" + Articulos.ProductName + "', ";
                cadenasql = cadenasql + ""+ Articulos.SupplierID + ",";
                cadenasql = cadenasql + ""+ Articulos.CategoryID + ",";
                cadenasql = cadenasql + ""+ Articulos.QuantityPerUnit + ",";
                cadenasql = cadenasql + ""+ Articulos.UnitPrice + ",";   
                cadenasql = cadenasql + ""+ Articulos.UnitsOnOrder + ")";

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

      







    }
}
