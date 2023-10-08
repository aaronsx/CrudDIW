using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos para generar conexiones a base de datos.
    /// ASMP-08/10/2023
    /// </summary>
    internal interface InterfazConexionBD
    {
        /// <summary>
        /// Método que genera la conexión a base de datos.
        /// ASMP-08/10/2023
        /// </summary>
        /// <returns>La conexion</returns>
        NpgsqlConnection CrearConexionBD();
        /// <summary>
        /// Método que cierra la conexión a la base de datos.
        /// ASMP-08/10/2023
        /// </summary>
        /// <param name="conexion">conexion</param>
        /// <returns></returns>
        void CerrerConexionBD(NpgsqlConnection conexion);
        
    }
}
