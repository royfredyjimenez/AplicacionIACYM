using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using System.Data;

namespace CapaNegocio
{
   public class NProducto
    {
        #region CRUD de Datos

        ////Método Insertar que llama al método Insertar de la clase DEmpleados de la CapaDatos
        public static string Insertar(string CategoryName, string Description, byte[] Picture)
        {

            DProducto prod = new DProducto();
            //cat.CategoryName = CategoryName;
            //cat.Description = Description;
            //cat.Picture = Picture;
            return prod.Insertar(prod);
        }
        // Metodo Editar que llama al metodo Editar de la Clase DEmpleados

        //public static string Editar(int EmployeeID, string apellidos, string nombre, string direccion, string pais, byte[] imagen, string acceso, string password)
        //{

        //    //DEmpleados emp = new DEmpleados();
        //    //emp.EmployeeID = EmployeeID;
        //    //emp.LastName = apellidos;
        //    //emp.FirstName = nombre;
        //    //emp.Address = direccion;
        //    //emp.Country = pais;
        //    //emp.Imagen = imagen;
        //    //emp.Accesso = acceso;
        //    //emp.Password = password;
        //    //return emp.Editar(emp);
        //}

        //Método Eliminar que llama al método Eliminar de la clase DProducto de la capa de datos

        public static string Eliminar(int ProductID)
        {
            DProducto prod = new DProducto();
            prod.ProductID = ProductID;
            return prod.Eliminar(prod);
        }


        #endregion

        #region Anexos
        //Método Mostrar que llama al método Mostrar de la clase DEmpleados
        public static DataTable Mostrar()
        {
            return new DProducto().Mostrar();

        }
        #endregion

        #region Busqueda de Datos

        //public static DataTable BuscarCategoria(string textobuscar)
        //{
        //    DProducto prod = new DProducto();
        //    prod.TextoBuscar = textobuscar;
        //    return prod.BuscarCategoria(prod);
        //}

        #endregion



    }
}
