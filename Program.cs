using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GestorCalificaciones;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        GestorCalificaciones gestor = new GestorCalificaciones();
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine();
            Console.WriteLine("=== GESTOR DE CALIFICACIONES ===");
            Console.WriteLine("1. Registrar estudiante");
            Console.WriteLine("2. Agregar calificación");
            Console.WriteLine("3. Consultar estudiantes y promedios");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            if (opcion == null)
            {
                break;
            }

            switch (opcion.Trim())
            {
                case "1":
                    RegistrarEstudiante(gestor);
                    break;

                case "2":
                    AgregarCalificacion(gestor);
                    break;

                case "3":
                    ConsultarPromedios(gestor);
                    break;

                case "4":
                    continuar = false;
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    Console.WriteLine("Opción inválida. Seleccione del 1 al 4.");
                    break;
            }
        }
    }

    static string LeerTextoObligatorio(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (entrada == null)
            {
                Environment.Exit(0);
            }

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("Este campo es obligatorio. Inténtelo nuevamente.");
        }
    }

    static void RegistrarEstudiante(GestorCalificaciones gestor)
    {
        Console.WriteLine();
        Console.WriteLine("--- Registrar estudiante ---");

        string matricula = LeerTextoObligatorio("Matrícula: ");

        if (gestor.BuscarEstudiante(matricula) != null)
        {
            Console.WriteLine("Ya existe un estudiante con esa matrícula.");
            return;
        }

        string nombre = LeerTextoObligatorio("Nombre: ");
        bool registrado = gestor.RegistrarEstudiante(matricula, nombre);

        if (registrado)
        {
            Console.WriteLine("Estudiante registrado correctamente.");
        }
        else
        {
            Console.WriteLine("No se pudo registrar el estudiante.");
        }
    }

    static void AgregarCalificacion(GestorCalificaciones gestor)
    {
        Console.WriteLine();
        Console.WriteLine("--- Agregar calificación ---");

        string matricula = LeerTextoObligatorio("Matrícula del estudiante: ");
        Estudiante? estudiante = gestor.BuscarEstudiante(matricula);

        if (estudiante == null)
        {
            Console.WriteLine("No existe un estudiante con esa matrícula.");
            return;
        }

        while (true)
        {
            string entrada = LeerTextoObligatorio(
                "Calificación de 0 a 100: ");

            string entradaNormalizada = entrada.Replace(',', '.');

            bool esNumero = decimal.TryParse(
                entradaNormalizada,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out decimal calificacion);

            if (!esNumero)
            {
                Console.WriteLine("Escriba un número válido.");
                continue;
            }

            if (!estudiante.AgregarCalificacion(calificacion))
            {
                Console.WriteLine("La calificación debe estar entre 0 y 100.");
                continue;
            }

            Console.WriteLine($"Calificación agregada a {estudiante.Nombre}.");
            break;
        }
    }

    static void ConsultarPromedios(GestorCalificaciones gestor)
    {
        while (true)
        {   
            Console.WriteLine();
            Console.WriteLine("=== CONSULTAR ESTUDIANTES Y PROMEDIOS ===");
            Console.WriteLine("1. Consultar un estudiante por matrícula");
            Console.WriteLine("2. Consultar todos los estudiantes");
            Console.WriteLine("3. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            if (opcion == null)
            {
                Environment.Exit(0);
            }

            switch (opcion.Trim())
            {
                case "1":
                {
                    string matricula = LeerTextoObligatorio(
                        "Matrícula del estudiante: ");

                    Estudiante? estudiante = gestor.BuscarEstudiante(matricula);

                    if (estudiante == null)
                    {
                        Console.WriteLine(
                            "No existe un estudiante con esa matrícula.");
                    }
                    else
                    {
                        MostrarDetalleEstudiante(estudiante);
                    }

                    break;
                }

                case "2":
                    MostrarEstudiantes(gestor);
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine(
                        "Opción inválida. Seleccione 1, 2 o 3.");
                    break;
            }
        }
    }

    static void MostrarEstudiantes(GestorCalificaciones gestor)
    {
        Console.WriteLine();
        Console.WriteLine("--- Todos los estudiantes ---");

        IReadOnlyList<Estudiante> estudiantes = gestor.ConsultarEstudiantes();

        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }

        foreach (Estudiante estudiante in estudiantes)
        {
            MostrarDetalleEstudiante(estudiante);
        }
    }

    static void MostrarDetalleEstudiante(Estudiante estudiante)
    {
        decimal? promedio = estudiante.CalcularPromedio();

        Console.WriteLine();
        Console.WriteLine($"Matrícula: {estudiante.Matricula}");
        Console.WriteLine($"Nombre: {estudiante.Nombre}");
        Console.WriteLine(
            $"Cantidad de calificaciones: {estudiante.CantidadCalificaciones}");

        if (promedio.HasValue)
        {
            Console.WriteLine($"Promedio: {promedio.Value:F2}");
        }
        else
        {
            Console.WriteLine("Promedio: Sin calificaciones");
        }

        Console.WriteLine($"Estado: {estudiante.ObtenerEstado()}");
    }
}
/*
COMENTARIOS APT:
- Program controla el menú, lee el teclado y presenta los resultados.
- Main es el punto de inicio; crea un único gestor para conservar los registros.
- while repite el menú y switch ejecuta la opción seleccionada.
- LeerTextoObligatorio rechaza entradas vacías o formadas solo por espacios.
- Si se cierra la entrada de la consola, el programa termina normalmente.
- RegistrarEstudiante solicita los datos y comprueba matrículas duplicadas.
- AgregarCalificacion busca al estudiante y solicita una nota válida.
- TryParse convierte texto a número sin fallar por un formato incorrecto.
- Replace permite introducir decimales con coma o punto, sin separadores de miles.
- Las notas fuera de 0 a 100 se rechazan y se vuelven a solicitar.
- ConsultarPromedios permite consultar por matrícula, consultar todos o volver.
- MostrarEstudiantes recorre el listado completo.
- MostrarDetalleEstudiante presenta los datos, promedio y estado de un estudiante.
- HasValue comprueba si existe un promedio; Value obtiene ese número.
- F2 muestra el promedio con dos decimales, sin cambiar el cálculo.
- Los datos permanecen en memoria y se pierden al cerrar la aplicación.
*/