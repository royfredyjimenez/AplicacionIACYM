using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos;

namespace CapaNegocio
{
    public class NEmpleados
    {

        #region CRUD de Datos
        //Método Insertar que llama al método Insertar de la clase DEmpleados de la CapaDatos
        public static string Insertar(string LastName, string FirstName, DateTime BirthDate, string Address, string Country, byte[] Imagen, string accesso, string usuario, string password)
        {
            DEmpleados emp = new DEmpleados();
            emp.LastName = LastName;
            emp.FirstName = FirstName;
            emp.BirthDate = BirthDate;
            emp.Address = Address;
            emp.Country = Country;
            emp.Imagen = Imagen;
            emp.Accesso = accesso;
            emp.Usuario = usuario;
            emp.Password = password;
            return emp.Insertar(emp);
        }
        // Metodo Editar que llama al metodo Editar de la Clase DEmpleados

        public static string Editar(int EmployeeID, string apellidos, string nombre, DateTime BirthDate, string direccion, string pais, byte[] imagen, string acceso, string password)
        {

            DEmpleados emp = new DEmpleados();
            emp.EmployeeID = EmployeeID;
            emp.LastName = apellidos;
            emp.FirstName = nombre;
            emp.BirthDate = BirthDate;
            emp.Address = direccion;
            emp.Country = pais;
            emp.Imagen = imagen;
            emp.Accesso = acceso;
            emp.Password = password;
            return emp.Editar(emp);
        }

        //Método Eliminar que llama al método Eliminar de la clase DTrabajador
        //de la CapaDatos
        public static string Eliminar(int EmployeeID)
        {
            DEmpleados emp = new DEmpleados();
            emp.EmployeeID = EmployeeID;
            return emp.Eliminar(emp);
        }


        #endregion

        #region Busqueda de Datos

        public static DataTable BuscarApellidos(string textobuscar)
        {
            DEmpleados emp = new DEmpleados();
            emp.TextoBuscar = textobuscar;
            return emp.BuscarApellidos(emp);
        }

        public static DataTable BuscarPais(string textobuscar)
        {
            DEmpleados emp = new DEmpleados();
            emp.TextoBuscar = textobuscar;
            return emp.BuscarPais(emp);
        }

        #endregion Busqueda de Datos

        #region Anexos

        //Método Mostrar que llama al método Mostrar de la clase DEmpleados
        public static DataTable Mostrar()
        {
            return new DEmpleados().Mostrar();

        }

        public static DataTable Login(string Usuario, string Password)
        {
            DEmpleados emp = new DEmpleados();
            emp.Usuario = Usuario;
            emp.Password = Password;
            return emp.Login(emp);
        }

        #endregion



    }

}
