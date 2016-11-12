using System;
using System.Windows.Forms;
using CapaNegocio;
using System.IO;
using System.Drawing;

namespace CapaPresentacion
{
    public partial class frmCategoria : Form
    {

        private bool IsNuevo = false;
        private bool IsEditar = false;


        public frmCategoria()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtCategoryName, "Ingresa el nombre de la Categoria");
            this.ttMensaje.SetToolTip(this.txtDescription,  "Ingresa la descripcion de la Categoria");
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

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtCategoryName.ReadOnly = !valor;
            this.txtDescription.ReadOnly  = !valor;
        }

        //Habilitar los botones
        private void Botones()
        {
            this.txtCategoryID.ReadOnly = true;
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

        #region Carga de datos para el formulario
        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dgvCategorias.Columns[0].Visible = false;  //check
            this.dgvCategorias.Columns[1].Visible = false;  //EmployeeID
            this.dgvCategorias.Columns[4].Visible = false;  //Photo
        }

        private void Mostrar()
        {
           this.dgvCategorias.DataSource =  NCategoria.Mostrar();
           this.OcultarColumnas();
           lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvCategorias.Rows.Count);
        }


        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtCategoryName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;

        }

        private void frmCategoria_Load(object sender, EventArgs e)
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
        #endregion

        #region Busqueda de  Datos
        //Método BuscarNombre
        private void BuscarCategoria()
        {
            this.dgvCategorias.DataSource = NCategoria.BuscarCategoria(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvCategorias.Rows.Count);
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }

        #endregion

        #region Datagridview Categorias


        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.columIndex nro de columna  e.RowIndex que nro de fila
            if (e.ColumnIndex == dgvCategorias.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dgvCategorias.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

       private void dgvCategorias_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count > 0 && dgvCategorias.CurrentRow.Index < dgvCategorias.Rows.Count)
            {
              // Lleno las cajas de texto con el contenido del dgvempleado

                this.txtCategoryID.Text = Convert.ToString(this.dgvCategorias.CurrentRow.Cells["CategoryID"].Value);
                this.txtCategoryName.Text = Convert.ToString(this.dgvCategorias.CurrentRow.Cells["CategoryName"].Value);
                this.txtDescription.Text = Convert.ToString(this.dgvCategorias.CurrentRow.Cells["Description"].Value);

                if (this.dgvCategorias.CurrentRow.Cells["Picture"].Value == DBNull.Value)
                    pxImagen.Image = Helpers.ImageHelper.ObtenerCategoriaNoDisponible();

                else
                {
                    try
                    {


                        Byte[] imgbd = (Byte[])this.dgvCategorias.CurrentRow.Cells["Picture"].Value;

                        if (imgbd.Length < 78)
                        {

                            pxImagen.Image = Helpers.ImageHelper.ByteArrayToImage(imgbd);


                        }


                        else
                        {
                            this.pxImagen.DataBindings.Clear();
                            Binding bdPhoto = new Binding("Image", dgvCategorias.DataSource, "Picture");
                            bdPhoto.Format += new ConvertEventHandler(this.PictureFormat); //bmp
                            pxImagen.DataBindings.Add(bdPhoto);
                            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }

            }
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

                MemoryStream ms = new System.IO.MemoryStream();
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

        #endregion

        #region CRUD de Categoria

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtCategoryName.Focus();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtCategoryName.Text == string.Empty || this.txtDescription.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtCategoryName, "Ingrese un Valor");
                    errorIcono.SetError(txtDescription, "Ingrese un Valor");

                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtCategoryName.Text, this.txtDescription.Text.Trim(),
                            //Helpers.ImageHelper.ImageToByteArray(pxImagen.Image)
                            imagen

                            );

                    }
                    else
                    {
                        //rpta = NArticulo.Editar(this.txtCategoryName.Text, this.txtDescription.Text.Trim(),
                        //    this.txtDescripcion.Text.Trim(), imagen, Convert.ToInt32(this.txtIdcategoria.Text),
                        //    Convert.ToInt32(this.cbIdpresentacion.SelectedValue));
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
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ////Si no se ha seleccionado un empleado no puede modificar
            //if (!this.txtIdEmpleado.Text.Equals(""))
            //{
            //    this.IsEditar = true;
            //    this.Botones();
            //}
            //else
            //{
            //    this.MensajeError("Debe de buscar un registro para Modificar");
            //}

        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {

            if (chkEliminar.Checked)
            {
                this.dgvCategorias.Columns[0].Visible = true; //check
                this.dgvCategorias.Columns[1].Visible = true; //CategoryID
            }
            else
            {
                this.dgvCategorias.Columns[0].Visible = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Sistema Aplicacion IACYMCNC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dgvCategorias.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NCategoria.Eliminar(Convert.ToInt32(Codigo));

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }


        #endregion

        #region Anexos

        private void btnCargar_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Archivo JPG|*.jpg|All Files (*.*)|*.*";
            DialogResult result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(file.FileName);
            }


        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //FrmReporteCategoria frm = new FrmReporteArticulos();
            //frm
            //frm.Texto = txtBuscar.Text;
            //frm.ShowDialog();
        }


        #endregion



    }
}
