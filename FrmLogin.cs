using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Facturacion
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != String.Empty && txtPassword.Text != String.Empty)
            {
                if (txtUsuario.Text == "user" && txtPassword.Text == "123")
                {
                    FrmPrincipal frmprincipal = new FrmPrincipal();
                    this.Hide();
                    frmprincipal.Show();
                }
            }
        }
    }
}
