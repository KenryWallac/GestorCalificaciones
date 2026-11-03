using System;
using System.Collections.Generic;

namespace GestorCalificaciones;

public class GestorCalificaciones
{
    private readonly List<Estudiante> estudiantes = new();

    public Estudiante? BuscarEstudiante(string matricula)
    {
        string matriculaBuscada = matricula.Trim();

        foreach (Estudiante estudiante in estudiantes)
        {
            if (string.Equals(
                estudiante.Matricula,
                matriculaBuscada,
                StringComparison.OrdinalIgnoreCase))
            {
                return estudiante;
            }
        }

        return null;
    }

    public bool RegistrarEstudiante(string matricula, string nombre)
    {
        if (string.IsNullOrWhiteSpace(matricula) ||
            string.IsNullOrWhiteSpace(nombre))
        {
            return false;
        }

        if (BuscarEstudiante(matricula) != null)
        {
            return false;
        }

        Estudiante nuevoEstudiante = new Estudiante(matricula, nombre);

        estudiantes.Add(nuevoEstudiante);

        return true;
    }

    public IReadOnlyList<Estudiante> ConsultarEstudiantes()
    {
        return estudiantes.AsReadOnly();
    }
}

/*
COMENTARIOS APT:
- Esta clase administra los estudiantes registrados en memoria.
- La lista privada impide agregar o eliminar estudiantes directamente desde fuera.
- BuscarEstudiante recorre la lista y busca por matrícula.
- La comparación ignora mayúsculas y minúsculas: "A001" y "a001" son equivalentes.
- Estudiante? permite devolver un estudiante o null cuando no se encuentra.
- RegistrarEstudiante rechaza datos vacíos y matrículas duplicadas.
- Si el registro es válido, crea el estudiante, lo agrega y devuelve true.
- ConsultarEstudiantes devuelve una vista de solo lectura de la lista.
- Esta clase administra datos; el menú y los mensajes estarán en Program.cs.
*/
