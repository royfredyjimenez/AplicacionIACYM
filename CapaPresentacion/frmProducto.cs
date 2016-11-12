using System;
using System.Windows.Forms;
using CapaNegocio;
namespace CapaPresentacion
{
    public partial class frmProducto : Form
    {
        private bool IsNuevo = false;

        private bool IsEditar = false;

        public frmProducto()
        {
            InitializeComponent();




         }

        private static frmProducto _Instancia;

        public static frmProducto GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmProducto();
            }
            return _Instancia;
        }

        public void setCategoria(string idcategoria, string nombre)
        {
            this.txtCategoryID.Text = idcategoria;
            this.txtCategoryName.Text = nombre;
        }


        #region Mensajes

        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
        #region habilitacion de controles , botones de formulario
        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            //this.txtCodigo.Text = string.Empty;
            //this.txtNombre.Text = string.Empty;
            //this.txtDescripcion.Text = string.Empty;
            //this.txtIdcategoria.Text = string.Empty;
            //this.txtCategoria.Text = string.Empty;
            //this.txtIdarticulo.Text = string.Empty;
            //this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;
        }

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtProductName.ReadOnly = !valor;
            this.txtCategoryName.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtSupplierID.ReadOnly = !valor;

        }

        //Habilitar los botones
        private void Botones()
        {
            this.txtProductID.ReadOnly = true;
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }



        #endregion
        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dgvProducto.Columns[0].Visible = false;  //check
            //this.dgvProducto.Columns[1].Visible = false;  //EmployeeID
            //this.dgvEmpleados.Columns[10].Visible = false; //Photo
        }
        //Método Mostrar
        private void Mostrar()
        {
            this.dgvProducto.DataSource = NProducto.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvProducto.Rows.Count);
        }
        private void frmProducto_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        //Método BuscarProducto
        private void BuscarProducto()
        {
          //  this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            //this.OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarProducto();

        }

        private void frmProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmVistaCategoria_Producto form = new frmVistaCategoria_Producto();
            form.ShowDialog();

        }


    }
}
