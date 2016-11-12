using System;
using System.Windows.Forms;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class frmVistaCategoria_Producto : Form
    {
        public frmVistaCategoria_Producto()
        {
            InitializeComponent();
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dgvCategoria.Columns[0].Visible = false; //checkbox
            this.dgvCategoria.Columns[1].Visible = false; //CategoryID
            this.dgvCategoria.Columns[4].Visible = false; //Picture
        }

        //Método Mostrar
        private void Mostrar()
        {
            this.dgvCategoria.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvCategoria.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarCategoria()
        {
            this.dgvCategoria.DataSource = NCategoria.BuscarCategoria(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dgvCategoria.Rows.Count);
        }

        private void frmVistaCategoria_Producto_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }


        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }

        private void dgvCategoria_DoubleClick(object sender, EventArgs e)
        {
            frmProducto form = frmProducto.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(this.dgvCategoria.CurrentRow.Cells["CategoryID"].Value);
            par2 = Convert.ToString(this.dgvCategoria.CurrentRow.Cells["CategoryName"].Value);
            form.setCategoria(par1, par2);
           // this.Hide();
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            this.BuscarCategoria();
        }
    }
}
