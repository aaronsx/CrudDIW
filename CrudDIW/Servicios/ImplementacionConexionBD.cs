using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Implementación de la interfaz de conexión a la base de datos
    /// Aaron-28/09/2023
    /// </summary>
    internal class ImplementacionConexionBD : InterfazConexionBD
    {
        /// <summary>
        /// Implementación de la interfaz para conectar a la base de datos.
        /// ASMP-08/10/2023
        /// </summary>
        public NpgsqlConnection CrearConexionBD()
        {
            NpgsqlConnection conexion = null;
            try
            {
                //Se lee la cadena de conexión a Postgresql del archivo de configuración
                string stringConexionPostgresql = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
                Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Cadena conexión: " + stringConexionPostgresql);

               
                string estado = "";

                if (!string.IsNullOrWhiteSpace(stringConexionPostgresql))
                {
                    try
                    {
                        conexion = new NpgsqlConnection(stringConexionPostgresql);
                        conexion.Open();
                        //Se obtiene el estado de conexión para informarlo por consola
                        estado = conexion.State.ToString();
                        if (estado.Equals("Open"))
                        {

                            Console.WriteLine("[INFORMACIÓN-ConexionPostgresqlImplementacion-generarConexionPostgresql] Estado conexión: " + estado);

                        }
                        else
                        {
                            conexion = null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[ERROR-ConexionPostgresqlImplementacion-generarConexionPostgresql] Error al generar la conexión:" + e);
                        conexion = null;
                        return conexion;

                    }
                }

                return conexion;
            }
            catch(Exception e) 
            {
                Console.WriteLine("[ERROR-ImplementacionConexionBD-CrearConexionBD] Error al generar la conexión:" + e);
                conexion = null;
                return conexion;
            }
        }
        /// <summary>
        /// Cerrar conexion.
        /// ASMP-08/10/2023
        /// </summary>
        /// <param name="conexion"></param>
        public void CerrerConexionBD(NpgsqlConnection conexion)
        {
            conexion.Close();
        }

       
    }
}
