using AccesoDatos;
using System.Data;


namespace CapaNegocio
{
    public class NCategoria
    {

        #region CRUD de Datos

        //Método Insertar que llama al método Insertar de la clase DEmpleados de la CapaDatos
        public static string Insertar(string CategoryName, string Description, byte[] Picture)
        {

            DCategoria cat = new DCategoria();
            cat.CategoryName = CategoryName;
            cat.Description = Description;
            cat.Picture = Picture;
            return cat.Insertar(cat);
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

        //Método Eliminar que llama al método Eliminar de la clase DEmpleados
        //de la CapaDatos
        public static string Eliminar(int CategoryID)
        {
            DCategoria cat = new DCategoria();
            cat.CategoryId = CategoryID;
            return cat.Eliminar(cat);
        }


        #endregion

        #region Anexos
        //Método Mostrar que llama al método Mostrar de la clase DEmpleados
        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();

        }
        #endregion

        #region Busqueda de Datos

        public static DataTable BuscarCategoria(string textobuscar)
        {
            DCategoria cat = new DCategoria();
            cat.TextoBuscar = textobuscar;
            return cat.BuscarCategoria(cat);
        }

        #endregion




    }
}
