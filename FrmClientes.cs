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
    public partial class FrmClientes : Form
    {
        /// <summary>
        /// Metodo: permite llenar el data grid con la informacion de la Base de datos
        /// </summary>
        private void LlenarGrid()
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            DataTable dt;
            dt = Acceso.CargarTabla("TBLCLIENTES", "");
            dgClientes.DataSource = dt;
        }

        //---<Funciones de Validacion>---

        /// <summary>
        /// Funcion para validar los campos del formulario
        /// </summary>
        /// <returns>true: si los datos estan validados</returns>
        private Boolean validar()
        {
            Boolean errorCampos = true;
            if (txtNombre.Text == string.Empty)
            {
                MensajeError.SetError(txtNombre, "debeingresar el nombre del empleado");
                txtNombre.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtNombre, ""); }
            if (txtDocumento.Text == "")
            {
                MensajeError.SetError(txtDocumento, "debeingresar el documento");
                txtDocumento.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtDocumento, ""); }
            if (!EsNumerico(txtDocumento.Text))
            {
                MensajeError.SetError(txtDocumento, "El Documento debe ser numerico");
                txtDocumento.Focus();
                return false;
            }
            MensajeError.SetError(txtDocumento, "");
            return errorCampos;
        }

        /// <summary>
        /// Metodo para validar si un valor es numerico
        /// </summary>
        /// <param name="num"> string a validar</param>
        /// <returns>true: si num es un numero</returns>
        private bool EsNumerico(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //---</Funciones de Validacion>---

        //---<Metodos de Actualizacion>---
        /// <summary>
        /// Metodo: Actualiza la base de datos
        /// </summary>
        private void Actualizar()
        {
            if (validar())
            {
                try
                {
                    Acceso_Datos Acceso = new Acceso_Datos();
                    string sentencia = $"Exec Actualizar_Clientes {Convert.ToInt32(txtIdCliente.Text)} , '{txtNombre.Text}'," +
                        $"{Convert.ToInt64(txtDocumento.Text)} , '{txtDireccion.Text}' , '{txtTelefono.Text}' , '{txtEmail.Text}' ," +
                        $"'{DateTime.Now:s}' , 'Jose'";
                    MessageBox.Show(Acceso.EjecutarComando(sentencia));
                    LlenarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("fallo de inserción: " + ex);
                }
            }
        }
        /// <summary>
        /// Metodo: Elimina un campo de la base de datos
        /// </summary>
        private void Eliminar()
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            string sentencia = $"Exec Eliminar_Clientes {Convert.ToInt32(txtIdCliente.Text)}";
            MessageBox.Show(Acceso.EjecutarComando(sentencia));
            LlenarGrid();
        }
        /// <summary>
        /// Metodo: limpia los campos del formulario
        /// </summary>
        private void Nuevo()
        {
            txtIdCliente.Text = 0.ToString();
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
        //---</Metodos de Actualizacion>---

        public FrmClientes()
        {
            InitializeComponent();
        }

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

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void dgClientes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int posicion = 0;
            posicion = dgClientes.CurrentRow.Index;
            txtIdCliente.Text = dgClientes[0, posicion].Value.ToString();
            txtNombre.Text = dgClientes[1, posicion].Value.ToString();
            txtDocumento.Text = dgClientes[2, posicion].Value.ToString();
            txtDireccion.Text = dgClientes[3, posicion].Value.ToString();
            txtTelefono.Text = dgClientes[4, posicion].Value.ToString();
            txtEmail.Text = dgClientes[5, posicion].Value.ToString();
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
            Acceso_Datos Acceso = new Acceso_Datos();
            if (txtBuscar.Text != "")
            {
                string Tabla = "TBLCLIENTES";
                string Condicion = $"where StrNombre like '%{txtBuscar.Text}%'";
                dgClientes.DataSource = Acceso.CargarTabla(Tabla, Condicion);
                txtBuscar.Text = "";
            }
            else
            {
                LlenarGrid();
            }
        }
    }
}
