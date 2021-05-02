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
    public partial class FrmProductos : Form
    {
        /// <summary>
        /// Metodo para llenar el data grid view con la informacion de la base de datos
        /// </summary>
        private void LlenarGrid()
        {
            //DataGridVeiw
            Acceso_Datos Acceso = new Acceso_Datos();
            DataTable dt;
            string Tabla = "TBLPRODUCTO";
            dt = Acceso.CargarTabla(Tabla, "");
            dgProductos.DataSource = dt;

            //ComboBox
            Tabla = "TBLCATEGORIA_PROD";
            dt = Acceso.CargarTabla(Tabla, "");
            cboCategoria.DataSource = dt;
            cboCategoria.DisplayMember = "StrReferencia";
            cboCategoria.ValueMember = "IdCategoria";
        }

        //---<Funciones de Validacion>---

        /// <summary>
        /// Funcion para validar los campos del formulario
        /// </summary>
        /// <returns>true: si los datos estan validados</returns>
        private Boolean validar()
        {
            Boolean errorCampos = true;
            if (txtNombreProducto.Text == string.Empty)
            {
                MensajeError.SetError(txtNombreProducto, "debeingresar el nombre del producto");
                txtNombreProducto.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtNombreProducto, ""); }
            if (txtCodigoProducto.Text == "")
            {
                MensajeError.SetError(txtCodigoProducto, "debeingresar el documento");
                txtCodigoProducto.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtCodigoProducto, ""); }

            return errorCampos;
        }
        //---</Funciones de Validacion>---

        //---<Metodos de Actualizacion>---
        /// <summary>
        /// Metodo: Actualiza la base de datos con la informacion del formulario
        /// </summary>
        private void Actualizar()
        {
            if (validar())
            {
                try
                {
                    Acceso_Datos Acceso = new Acceso_Datos();
                    string sentencia = $"Exec Actualizar_Producto {Convert.ToInt32(txtIdProducto.Text)} , '{txtNombreProducto.Text}' , " +
                        $"'{txtCodigoProducto.Text}', {Convert.ToDouble(txtPrecioCompra.Text)}, {Convert.ToDouble(txtPrecioVenta.Text)} , " +
                        $"{cboCategoria.SelectedValue}, '{txtDetalle.Text}', '{txtImagenProducto.Text}', {Convert.ToInt32(txtStock.Text)}, " +
                        $"'{DateTime.Now:s}', 'Jose'";
                    MessageBox.Show(Acceso.EjecutarComando(sentencia));
                    LlenarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("fallo en la inserción" + ex);
                }
            }
        }
        /// <summary>
        /// Metodo: Elimina una fila de la base de datos
        /// </summary>
        private void Eliminar()
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            string sentencia = $"Exec Eliminar_Producto {Convert.ToInt32(txtIdProducto.Text)}";
            MessageBox.Show(Acceso.EjecutarComando(sentencia));
            LlenarGrid();
        }
        /// <summary>
        /// Metodo: Limpia los datos del Formulario para añadir un nuevo registro a la base de datos
        /// </summary>
        private void Nuevo()
        {
            txtIdProducto.Text = 0.ToString();
            txtNombreProducto.Text = string.Empty;
            txtCodigoProducto.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtImagenProducto.Text = string.Empty;
            txtStock.Text = string.Empty;
        }

        public FrmProductos()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento: Boton Salir Presionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            //verificamos si desea cerrar la ventana
            DialogResult Rta;
            Rta = MessageBox.Show("Desea salir de la edición ?", "MENSAJE DE ADVERTENCIA",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Rta == DialogResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Evento: carga del formulario Productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }
        /// <summary>
        /// Evento: Celda del data grid pulsada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProductos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int posicion = dgProductos.CurrentRow.Index;
            txtIdProducto.Text = dgProductos[0, posicion].Value.ToString();
            txtNombreProducto.Text = dgProductos[1, posicion].Value.ToString();
            txtCodigoProducto.Text = dgProductos[2, posicion].Value.ToString();
            txtPrecioCompra.Text = dgProductos[3, posicion].Value.ToString();
            txtPrecioVenta.Text = dgProductos[4, posicion].Value.ToString();
            cboCategoria.SelectedValue = Convert.ToInt32(dgProductos[5, posicion].Value.ToString());
            txtDetalle.Text = dgProductos[6, posicion].Value.ToString();
            txtImagenProducto.Text = dgProductos[7, posicion].Value.ToString();
            txtStock.Text = dgProductos[8, posicion].Value.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != string.Empty)
            {
                Acceso_Datos Acceso = new Acceso_Datos();
                DataTable dt;
                string Tabla = "TBLPRODUCTO";
                string Condicion = $"where StrNombre like '%{txtBuscar.Text}%'";
                dt = Acceso.CargarTabla(Tabla, Condicion);
                dgProductos.DataSource = dt;
            }
            else
                LlenarGrid();
        }
    }
}
