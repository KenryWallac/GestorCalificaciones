using System;
using System.Collections.Generic;

// El namespace agrupa las clases de nuestro proyecto.
// Los otros archivos de C# utilizarán este mismo espacio de nombres.
namespace GestorCalificaciones;

public class Estudiante
{
    private readonly List<decimal> calificaciones = new();

    public string Matricula { get; }
    public string Nombre { get; }

    public int CantidadCalificaciones
    {
        get { return calificaciones.Count; }
    }

    public Estudiante(string matricula, string nombre)
    {
        if (string.IsNullOrWhiteSpace(matricula))
        {
            throw new ArgumentException("La matrícula es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.");
        }

        Matricula = matricula.Trim();
        Nombre = nombre.Trim();
    }

    public bool AgregarCalificacion(decimal calificacion)
    {
        if (calificacion < 0 || calificacion > 100)
        {
            return false;
        }

        calificaciones.Add(calificacion);
        return true;
    }

    public decimal? CalcularPromedio()
    {
        if (calificaciones.Count == 0)
        {
            return null;
        }

        decimal suma = 0;

        foreach (decimal calificacion in calificaciones)
        {
            suma += calificacion;
        }

        return suma / calificaciones.Count;
    }

    public string ObtenerEstado()
    {
        decimal? promedio = CalcularPromedio();

        if (!promedio.HasValue)
        {
            return "Sin calificaciones";
        }

        if (promedio.Value >= 70)
        {
            return "Aprobado";
        }

        return "Reprobado";
    }
}

/*
COMENTARIOS APT:
- Esta clase representa un estudiante con matrícula, nombre y calificaciones.
- Matricula es texto para conservar los ceros iniciales, como "001".
- Las propiedades con get permiten leer los datos; el constructor los asigna.
- El constructor exige matrícula y nombre, y elimina espacios en los extremos.
- La lista de notas es privada para impedir modificaciones sin validación.
- readonly impide reemplazar la lista, pero permite agregarle notas.
- AgregarCalificacion acepta notas de 0 a 100 y devuelve true si las agregó.
- CalcularPromedio usa foreach para sumar las notas y dividir entre su cantidad.
- decimal? permite devolver null cuando no hay notas y evitar dividir entre cero.
- ObtenerEstado devuelve "Sin calificaciones", "Aprobado" desde 70 o "Reprobado".
*/