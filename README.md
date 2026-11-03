# Gestor de calificaciones

Práctica 1 · C# y .NET Básico · ITLA

## Descripción

Aplicación de consola desarrollada en C# para .NET 10. Permite registrar estudiantes con matrícula única, agregar calificaciones y consultar sus promedios y estados, tanto individualmente por matrícula como en un listado completo.

## Requisitos

- SDK de .NET 10 instalado. El Runtime por sí solo no es suficiente para desarrollar y compilar el proyecto.
- Una terminal en macOS, Windows o Linux.

Para comprobar que el SDK 10 está instalado:

```bash
dotnet --list-sdks
```

Debe aparecer una versión que comience por `10.0.`.

## Cómo ejecutar

1. Si recibiste el proyecto en ZIP, descomprímelo.
2. Abre una terminal dentro de la carpeta que contiene `GestorCalificaciones.csproj`.
3. Compila el proyecto:

   ```bash
   dotnet build
   ```

4. Si la compilación termina sin errores, ejecuta:

   ```bash
   dotnet run
   ```

Los comandos restauran las dependencias del proyecto cuando es necesario. Las carpetas `bin` y `obj` se generan automáticamente; no se incluyen en la entrega.

## Uso del programa

### Menú principal

| Opción | Acción |
| --- | --- |
| 1 | Registrar un estudiante con matrícula y nombre. |
| 2 | Agregar una calificación a un estudiante existente. |
| 3 | Abrir el submenú de consultas. |
| 4 | Salir de la aplicación. |

### Submenú de consultas

| Opción | Acción |
| --- | --- |
| 1 | Consultar un estudiante por su matrícula. |
| 2 | Consultar todos los estudiantes registrados. |
| 3 | Volver al menú principal. |

Las consultas muestran matrícula, nombre, cantidad de calificaciones, promedio y estado. Después de consultar, el submenú vuelve a aparecer.

## Reglas y validaciones

- La matrícula y el nombre son obligatorios; se rechazan entradas vacías o compuestas solo por espacios.
- Se eliminan los espacios al principio y al final de esos datos.
- La matrícula se guarda como texto para conservar los ceros iniciales, como `01`.
- No se permiten matrículas duplicadas. La comparación no distingue mayúsculas y minúsculas.
- Solo se agregan notas a estudiantes existentes.
- Las calificaciones deben ser numéricas y estar entre 0 y 100, incluidos ambos límites.
- Los decimales pueden introducirse con coma o punto, por ejemplo `85,5` o `85.5`, sin separadores de miles.
- Si una nota es inválida, se muestra un mensaje y se solicita nuevamente.
- El promedio se obtiene sumando las notas y dividiendo entre su cantidad.
- Un promedio mayor o igual que 70 corresponde a **Aprobado**; uno menor que 70 corresponde a **Reprobado**.
- Si no hay notas, se muestra **Sin calificaciones**, sin dividir entre cero ni clasificar al estudiante como reprobado.
- Las opciones inválidas muestran un mensaje y permiten continuar utilizando el menú.
- El promedio se presenta con dos decimales. El formato de presentación no modifica el cálculo utilizado para determinar el estado.

## Organización del código

| Archivo | Responsabilidad |
| --- | --- |
| `Estudiante.cs` | Define matrícula y nombre, protege la lista de notas, valida las calificaciones y calcula promedio y estado. |
| `GestorCalificaciones.cs` | Administra la colección de estudiantes, evita matrículas duplicadas y permite registrar, buscar y consultar. |
| `Program.cs` | Contiene el menú principal, el submenú, la lectura y validación de entradas y la presentación de resultados. |
| `GestorCalificaciones.csproj` | Configura la aplicación de consola y su destino `net10.0`. |
| `README.md` | Documenta el funcionamiento y las instrucciones de ejecución. |
| `EVIDENCIAS/` | Contiene las capturas del menú principal y de la consulta de promedios con estudiantes registrados. |

Se aplica encapsulamiento mediante listas privadas y métodos que controlan las modificaciones. La matrícula y el nombre se asignan mediante el constructor. Se utilizan métodos con parámetros, colecciones y estructuras `if`, `switch`, `while` y `foreach`. La lógica del modelo se mantiene separada de la interacción por consola.

**APT significa «A Prueba de Tontos»**. En este proyecto, identifica comentarios claros, directos y relativamente breves que explican el propósito y las decisiones principales del código, para facilitar su comprensión al retomarlo después de un tiempo. En archivos de hasta unas 150–200 líneas, se agrupan al principio o al final; se amplían solo cuando es necesario.

## Pruebas documentadas en las evidencias

Los siguientes datos y resultados corresponden a las capturas de la carpeta `EVIDENCIAS`. Las imágenes recogen distintas ejecuciones del programa; no representan necesariamente un único listado simultáneo.

### Estudiantes y promedios

| Matrícula | Nombre | Notas en la consulta documentada | Promedio mostrado | Estado mostrado | Evidencia |
| --- | --- | --- | --- | --- | --- |
| `01` | KENRY | 83 y 99 | 91.00 | Aprobado | #1 y #2 |
| `02` | LEONARDO | 76 y 89 | 82.50 | Aprobado | #1 y #2 |
| `03` | WALLACE | 69 y 67 | 68.00 | Reprobado | #3 |
| `04` | PEDRO | Sin notas | Sin calificaciones | Sin calificaciones | #4 y #5 |

En la evidencia #4 se agrega posteriormente una tercera nota de `9` a WALLACE. El promedio `68.00` de la evidencia #3 corresponde al momento anterior, cuando tenía únicamente las notas 69 y 67; no es su promedio después de agregar el 9.

### Operaciones y validaciones observadas

| Prueba realizada | Resultado observado | Evidencia |
| --- | --- | --- |
| Compilar con `dotnet build` y ejecutar con `dotnet run`. | Compilación correcta e inicio del menú. | #1 |
| Consultar individualmente las matrículas `01` y `02`. | Se muestran KENRY y LEONARDO con sus respectivos promedios y estados. | #2 |
| Consultar todos los estudiantes. | Se muestran KENRY y LEONARDO en el mismo listado. | #2 |
| Consultar a WALLACE con notas 69 y 67. | Promedio 68.00; Reprobado. | #3 |
| Intentar registrar nuevamente la matrícula `03`. | Se muestra: «Ya existe un estudiante con esa matrícula». | #4 |
| Introducir las notas 130, 233 y 555 para la matrícula `03`. | Se rechazan y se vuelve a solicitar una nota entre 0 y 100. | #4 |
| Introducir 9 después de las notas rechazadas. | Se acepta la nota para WALLACE. | #4 |
| Consultar la matrícula `04`, PEDRO, sin agregar notas. | Cantidad de calificaciones: 0; promedio y estado: Sin calificaciones. | #4 y #5 |
| Intentar agregar una nota a la matrícula inexistente `00`. | Se muestra: «No existe un estudiante con esa matrícula». | #5 |
| Seleccionar las opciones 5 y 6 en el menú principal. | Se muestra el mensaje de opción inválida y el menú continúa. | #5 |
| Volver desde el submenú y seleccionar Salir. | Se regresa al menú principal y se finaliza normalmente con la opción 4. | #2 y #5 |

## Almacenamiento

Los datos se guardan únicamente en memoria durante la ejecución. Al cerrar o reiniciar el programa, los estudiantes y sus notas se pierden. No se utiliza base de datos ni almacenamiento en archivos.

## Evidencias y entrega

La carpeta `EVIDENCIAS` contiene cinco capturas que documentan el menú, las consultas y las validaciones:

| Archivo | Contenido |
| --- | --- |
| `EVIDENCIA #1.png` | Compilación, menú, registro de KENRY y LEONARDO y captura de notas. |
| `EVIDENCIA #2.png` | Nota 89 de LEONARDO, consultas individuales y listado completo de KENRY y LEONARDO, regreso y salida. |
| `EVIDENCIA #3.png` | Registro de WALLACE, notas 69 y 67 y promedio reprobado de 68.00. |
| `EVIDENCIA #4.png` | Matrícula duplicada, rechazo de notas fuera de rango, aceptación de 9 y consulta de PEDRO sin notas. |
| `EVIDENCIA #5.png` | PEDRO sin notas, matrícula inexistente, opciones inválidas y salida normal. |

Para entregar en ZIP, incluir el código fuente, el archivo `.csproj`, este README y la carpeta `EVIDENCIAS`. Excluir las carpetas `bin` y `obj`. Como alternativa, se puede entregar un enlace a un repositorio accesible con esos archivos.
