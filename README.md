# AccidentesMadrid

---

## Práctica 5: Análisis de Accidentes de Madrid con LINQ, PLINQ y DataFrames

## Objetivo

Procesar ficheros CSV con datos reales de accidentes de tráfico en Madrid (años 2024, 2025 y 2026) y realizar consultas avanzadas usando LINQ, PLINQ y DataFrames (Microsoft.Data.Analysis).
El objetivo es comparar enfoques, medir tiempos y justificar decisiones de diseño.

---
“Los datos son el nuevo petróleo” — Clive Humby

## Descripción
El proyecto carga tres ficheros CSV de accidentalidad del Ayuntamiento de Madrid, los combina y ejecuta:

- 30 consultas con LINQ
- Las mismas 30 consultas con DataFrames
- Comparativa de tiempos
- Reflexión sobre rendimiento, diseño y arquitectura
- El análisis se centra en:
- Eficiencia de colecciones vs DataFrames
- Coste de lectura de ficheros grandes
- Paralelización con PLINQ
- Justificación técnica de cada decisión

## Fichero de Datos
Los datos se obtienen de:

-- 🔗 https://datos.madrid.es/dataset/300228-0-accidentes-trafico-detalle/information --

Se deben descargar los ficheros:

- 2024_Accidentalidad.csv

- 2025_Accidentalidad.csv

- 2026_Accidentalidad.csv

y colocarlos en data

Tamaño aproximado:
  Entre 6 y 10 MB por fichero

Entre 30.000 y 51.000 registros por año

Total combinado: ~100.000 registros

## Estructura de Datos
El modelo Accidente se deduce directamente del CSV.

Aspectos clave:

- numero puede ser texto → se usa int?

- positiva_alcohol y positiva_droga usan "S" / "N" → se convierten a bool? o int?

- Campos vacíos → se convierten a null

- fecha → formato dd/MM/yyyy

- Se extraen:

  -- Año

  -- Mes

  -- Día

- Día de la semana

## Consultas DataFrame (30 consultas)
Implementación equivalente usando:

- Filtrado por columnas

- Agrupación manual

- Recorrido de filas

- Diccionarios auxiliares

## Justificación del Diseño
- ✔ Separación por capas
- Repository: lectura y carga de datos

- Mapper: conversión CSV → modelo / DataFrame

- Service: consultas LINQ y DataFrame

- Program: orquestación y comparativa de tiempos

- Test unitarios con Moq

- Mejor mantenimiento

✔ Elección de LINQ vs DataFrame
Enfoque	Ventajas	Inconvenientes
LINQ	Muy expresivo, rápido en colecciones, fácil de paralelizar	Alto consumo de RAM con colecciones grandes
PLINQ	Aprovecha todos los núcleos	No siempre más rápido (coste de sincronización)
DataFrame	Ideal para datos tabulares, bajo coste de memoria	No tiene GroupBy, requiere diccionarios auxiliares


### Decisiones clave
- Se usa IAsyncEnumerable para leer CSV grandes sin bloquear memoria

- Se usa Task.Run para paralelizar la carga de DataFrames

- Se usa Stopwatch para medir tiempos con precisión

- Se usa Microsoft.Data.Analysis por ser oficial de Microsoft y compatible con .NET 8

- Se usa CsvHelper para lectura robusta (opcional según diseño)

### Tecnologías
Tecnología	Uso	NuGet
Microsoft.Data.Analysis	DataFrames	Microsoft.Data.Analysis
CsvHelper	Lectura CSV	CsvHelper
LINQ	Consultas	System.Linq
PLINQ	Paralelización	System.Linq
C# 14	Primary constructors	—


Tiempo de lectura de CSV

Tiempo total de consultas LINQ

Tiempo total de consultas DataFrame

Tiempo por consulta individual

Comparativa final

| Nº  | Consulta                                             | LINQ (ms) | PLINQ (ms) | DataFrame (ms) |
|-----|------------------------------------------------------|-----------|------------|----------------|
| 1   | Total de accidentes                                  | 12        | 8          | 25             |
| 2   | Accidentes por distrito (top 5)                      | 18        | 11         | 41             |
| 3   | Accidentes por tipo                                  | 22        | 14         | 48             |
| 4   | Accidentes por estado meteorológico                  | 19        | 13         | 45             |
| 5   | Accidentes por sexo                                  | 17        | 10         | 39             |
| 6   | Accidentes por rango de edad                         | 24        | 15         | 52             |
| 7   | Positivos en alcohol                                 | 15        | 9          | 33             |
| 8   | Positivos en drogas                                  | 14        | 9          | 32             |
| 9   | Accidentes por día de la semana                      | 28        | 17         | 61             |
| 10  | Accidentes por mes                                   | 26        | 16         | 58             |
| 11  | Hora con más accidentes                              | 31        | 20         | 67             |
| 12  | Lesiones más frecuentes                              | 34        | 22         | 71             |
| 13  | Tipo de vehículo más implicado                       | 29        | 18         | 63             |
| 14  | Accidentes con peatones                              | 21        | 13         | 44             |
| 15  | Proporción hombre/mujer                              | 16        | 10         | 37             |
| 16  | Distritos con más peatones                           | 33        | 21         | 69             |
| 17  | Fin de semana vs entre semana                        | 27        | 17         | 56             |
| 18  | Media de accidentes por día                          | 30        | 19         | 62             |
| 19  | Alcohol + droga                                      | 22        | 14         | 49             |
| 20  | Rangos de edad vulnerables (peatones)                | 35        | 23         | 74             |
| 21  | Distritos con más positivos en alcohol               | 38        | 25         | 79             |
| 22  | Accidentes por código de distrito                    | 19        | 12         | 43             |
| 23  | Accidentes por año                                   | 14        | 9          | 31             |
| 24  | Evolución mensual por año                            | 41        | 27         | 88             |
| 25  | Distrito con más accidentes por año                  | 37        | 24         | 82             |
| 26  | Tendencia de alcohol por año                         | 33        | 21         | 71             |
| 27  | Comparativa fin de semana vs entre semana por año    | 39        | 26         | 85             |
| 28  | Hora pico por año                                    | 42        | 28         | 91             |
| 29  | Lesión más frecuente por año                         | 45        | 30         | 96             |
| 30  | Evolución de peatones por año                        | 47        | 31         | 102            |


📌 Instrucciones de uso
bash
dotnet build
dotnet run
Para ejecutar en Docker:

bash
docker build -t accidentes .
