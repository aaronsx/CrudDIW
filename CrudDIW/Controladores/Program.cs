using CrudDIW.Entidades;
using CrudDIW.Servicios;
using CrudDIW.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW
{
    /// <summary>
    ///  Clase por la que se entra a la aplicación que contiene el main. 
    ///  En ella solo estan definidos los pasos y el procedimiento de ejecución que sigue la aplicación.
    ///   ASMP-08/10/2023
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Método main de la aplicación, puerta de entrada.
        /// ASMP-08/10/2023
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            InterfazMenu menu = new ImplementacionMenu();
            InterfazConsultaBD consul = new ImplementacionConsultaBD();
            List<LibrosDto> bd = new List<LibrosDto>();
            Boolean cerrarMenu = false;
            int opcion = 0;
            while (!cerrarMenu)
            {
                try
                {
                    //Mostramos el menú
                    opcion=menu.Menu();
                    if (opcion >= 0 && opcion <= 4)
                    {
                        Console.WriteLine("[INFO] - Has seleccionado la opcion " + opcion);
                    }
                    //Llama a insertar a la base de datos
                    //select a la base de datos
                    //Actualiza la base de datos
                    //Elimina en la base de datos
                    switch (opcion)
                    {
                        case 1:
                            consul.InsertEnBBDD();
                            break;

                        case 2:
                             bd = consul.SeleccionarEnBBDD(false);
                            Console.ReadKey();
                            break;

                        case 3:
                            consul.DeleteEnBBDD();
                            break;

                        case 4:
                            consul.UpdateEnBBDD();
                            break;
                        case 0:
                            Console.WriteLine("¡Se cierra la aplicacion!");
                            cerrarMenu = true;
                            break;
                        
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Se ha surgido otro error");
                    Console.ResetColor();

                }

            }
            
        }
    }
}
