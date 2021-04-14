using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace Sistema_Facturacion
{
    public partial class FrmPrincipal : MaterialForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Este metodo nos permite abrir un formulario en el panel contenedor
        /// </summary>
        /// <param name="formHijo"></param>
        private void AbrirForm(Form formHijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmClientes frmCliente = new FrmClientes();
            AbrirForm(frmCliente);
        }


        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProductos frmProductos = new FrmProductos();
            AbrirForm(frmProductos);
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FrmCategoria frmCategoria = new FrmCategoria();
            AbrirForm(frmCategoria);
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            FrmSeguridad frmSeguridad = new FrmSeguridad();
            AbrirForm(frmSeguridad);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            FrmEmpleado frmEmpleado = new FrmEmpleado();
            AbrirForm(frmEmpleado);
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FrmRoles frmRoles = new FrmRoles();
            AbrirForm(frmRoles);
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FrmFacturas frmFacturas = new FrmFacturas();
            AbrirForm(frmFacturas);
        }

        private void btnInformes_Click(object sender, EventArgs e)
        {
            FrmInformes frmInformes = new FrmInformes();
            AbrirForm(frmInformes);
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            FrmAyuda frmAyuda = new FrmAyuda();
            AbrirForm(frmAyuda);
        }

        private void btnAcerca_Click(object sender, EventArgs e)
        {
            FrmAcerca frmAcerca = new FrmAcerca();
            AbrirForm(frmAcerca);
        }

        private void TabOpcionesMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
