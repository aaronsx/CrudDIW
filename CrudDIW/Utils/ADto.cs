using CrudDIW.Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Utils
{
    /// <summary>
    /// Métodos que pasan a objeto de tipo DTO
    /// ASMP-08/10/2023
    /// </summary>
    internal class ADto
    {
        /// <summary>
        /// Captura el resultado de la query
        /// ASMP-08/10/2023
        /// </summary>
        /// <param name="resultadoConsulta"></param>
        /// <returns></returns>
        public List<LibrosDto> readerALibroDto(NpgsqlDataReader resultadoConsulta)
        {
            List<LibrosDto> listaLibros = new List<LibrosDto>();
            while (resultadoConsulta.Read())
            {
                LibrosDto libros = new LibrosDto();
                libros.IdLibro = long.Parse(resultadoConsulta[0].ToString());
                libros.Titulo = resultadoConsulta[1].ToString();
                libros.Autor = resultadoConsulta[2].ToString();
                libros.Isbn = resultadoConsulta[3].ToString();
                libros.Edicion = (int)Int64.Parse(resultadoConsulta[4].ToString());
                listaLibros.Add(libros);
            }
            return listaLibros;
        }
    }
}
