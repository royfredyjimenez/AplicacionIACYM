using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Windows.Forms;
using System.Reflection;
using AccesoDatos;
using CapaNegocio;
using Helpers;


namespace CapaPresentacion
{
    public partial class frmEmpleado : Form
    {

        private bool IsNuevo = false;
        private bool IsEditar = false;

        public frmEmpleado()
        {
              InitializeComponent();
              this.ttMensaje.SetToolTip(this.txtNomb, "Ingrese el Nombre del Empleado");
              this.ttMensaje.SetToolTip(this.txtApellido, "Ingrese el Apellido del Empleado");
              this.ttMensaje.SetToolTip(this.txtDireccion, "Ingrese la Direccion del Empleado");
              this.ttMensaje.SetToolTip(this.txtPais, "Ingrese el Pais del empleado");
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

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtNomb.ReadOnly = !valor;
            this.txtApellido.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtPais.ReadOnly = !valor;
            this.txtAcceso.ReadOnly = !valor;
            this.txtClave.ReadOnly = !valor;
        }

        //Habilitar los botones
        private void Botones()
        {
            this.txtIdEmpleado.ReadOnly = true;
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
            this.dgvEmpleados.Columns[0].Visible = false;  //check
            this.dgvEmpleados.Columns[1].Visible = false;  //EmployeeID
            this.dgvEmpleados.Columns[10].Visible = false; //Photo
        }
        //Método Mostrar
        private void Mostrar()
        {
            this.dgvEmpleados.DataSource = NEmpleados.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvEmpleados.Rows.Count);
        }

        #region Busqueda de Datos
        private void BuscarApellidos()
        {
            this.dgvEmpleados.DataSource = NEmpleados.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dgvEmpleados.Rows.Count);
        }


        private void BuscarPais()
        {
            this.dgvEmpleados.DataSource = NEmpleados.BuscarPais(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dgvEmpleados.Rows.Count);
        }

        #endregion


        private void frmEmpleado_Load(object sender, EventArgs e)
        {
            //Para ubicar al formulario en la parte superior del contenedor
            this.Top = 0;
            this.Left = 0;
            //Le decimos al DataGridView que no auto genere las columnas
            //this.datalistado.AutoGenerateColumns = false;
            //Llenamos el DataGridView con la informacion
            //de todos nuestros Trabajadores
            this.Mostrar();
            //Deshabilita los controles
            this.Habilitar(false);
            //Establece los botones
            this.Botones();

        }

       //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtIdEmpleado.Text = string.Empty;
            this.txtNomb.Text = string.Empty;
            this.txtApellido.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtPais.Text = string.Empty;

        }
        #region Botones
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
            else if (cbBuscar.Text.Equals("Country"))
            {
                this.BuscarPais();
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNomb.Focus();

        }


        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Archivo JPG|*.jpg";
            DialogResult result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(file.FileName);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNomb.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtNomb, "Ingrese un valor");
                }
                else
                {
                    //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NEmpleados.Insertar(this.txtApellido.Text.Trim(), this.txtNomb.Text.Trim(), dtFecha_Nacimiento.Value,
                        this.txtDireccion.Text.Trim(), this.txtPais.Text.Trim().ToUpper(), Helpers.ImageHelper.ImageToByteArray(pxImagen.Image),
                        this.txtAcceso.Text.Trim(), this.txtUsuario.Text.Trim(), this.txtClave.Text.Trim()
                                     );

                    }
                    else // editar empleados
                    {
                        rpta = NEmpleados.Editar(Convert.ToInt32(this.txtIdEmpleado.Text),
                         this.txtApellido.Text,
                         this.txtNomb.Text,
                         this.txtDireccion.Text,
                         this.txtPais.Text.Trim().ToUpper(),
                         Helpers.ImageHelper.ImageToByteArray(pxImagen.Image),
                         this.txtAcceso.Text.Trim(),
                        // this.txtUsuario.Text.Trim(),
                         this.txtClave.Text);

                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();

                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            //Si no se ha seleccionado un empleado no puede modificar
            if (!this.txtIdEmpleado.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
            }
            else
            {
                this.MensajeError("Debe de buscar un registro para Modificar");
            }


        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dgvEmpleados.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NEmpleados.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Eliminó Correctamente el registro");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }



        }

        #endregion

        private void dgvEmpleados_DoubleClick(object sender, EventArgs e)
        {
// Lleno las cajas de texto con el contenido del dgvempleado
           this.txtIdEmpleado.Text  =  Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["EmployeeID"].Value);
           this.txtNomb.Text = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["FirstName"].Value);
           this.txtApellido.Text = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["LastName"].Value);
           this.txtDireccion.Text = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["Address"].Value);
           this.txtPais.Text = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["Country"].Value);
           this.txtAcceso.Text = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["accesso"].Value);
           this.txtClave.Text  = Convert.ToString(this.dgvEmpleados.CurrentRow.Cells["password"].Value);

            // Para mostrar la imagen de la base de datos en el picture box.

            if (this.dgvEmpleados.CurrentRow.Cells["Photo"].Value  == DBNull.Value)
            // Si se 
                 pxImagen.Image = Helpers.ImageHelper.ObtenerImagenNoDisponible();

            else
            {
                try
                {
                    this.pxImagen.DataBindings.Clear();
                    Binding bdPhoto = new Binding("Image", dgvEmpleados.DataSource, "Photo");
                    bdPhoto.Format += new ConvertEventHandler(this.PictureFormat);
                    pxImagen.DataBindings.Add(bdPhoto);
                    this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                catch (Exception ex)
                {
                  MessageBox.Show(ex.Message);

                }
            }

            //this.tabControl1.SelectedIndex = 1;


        }

         // Convierte los bits de imagen en un Objeto Bitmap que puede ser asignado a un PictureBox control
        private void PictureFormat(object sender, ConvertEventArgs e)
        {
            // e.Value es el valor original
            try
            {
                Byte[] img = (Byte[])e.Value;

                 //Normalmente, el campo BLOB contiene solo la imagen en si.
                //Desafortunadamente, este no es el caso con Northwind, en cuyas imagenes
                //estan prefijadas con una cabecera de 78 bytes. Entonces, Para poder crear
                //un objeto valido se deben evitar cargar estos bytes.

                    MemoryStream ms = new MemoryStream();
                    int offset = 78;
                    ms.Write(img, offset, img.Length - offset);
                    Bitmap bmp = new Bitmap(ms);
                    ms.Close();

                    e.Value = bmp;
          

             }
            catch (Exception ex)
            {
                  MessageBox.Show(ex.Message);
           
            }


        }

// selecciono una fila de mi dgvEmpleado
        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.columIndex nro de columna  e.RowIndex que nro de fila
            if (e.ColumnIndex == dgvEmpleados.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgvEmpleados.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }


        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dgvEmpleados.Columns[0].Visible = true; //check
                this.dgvEmpleados.Columns[1].Visible = true; //EmployeeID
            }
            else
            {
                this.dgvEmpleados.Columns[0].Visible = false;
            }
        }

       }



    }
