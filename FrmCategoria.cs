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
    public partial class FrmCategoria : Form
    {
        /// <summary>
        /// Metodo para llenar el data grid view con la informacion de la base de datos
        /// </summary>
        private void LlenarGrid()
        {
            //DataGridVeiw
            Acceso_Datos Acceso = new Acceso_Datos();
            DataTable dt;
            string Tabla = "TBLCATEGORIA_PROD";
            dt = Acceso.CargarTabla(Tabla, "");
            dgCategoria.DataSource = dt;
        }

        /// <summary>
        /// Funcion para validar los campos del formulario
        /// </summary>
        /// <returns>true: si los datos estan validados</returns>
        private Boolean validar()
        {
            Boolean errorCampos = true;
            if (txtNombreCategoria.Text == string.Empty)
            {
                MensajeError.SetError(txtNombreCategoria, "debeingresar el nombre del producto");
                txtNombreCategoria.Focus();
                errorCampos = false;
            }
            if (txtCodCategoria.Text == "")
            {
                MensajeError.SetError(txtCodCategoria, "debeingresar el documento");
                txtCodCategoria.Focus();
                errorCampos = false;
            }

            return errorCampos;
        }


        public FrmCategoria()
        {
            InitializeComponent();
        }

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
                    string sentencia = $"Exec Actualizar_Categoria_Prod {Convert.ToInt32(txtIdCategoria.Text)},'{txtCodCategoria.Text}' , '{txtNombreCategoria.Text}' , " +
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
            string sentencia = $"Exec Eliminar_Producto {Convert.ToInt32(txtIdCategoria.Text)}";
            MessageBox.Show(Acceso.EjecutarComando(sentencia));
            LlenarGrid();
        }

        /// <summary>
        /// Metodo: Limpia los datos del Formulario para añadir un nuevo registro a la base de datos
        /// </summary>
        private void Nuevo()
        {
            txtIdCategoria.Text = 0.ToString();
            txtNombreCategoria.Text = string.Empty;
            txtCodCategoria.Text = string.Empty;
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
        /// Evento: Celda del data grid pulsada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCategoria_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int posicion = dgCategoria.CurrentRow.Index;
            txtIdCategoria.Text = dgCategoria[0, posicion].Value.ToString();
            txtCodCategoria.Text = dgCategoria[1, posicion].Value.ToString();
            txtNombreCategoria.Text = dgCategoria[2, posicion].Value.ToString();
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
                string Tabla = "TBLCATEGORIA_PROD";
                string Condicion = $"where StrReferencia like '%{txtBuscar.Text}%'";
                dt = Acceso.CargarTabla(Tabla, Condicion);
                dgCategoria.DataSource = dt;
            }
            else
                LlenarGrid();
        }

        /// <summary>
        /// Evento: carga del formulario Productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

    }
}

