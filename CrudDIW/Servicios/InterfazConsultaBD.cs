using CrudDIW.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos para generar conexiones a base de datos
    /// ASMP-04/10/2023
    /// </summary>
    internal interface InterfazConsultaBD
    {
        /// <summary>
        ///  Método que realiza consulta sobre la bbdd.
        ///  ASMP-04/10/2023
        /// </summary>
        /// <param name="saberPedir">formaDeGuardado indica la forma en que se guardara</param>
        List<LibrosDto> SeleccionarEnBBDD(bool saberPedir);
        /// <summary>
        ///  Método que realiza consulta para insertar.
        ///  ASMP-08/10/2023
        /// </summary>
        void InsertEnBBDD();

        /// <summary>
        ///  Método que realiza consulta para eliminar.
        ///  ASMP-08/10/2023
        /// </summary>
        void DeleteEnBBDD();
        /// <summary>
        ///  Método que realiza consulta para actualizar.
        ///  ASMP-08/10/2023
        /// </summary>
        void UpdateEnBBDD();
    }
}
