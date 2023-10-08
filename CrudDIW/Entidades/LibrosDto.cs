using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Entidades
{
    /// <summary>
    /// Entidad que contiene el catálogo de libros.
    /// ASMP-08/10/2023
    /// </summary>
    internal class LibrosDto
    {
        //Atributos
        private long idLibro;
        private String titulo;
        private String autor;
        private String isbn;
        private int edicion;
        //Getter and setters
        public long IdLibro { get => idLibro; set => idLibro = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public int Edicion { get => edicion; set => edicion = value; }
        /// <summary>
        /// Metodo para representar las entidades de la clase LibrosDto
        /// </summary>
        /// <returns>String</returns>
        public String toString()
        {
            return "librosDto [titulo=" + titulo + ", autor=" + autor + ", isbn=" + isbn
                    + ", edicion=" + edicion + "]";
        }
    }
}
