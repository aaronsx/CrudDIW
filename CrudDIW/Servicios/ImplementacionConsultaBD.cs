using CrudDIW.Entidades;
using CrudDIW.Utils;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Implementacion de Consultas con la base de datos.
    /// ASMP-08/10/2023
    /// </summary>
    internal class ImplementacionConsultaBD : InterfazConsultaBD
    {
        public List<LibrosDto> SeleccionarEnBBDD(bool saberPedir)
        {
            List<LibrosDto> list = new List<LibrosDto>();
            InterfazConexionBD conexionInterfaz = new ImplementacionConexionBD();
            NpgsqlConnection conexion = conexionInterfaz.CrearConexionBD();
            ADto aDto = new ADto();
            
            try
            {
                if (!saberPedir)
                    saberPedir = MetodoSiono("¿Quieres seleccionar todos los libros?");
                //Si te viene true te devuelve con un select entero 
                if (saberPedir)
                {
                    //Se define y ejecuta la consulta Select
                    NpgsqlCommand consulta = new NpgsqlCommand("SELECT *  FROM gbp_almacen.gbp_alm_cat_libros", conexion);
                    NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

                    //Paso de DataReader a lista de alumnoDTO
                    list = aDto.readerALibroDto(resultadoConsulta);
                    int contador = list.Count;
                    Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBBDD-SeleccionarEnBBDD] Número de libros: " + contador);

                    Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBBDD-SeleccionarEnBBDD] Cierre conexión y conjunto de datos");
                    conexion.Close();
                    resultadoConsulta.Close();
                }
                else
                {
                    NpgsqlCommand consulta = new NpgsqlCommand("SELECT *  FROM gbp_almacen.gbp_alm_cat_libros WHERE titulo=@titu;", conexion);
                    NpgsqlDataReader resultadoConsulta = null;
                    do
                    {
                        //Select filtrando
                        Console.WriteLine("Introduce el titulo del libro");
                        String titulo = Console.ReadLine();

                        consulta.Parameters.AddWithValue("@titu", titulo);
                        resultadoConsulta = consulta.ExecuteReader();
                        list = aDto.readerALibroDto(resultadoConsulta);;
                        int cont = list.Count;

                        Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBBDD-SeleccionarEnBBDD] Número libros: " + cont);
                        Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBBDD-SeleccionarEnBBDD] Cierre conexión, declaración y resultado");
                       
                    } while (MetodoSiono("Quieres hacer select otro libro?"));
                   
                    
                   
                    conexion.Close();
                    resultadoConsulta.Close();
                }
                

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR-ImplementacionConsultaBBDD-SeleccionarEnBBDD] Error al ejecutar consulta: " + e);
                Console.ResetColor();
                conexion.Close();

            }
            foreach (LibrosDto mostrarLibro in list)
            {
                Console.WriteLine(mostrarLibro.toString());
            }
            return list;

        }

        public void InsertEnBBDD()
        {
            InterfazConexionBD conexionInterfaz = new ImplementacionConexionBD();
            NpgsqlConnection conexion = conexionInterfaz.CrearConexionBD();
            try
            {
                //Insertar 
                String query = "INSERT INTO gbp_almacen.gbp_alm_cat_libros (titulo, autor, isbn,edicion) VALUES (@titu,@aut,@isbn,@edito);";
               
                List<LibrosDto> listabd = new List<LibrosDto>();
                do
                {

                    LibrosDto libro = new LibrosDto();
                    libro.IdLibro = calculoId(listabd);
                    Console.WriteLine("Introduce titulo titulo");
                    libro.Titulo = Console.ReadLine();
                    Console.WriteLine("Introduce el autor");
                    libro.Autor = Console.ReadLine();
                    Console.WriteLine("Introduce el isbn");
                    libro.Isbn = Console.ReadLine();
                    Console.WriteLine("Introduce la edicion");
                    libro.Edicion = Convert.ToInt32(Console.ReadLine());

                    listabd.Add(libro);
                } while (MetodoSiono("Quieres insertar otro usuario?"));
                
            
                foreach (LibrosDto libros in listabd)
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, conexion);

                    if (!EnContrarSiExiste(libros.Isbn, true))
                    {

                        //Se cambia el @ por el nombre del titulo en la query y se ejecuta
                        command.Parameters.AddWithValue("@titu", libros.Titulo);
                        command.Parameters.AddWithValue("@aut", libros.Autor);
                        command.Parameters.AddWithValue("@isbn", libros.Isbn);
                        command.Parameters.AddWithValue("@edito", libros.Edicion);
                        command.ExecuteNonQuery();
                        Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBBDD-InsertEnBBDD] Cierre conexión, declaración y resultado");



                    }
                    else
                    {
                        //Si encuentra registro igual no te dejara insertar y te pedira si quieres buscar
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("[ERROR-ImplementacionConsultaBBDD-InsertEnBBDD] Ya existe una tabla con el mismo isbn te recomiendo que insertes uno a uno o elimine el repetido");
                        Console.ResetColor();
                        if (MetodoSiono("Quieres buscar el titulo que se repite?"))
                        {
                            List<LibrosDto> listabdActual = new List<LibrosDto>();
                            listabdActual=SeleccionarEnBBDD(true);
                            //Busca sobre la base de datos y te dice cual se repite
                            foreach (LibrosDto librosUs in listabd)
                                foreach (LibrosDto librosActual in listabdActual)
                                    if (librosUs.Isbn == librosActual.Isbn)
                                        Console.WriteLine("[AVISO-ImplementacionConsultaBBDD-InsertEnBBDD] los datos que se petien son:" + librosUs.toString());

                        }

                    }
                }

                

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("[ERROR-ImplementacionConsultaBBDD-InsertEnBBDD] Error al insertar en la base de datos: " + e);
                Console.ResetColor();
                conexionInterfaz.CerrerConexionBD(conexion);

            }
            conexionInterfaz.CerrerConexionBD(conexion);

        }
        public void DeleteEnBBDD()
        {
            InterfazConexionBD conexionInterfaz = new ImplementacionConexionBD();
            NpgsqlConnection conexion = conexionInterfaz.CrearConexionBD();
            try
            {
                String query = "DELETE FROM gbp_almacen.gbp_alm_cat_libros WHERE titulo=@titu;";
                NpgsqlCommand command = new NpgsqlCommand(query, conexion);
                List<LibrosDto> lista = new List<LibrosDto>();
                //Delete
                do
                {
                    lista=SeleccionarEnBBDD(true);
                    Console.WriteLine("Que libro quieres eliminar?(titulo)");
                    String titulo = Console.ReadLine();
                    if (EnContrarSiExiste(titulo, false))
                    {
                        if (MetodoSiono("Seguro que quieres eliminar?"))
                        {
                            command.Parameters.AddWithValue("titu", titulo);
                            command.ExecuteNonQuery();
                            Console.WriteLine("[INFORMACIÓN-ImplementacionConsultaBD-DeleteEnBBDD] Cierre conexión, declaración y resultado");

                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("[ERROR-ImplementacionConsultaBD-DeleteEnBBDD] No se encontro ningun libro con ese nombre");
                        Console.ResetColor();
                    }
                } while (MetodoSiono("Quieres eliminar a mas libros?"));
              
            }
            catch (Exception e)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.Error.WriteLine("[ERROR-ImplementacionConsultasBBDD-DeleteEnBBDD] Error al eliminar en la base de datos: " + e);
                Console.ResetColor();
                conexionInterfaz.CerrerConexionBD(conexion);

            }
            conexionInterfaz.CerrerConexionBD(conexion);
        }
        public void UpdateEnBBDD()
        {
            InterfazConexionBD conexionInterfaz = new ImplementacionConexionBD();
            NpgsqlConnection conexion = conexionInterfaz.CrearConexionBD();
            try
            {
                String query = "UPDATE gbp_almacen.gbp_alm_cat_libros SET titulo = @titu, autor = @aut,isbn = @isbn, edicion = @edito WHERE isbn=@isbnAnti;";
                NpgsqlCommand command = new NpgsqlCommand(query, conexion);
                List<LibrosDto> listabdActual = new List<LibrosDto>();
                listabdActual=SeleccionarEnBBDD(true);
                
                do
                {
                    Console.WriteLine("Que libro quieres modificar?(titulo)");
                    String titulo = Console.ReadLine();
                    String antiguoTitulo = "";
                    String antiguoAutor = "";
                    String antiguoIsbn = "";
                    int antiguaEdicion = 0;
                    if (EnContrarSiExiste(titulo, false))
                    {

                        foreach (LibrosDto librosActual in listabdActual)
                            if (titulo == librosActual.Titulo)
                            {
                                antiguoTitulo = librosActual.Titulo;
                                antiguoAutor = librosActual.Autor;
                                antiguoIsbn = librosActual.Isbn;
                                antiguaEdicion = librosActual.Edicion;
                            }

                        Console.WriteLine("Pulsa enter si no quieres que se guarde");
                        Console.WriteLine("Nuevo titulo?");
                        titulo = Console.ReadLine();
                        Console.WriteLine("Nuevo autor?");
                        String autor = Console.ReadLine();
                        Console.WriteLine("Nuevo isbn?");
                        String isbn = Console.ReadLine();
                        Console.WriteLine("Nueva edicion?(Pulsa 0 Si no quieres que se guarde)");
                        int edicion = Convert.ToInt32(Console.ReadLine());



                        if (titulo == String.Empty)
                            titulo = antiguoTitulo;
                        if (autor == String.Empty)
                            autor = antiguoAutor;
                        if (isbn == String.Empty)
                            isbn = antiguoIsbn;
                        if (edicion == 0)
                            edicion = antiguaEdicion;


                        command.Parameters.AddWithValue("titu", titulo);
                        command.Parameters.AddWithValue("aut", autor);
                        command.Parameters.AddWithValue("isbn", isbn);
                        command.Parameters.AddWithValue("edito", edicion);
                        command.Parameters.AddWithValue("isbnAnti", antiguoIsbn);
                        command.ExecuteNonQuery();


                        Console.WriteLine("[INFORMACIÓN-ImplementacionConsultasBBDD-UpdateEnBBDD] Cierre conexión, declaración y resultado");



                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("[ERROR-ImplementacionConsultaBD-UpdateEnBBDD]  No se encontro ningun libro con ese nombre");
                        Console.ResetColor();

                    }
                } while (MetodoSiono("Quieres actualizar a mas libros?"));

                conexionInterfaz.CerrerConexionBD(conexion);
            }
            catch (Exception e)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.Error.WriteLine("[ERROR-ImplementacionConsultasBBDD-UpdateEnBBDD] Error al actualizar en la base de datos: " + e);
                Console.ResetColor();
                conexionInterfaz.CerrerConexionBD(conexion);

            }
            conexionInterfaz.CerrerConexionBD(conexion);
        }

        /// <summary>
        /// Método para saber si existe el usuario que pides.
        /// ASMP-28/09/2023
        /// </summary>
        /// <param name="esIsbn">numero para diferenciar</param>
        /// <param name="personaPorSeparado">personaPorSeparado para saber el titulo</param>
        /// <returns>devuelve un boolean.</returns>
        private bool EnContrarSiExiste(String personaPorSeparado, bool esIsbn)
        {

            List<LibrosDto> listabdActual = new List<LibrosDto>();


            listabdActual=SeleccionarEnBBDD(true);
            if (esIsbn)
            {
                //Si introduce un Isbn y se recorre toda la lista para saber si existe o no
                foreach (LibrosDto librosActual in listabdActual)
                    if (personaPorSeparado == librosActual.Isbn)
                        return true;
            }
            else
            {
                //Si introduce una titulo y se recorre toda la lista para saber si existe o no
                foreach (LibrosDto librosActual in listabdActual)
                    if (personaPorSeparado == librosActual.Titulo)
                        return true;
            }
            return false;
        }
        /// <summary>
        /// Método Preguntar si quieres hacer una interaccion o no.
        /// ASMP-28/09/2023
        /// </summary>
        /// <param name="txt"> string para hacer la pregunta. </param>
        /// <returns>devuelve un boolean.</returns>
        private bool MetodoSiono(String txt)
        {
            //Preguntar si o si no

            String sioNo;
            bool cerrarmenu = true;
            do
            {
                Console.WriteLine(txt);
                sioNo = Console.ReadLine().ToLower();

                switch (sioNo)
                {
                    case "si":
                        return true;
                    case "no":
                        return false;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("***ERROR*** solo se puede si o no.");
                        Console.ResetColor();
                        cerrarmenu = false;
                        break;
                }

            } while (!cerrarmenu);
            return true;

        }
        /// <summary>
        /// Método para calcular una id segun el tamaño de la lista.
        /// ASMP-08/10/2023
        /// </summary>
        /// <param name="bdAntigua">Una lista.</param>
        /// <returns>devuelve la id.</returns>
        private long calculoId(List<LibrosDto> bdAntigua)
        {
            //Calculamos ids
            long auxiliar = 0;
            foreach (LibrosDto libro in bdAntigua)
            {
                long j = libro.IdLibro;
                if (auxiliar < j)
                {
                    auxiliar = j;
                }
            }
            return auxiliar + 1;
        }
        
    }


}
