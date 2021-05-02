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
    public partial class FrmEmpleado : Form
    {
        /// <summary>
        /// Metodo para llenar el datagrid con la información de la base de datos
        /// </summary>
        private void LlenarGrid()
        {
            DataTable dt = new DataTable();
            Acceso_Datos Acceso = new Acceso_Datos();
            dt = Acceso.CargarTabla("TBLEMPLEADO", "");
            dgEmpleados.DataSource = dt;

            dt = Acceso.CargarTabla("TBLROLES", "");
            cboCargo.DataSource = dt;
            cboCargo.DisplayMember = "StrDescripcion";
            cboCargo.ValueMember = "IdRolEmpleado";
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
            if (!esNumerico(txtDocumento.Text))
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
        private bool esNumerico(string num)
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

        //---<Funciones de Actualizaion>---

        /// <summary>
        /// Metodo para ingresar informacion a la base de datos
        /// </summary>
        /// <returns>true: si se guardo la informacion</returns>
        public bool Guardar()
        {
            Boolean actualizado = false;
            if (validar())
            {
                try
                {
                    Acceso_Datos Acceso = new Acceso_Datos();
                    string sentencia = $"Exec actualizar_Empleado {Convert.ToInt32(txtIdEmpleado.Text)},'{txtNombre.Text}'," +
                        $"'{ txtDocumento.Text}','{txtDireccion.Text}','{txtTelefono.Text}'," +
                        $"'{txtEmail.Text}',{ cboCargo.SelectedValue},'{dtmFechaInicio.Value:d}'," +
                        $"'{dtmFechaRetiro.Value:d}','{txtDatos.Text}' ,'{DateTime.Now:s}','Jose'";
                    MessageBox.Show(Acceso.EjecutarComando(sentencia));
                    LlenarGrid();
                    actualizado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("falló inserción: " + ex);
                    actualizado = false;
                }
            }
            return actualizado;
        }

        /// <summary>
        /// metodo para elminar un campo de la base de datos
        /// </summary>
        public void Eliminar()
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            string sentencia = $"Exec Eliminar_empleado '{ Convert.ToInt32(txtIdEmpleado.Text)}'";
            MessageBox.Show(Acceso.EjecutarComando(sentencia));
            LlenarGrid(); // actualizamos de nuevo el grid para que refleje el cambio
        }

        /// <summary>
        /// Metodo para limpiar los campos del formulario
        /// </summary>
        public void Nuevo()
        {
            txtIdEmpleado.Text = "0";
            txtNombre.Text = "";
            txtDocumento.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            cboCargo.SelectedIndex = 0;
            dtmFechaInicio.Value = DateTime.Now;
            dtmFechaRetiro.Value = Convert.ToDateTime("01/01/1900");
            txtDatos.Text = "";
        }
        //---</Funciones de actualización>---


        /// <summary>
        /// Metodo Principal, se inicializan los componentes del formulario
        /// </summary>
        public FrmEmpleado()
        {
            InitializeComponent();
        }


        //---<Eventos>---

        /// <summary>
        /// Evento al presionar boton Salir
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
        /// Evento de carga del formulario Empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        /// <summary>
        /// Evento que permite cargar los elementos de una fila del datagrid (database) en el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEmpleados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int posActual = 0;
            posActual = dgEmpleados.CurrentRow.Index;
            txtIdEmpleado.Text = dgEmpleados[0, posActual].Value.ToString();
            txtNombre.Text = dgEmpleados[1, posActual].Value.ToString();
            txtDocumento.Text = dgEmpleados[2, posActual].Value.ToString();
            txtDireccion.Text = dgEmpleados[3, posActual].Value.ToString();
            txtTelefono.Text = dgEmpleados[4, posActual].Value.ToString();
            txtEmail.Text = dgEmpleados[5, posActual].Value.ToString();
            cboCargo.SelectedValue = Convert.ToInt16(dgEmpleados[6, posActual].Value.ToString());
            dtmFechaInicio.Value = Convert.ToDateTime(dgEmpleados[7, posActual].Value.ToString());
            if (dgEmpleados[8, posActual].Value.ToString() != "")
            {
                dtmFechaRetiro.Value = Convert.ToDateTime(dgEmpleados[8, posActual].Value.ToString());
            }
            else
            {
                dtmFechaRetiro.Value = Convert.ToDateTime("01/01/1900");
            }
            txtDatos.Text = dgEmpleados[9, posActual].Value.ToString();
        }

        /// <summary>
        /// Evento click en el boton Nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        /// <summary>
        /// Evento Click en el boton Actualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        /// <summary>
        /// Evento click en el boton Eliminar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        /// <summary>
        /// Evento: Click en el boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            if (txtBuscar.Text != "")
            {
                string Tabla = "TBLEMPLEADO";
                string Condicion = $"where strNombre like '%{txtBuscar.Text}%'";
                dgEmpleados.DataSource = Acceso.CargarTabla(Tabla, Condicion);
                txtBuscar.Text = "";
            }
            else
            {
                LlenarGrid();
            }
        }

        //---</Eventos>---

    }
}
