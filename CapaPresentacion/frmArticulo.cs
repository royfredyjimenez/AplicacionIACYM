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
    public partial class frmArticulo : Form
    {
        public frmArticulo()
        {
            InitializeComponent();
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
          //  this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            //this.OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {

            this.BuscarNombre();



        }
    }
}
