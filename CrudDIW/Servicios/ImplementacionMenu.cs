using System;

namespace CrudDIW
{
    /// <summary>
    /// Implementación de la interfaz Menu.
    ///  ASMP-08/10/2023
    /// </summary>
    internal class ImplementacionMenu : InterfazMenu
    {
        public int Menu()
        {
            int opcion = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔════════════════════════════════════╗");
                Console.WriteLine("\t\t║                CRUD                ║");
                Console.WriteLine("\t\t╠════════════════════════════════════╣");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   1) Crear Datos                   ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   2) Leer Datos                    ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   3) Borrar Datos                  ║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║   4) Actualizar Datos              ║");
                Console.WriteLine("\t\t║____________________________________║");
                Console.WriteLine("\t\t║                                    ║");
                Console.WriteLine("\t\t║           0) Salir                 ║");
                Console.WriteLine("\t\t╚════════════════════════════════════╝");
                Console.Write("\t\t\tIntroduzca una opción: ");
                opcion = Console.ReadKey().KeyChar - '0';

                if (opcion < 0 || opcion > 4)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t\t[Error] El valor no está dentro del rango.");
                    Console.ResetColor();
                }
            } while (opcion < 0 || opcion > 4);

            return opcion;
        }
    }
}