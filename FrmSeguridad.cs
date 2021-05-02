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
    public partial class FrmSeguridad : Form
    {
        /// <summary>
        /// Metodo: cargar el combo box de Empleados
        /// </summary>
        private void Llenar_Empleados()
        {
            DataTable dt = new DataTable();
            Acceso_Datos Acceso = new Acceso_Datos();
            dt = Acceso.CargarTabla("TBLEMPLEADO", "");
            cboEmpleado.DataSource = dt;
            cboEmpleado.ValueMember = "IdEmpleado";
            cboEmpleado.DisplayMember = "StrNombre";
            cboEmpleado.SelectedIndex = 0;
            cboEmpleado_SelectionChangeCommitted(new object(), new EventArgs());

        }

        //---<Metodos de Validacion---
        /// <summary>
        /// Metodo: Valida el ingreso de los valores del formulario
        /// </summary>
        /// <returns></returns>
        private Boolean validar()
        {
            Boolean errorCampos = true;
            if (txtUsuario.Text == string.Empty)
            {
                MensajeError.SetError(txtUsuario, "debe ingresar un valor de Usuario");
                txtUsuario.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtUsuario, ""); }
            if (txtContraseña.Text == "")
            {
                MensajeError.SetError(txtContraseña, "Debe ingresar un valor de contraseña");
                txtContraseña.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtContraseña, ""); }
            return errorCampos;
        }
        //---</Metodos de Validacion---

        //---<Metodos de Actualizacion---
        /// <summary>
        /// Metodo: Permite Actualizar y guardar la informacion en la Base de datos
        /// </summary>
        /// <returns></returns>
        public bool Guardar()
        {
            Boolean actualizado = false;
            if (validar())
            {
                try
                {
                    Acceso_Datos Acceso = new Acceso_Datos();
                    string sentencia = $"Exec Actualizar_Seguridad { Convert.ToInt32(cboEmpleado.SelectedValue)},'{txtUsuario.Text}'," +
                        $"'{txtContraseña.Text}','{DateTime.Now:s}','Jose'";
                    MessageBox.Show(Acceso.EjecutarComando(sentencia));
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
        /// Metodo: permite eliminar un registro de la base de datos
        /// </summary>
        public void Eliminar()
        {
            Acceso_Datos Acceso = new Acceso_Datos();
            string sentencia = $"Exec Eliminar_Seguridad '{ Convert.ToInt32(cboEmpleado.SelectedValue)}'";
            MessageBox.Show(Acceso.EjecutarComando(sentencia));
            txtUsuario.Text = "";
            txtContraseña.Text = "";
        }

        /// <summary>
        /// Metodo: permite consultar los valores y mostrarlos en el formulario
        /// </summary>
        public void Consultar()
        {
            DataTable dt = new DataTable();
            string sentencia = "select StrUsuario,StrClave from TBLSEGURIDAD where IdEmpleado=" + cboEmpleado.SelectedValue.ToString();
            string tabla = "TBLSEGURIDAD";
            string condicion = $"where IdEmpleado = '{cboEmpleado.SelectedValue.ToString()}'";
            Acceso_Datos Acceso = new Acceso_Datos(); // creamos un objeto con la clase Acceso_datos
            dt = Acceso.CargarTabla(tabla, condicion);
            if (dt.Rows.Count > 0)
            {
                txtUsuario.Text = dt.Rows[0]["StrUsuario"].ToString();
                txtContraseña.Text = dt.Rows[0]["StrClave"].ToString();
            }
            else
            {
                MessageBox.Show("El usuario no dispone de datos de ingreso");
                txtUsuario.Text = "";
                txtContraseña.Text = "";
            }
        }

        public FrmSeguridad()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento: Click en el boton Salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult Rta;
            Rta = MessageBox.Show("Desea salir de la edición ?", "MENSAJE DE ADVERTENCIA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Rta == DialogResult.OK)
            {
                this.Close();
            }
        }
        /// <summary>
        /// Evento: Cargar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSeguridad_Load(object sender, EventArgs e)
        {
            Llenar_Empleados();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }


        private void cboEmpleado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Consultar();
        }
    }
}
