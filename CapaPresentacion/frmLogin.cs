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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            lblName.Text = DateTime.Now.ToString();

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataTable Datos = CapaNegocio.NEmpleados.Login(this.txtUsuario.Text, this.txtPass.Text);

            //Evaluar si existe el Usuario
            if (Datos.Rows.Count == 0)
            {
                MessageBox.Show("NO Tiene Acceso al Sistema", "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                frm.Apellidos= Datos.Rows[0][0].ToString();
                frm.Nombre = Datos.Rows[0][1].ToString();
                frm.Acceso = Datos.Rows[0][2].ToString();
                frm.Usuario = Datos.Rows[0][3].ToString();


                frm.Show();
                this.Hide();

            }


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblName.Text = DateTime.Now.ToString();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
