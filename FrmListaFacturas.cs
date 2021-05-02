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
    public partial class FrmListaFacturas : Form
    {
        /// <summary>
        /// Metodo para llenar el data grid view con la informacion de la base de datos
        /// </summary>
        private void LlenarGrid()
        {
            //DataGridVeiw
            Acceso_Datos Acceso = new Acceso_Datos();
            DataTable dt;
            string Tabla = "TBLFACTURA";
            dt = Acceso.CargarTabla(Tabla, "");
            dgFacturas.DataSource = dt;
        }

        public FrmListaFacturas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
                Acceso_Datos Acceso = new Acceso_Datos();
                DataTable dt;
                string Tabla = "TBLFACTURA";
                string Condicion = $"where DtmFecha like '{dtmFactura.Value:s}'";
                dt = Acceso.CargarTabla(Tabla, Condicion);
                dgFacturas.DataSource = dt;

        }

        private void FrmListaFacturas_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult Rta;
            Rta = MessageBox.Show("Desea salir de la edición ?", "MENSAJE DE ADVERTENCIA",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Rta == DialogResult.OK)
            {
                this.Close();
            }

        }
    }
}
