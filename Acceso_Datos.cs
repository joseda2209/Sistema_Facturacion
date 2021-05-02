using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_Facturacion
{
    /// <summary>
    /// En esta clase estan ubicados los metodos para accesder y manipular la base de datos
    /// </summary>
    class Acceso_Datos
    {
        SqlConnection conexion; // se define la variable para la conexión de tipo SqlConnection
        SqlCommand cmd; // se defie la variable para realizar comandos en la BD
        SqlDataReader LectorDatos = null;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        /// <summary>
        /// Abre una conexion con la base de datos DBFacturas
        /// </summary>
        public void AbrirBd()
        {
            try // permite captura de un error en caso que se presente
            {
                // creamos un objeto de tipo conexión a la base de datos y se pasa como parámetro la cadena de conexión
                conexion = new SqlConnection("Data Source=localhost\\HER_PROG_III; Database = DBFacturas; Integrated Security = true;");
                conexion.Open(); // invocamos método para abrir la base de datos
            }
            catch (Exception ex)// si hay error presenta el siguiente mensaje
            {
                MessageBox.Show("falló conexión " + ex);
            }
        }
        /// <summary>
        /// Cierra la conexion con la base de datos
        /// </summary>
        public void CerrarrBd()
        {
            try // permite captura de un error en caso que se presente
            {
                conexion.Close(); // invocamos método para cerrar la base de datos
            }
            catch (Exception ex)// si hay error presenta el siguiente mensaje
            {
                MessageBox.Show("falló cerrar conexión " + ex);
            }
        }

        public string ValidarUsuario(string StrUsuario, string StrClave)
        {
            try
            {
                string StrEmpleado = string.Empty;
                string sentencia = $"select e.strNombre, e.IdRolEmpleado from TBLSEGURIDAD s JOIN TBLEMPLEADO e ON s.IdEmpleado = e.IdEmpleado where StrUsuario = '{StrUsuario}' and StrClave = '{StrClave}'";
                AbrirBd();
                cmd = new SqlCommand();
                // utilizamos las propiedades de SqlCommand
                cmd.Connection = conexion;
                cmd.CommandText = sentencia;
                cmd.CommandType = CommandType.Text; // otros tipos son :CommandType.StoredProcedure CommandType.TableDirect
                cmd.CommandTimeout = 10;
                LectorDatos = cmd.ExecuteReader();
                while (LectorDatos.Read())
                {
                    StrEmpleado = Convert.ToString(LectorDatos.GetValue(0));
                }
                if (LectorDatos != null) // cerramos el lector de datos
                {
                    LectorDatos.Close();
                }
                return StrEmpleado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLA LECTURA: " + ex);
                return "";
            }
        }

        /// <summary>
        /// Método que permite consultar una tabla y recuperar un conjunto de datos permite filtrar la información 
        /// requerida
        /// </summary>
        /// <param name="tabla"> tabla de la base de datos</param>
        /// <param name="strCondicion">condicion para la sentencia</param> 
        /// <returns>DataTable</returns>
        public DataTable CargarTabla(string tabla, string strCondicion)
        {
            try
            {
                AbrirBd();
                string Sql = $"Select * from {tabla} " + strCondicion;
                da = new SqlDataAdapter(Sql, conexion);
                ds = new DataSet();
                da.Fill(ds, tabla);
                dt = ds.Tables[tabla];
                CerrarrBd();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR EN LA CONSULTA: " + ex.ToString());
                return null;
            }
        }

        // ------- recibe una sentencia de para realizar acciones de actualizar, retirar y nuevo
        // solo retorna un valor numerico que indica cuantas filas fueron afectadas

        /// <summary>
        /// Metodo para ejecutar comandos sql
        /// </summary>
        /// <param name="sentencia">sentencia sql a ejecutar</param>
        /// <returns>strig: si filas fueron afectadas</returns>
        public string EjecutarComando(string sentencia)
        {
            string salida;
            try
            {
                int retornado;
                AbrirBd();
                cmd = new SqlCommand(sentencia, conexion);
                retornado = cmd.ExecuteNonQuery(); // UTILIZADO PARA UPDATE, INSERT y DELETE
                CerrarrBd();
                if (retornado > 0)
                {
                    salida = "LOS DATOS SE ACTUALIZARON SATISFACTORIAMENTE!";
                }
                else
                {
                    salida = "Los datos no fueron Actualizados";
                }
            }
            catch (Exception ex)
            {
                salida = "falló inserción: " + ex;
            }
            return salida;
        }
    }
}
