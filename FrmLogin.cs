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
            string Respuesta = ""; // creamos variable para controlar si encontró el usuario en la base de datos
            if (txtUsuario.Text != "" && txtPassword.Text != string.Empty) // verifico que el usuario y la clave del los campos del formulario no estén vacíos
            {
                Acceso_Datos Acceso = new Acceso_Datos(); // creamos un objeto con la clase Acceso_datos
                Respuesta = Acceso.ValidarUsuario(txtUsuario.Text, txtPassword.Text);
                if (Respuesta != "")
                {
                    MessageBox.Show("Bienvenido : " + Respuesta);
                    FrmPrincipal frmppal = new FrmPrincipal(); //Creamos el objeto del formulario FrmPrincipal
                    this.Hide(); // Ocultamos el formulario login
                    frmppal.Show(); // Mostramos el formulario principal
                }
                else
                {
                    MessageBox.Show("USUARIOS Y CLAVE NO ENCONTRADOS");
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    txtPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Debes ingresar un usuario y una clave");
            }
        }
    }
}

