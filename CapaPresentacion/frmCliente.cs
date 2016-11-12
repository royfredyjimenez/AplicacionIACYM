using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmCliente : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public frmCliente()
        {
            InitializeComponent();
            //this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre del Cliente");
            //this.ttMensaje.SetToolTip(this.txtApellidos, "Ingrese los Apellidos del Cliente");
            //this.ttMensaje.SetToolTip(this.txtDireccion, "Ingrese la dirección del Cliente");
            //this.ttMensaje.SetToolTip(this.txtNum_Documento, "Ingrese el número de documento del Cliente");



        }

        #region Busqueda
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

        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            //this.txtNombre.Text = string.Empty;
            //this.txtApellidos.Text = string.Empty;
            //this.txtNum_Documento.Text = string.Empty;
            //this.txtDireccion.Text = string.Empty;
            //this.txtTelefono.Text = string.Empty;
            //this.txtEmail.Text = string.Empty;
            //this.txtIdcliente.Text = string.Empty;

        }

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            //this.txtNombre.ReadOnly = !valor;
            //this.txtApellidos.ReadOnly = !valor;
            //this.txtDireccion.ReadOnly = !valor;
            //this.cbTipo_Documento.Enabled = valor;
            //this.txtNum_Documento.ReadOnly = !valor;
            //this.txtTelefono.ReadOnly = !valor;
            //this.txtEmail.ReadOnly = !valor;
            //this.txtIdcliente.ReadOnly = !valor;
        }

        //Habilitar los botones
        private void Botones()
        {
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

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        #endregion
        //Método Mostrar
        private void Mostrar()
        {
            //this.dataListado.DataSource = NCliente.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }


        private void frmCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }




    }
}
