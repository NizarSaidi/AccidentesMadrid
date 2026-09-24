using AccidentesMadrid.Models;
using AccidentesMadrid.Repository;
using Microsoft.Data.Analysis;

namespace AccidentesMadrid.Service;

public class ServiceAccidentes : IService<Accidentes>
{
    private IRepository<Accidentes, List<Accidentes>> _repository;
    private IRepository<Accidentes, DataFrame> _repositoryDt;

    public ServiceAccidentes(IRepository<Accidentes, List<Accidentes>> repository,
        IRepository<Accidentes, DataFrame> repositoryDT)
    {
        _repository = repository;
        _repositoryDt = repositoryDT;
    }


    public Dictionary<int, TimeSpan> ImprimirConsultasLinq()
    {
        var resultados = new Dictionary<int, TimeSpan>();
        var consultas = new List<Func<TimeSpan>>
        {
            Consulta1,
            Consulta2,
            Consulta3,
            Consulta4,
            Consulta5,
            Consulta6,
            Consulta7,
            Consulta8,
            Consulta9,
            Consulta10,
            Consulta11,
            Consulta12,
            Consulta13,
            Consulta14,
            Consulta15,
            Consulta16,
            Consulta17,
            Consulta18,
            Consulta19,
            Consulta20,
            Consulta21,
            Consulta22,
            Consulta23,
            Consulta24,
            Consulta25,
            Consulta26,
            Consulta27,
            Consulta28,
            Consulta29,
            Consulta30
        };
        Console.WriteLine("================================");
        foreach (var consulta in consultas)
        {
            Console.WriteLine($"Ejecutando consulta {consultas.IndexOf(consulta) + 1}");
            var tiempo = consulta();
            resultados[consultas.IndexOf(consulta) + 1] = tiempo;
            Console.WriteLine($"Tiempo de ejecución: {tiempo.TotalMilliseconds} ms");
            Console.WriteLine("================================");
        }

        return resultados;
    }
    

    // Total de accidentes
    public TimeSpan Consulta1()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentes = _repository.GetAll().Result.Count;
        Console.Write($"Total de accidentes: {accidentes}\n");
        var endTime = DateTime.UtcNow.Ticks;

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por distrito (top 5)
    public TimeSpan Consulta2()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorDistrito =
            _repository.GetAll().Result.GroupBy(p => p.Distrito).ToDictionary(
                g => g.Key, // Clave del diccionario
                g => g.ToList().Count // Valor del diccionario
            ).OrderBy(x => x.Value).Take(5);

        var endTime = DateTime.UtcNow.Ticks;

        Console.WriteLine("Accidentes por distrito (top 5):");
        foreach (var kvp in accidentesPorDistrito)
        {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por tipo
    public TimeSpan Consulta3()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorTipo =
            _repository.GetAll().Result.GroupBy(p => p.TipoAccidente).ToDictionary(
                g => g.Key, // Clave del diccionario
                g => g.ToList().Count // Valor del diccionario
            );
        var endTime = DateTime.UtcNow.Ticks;

        Console.WriteLine("Accidentes por tipo:");
        foreach (var kvp in accidentesPorTipo)
        {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por estado meteorológico
    public TimeSpan Consulta4()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorEstadoMeteorologico =
            _repository.GetAll().Result.GroupBy(p => p.EstadoMeteorológico).ToDictionary(
                g => g.Key, // Clave del diccionario
                g => g.ToList().Count // Valor del diccionario
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por estado meteorológico:");
        foreach (var kvp in accidentesPorEstadoMeteorologico)
        {
            Console.WriteLine($"Estado meteorológico: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por sexo
    public TimeSpan Consulta5()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorSexo =
            _repository.GetAll().Result.GroupBy(p => p.Sexo).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por sexo:");
        foreach (var kvp in accidentesPorSexo)
        {
            Console.WriteLine($"Sexo: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por rango de edad
    public TimeSpan Consulta6()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorRangoEdad =
            _repository.GetAll().Result.GroupBy(p => p.RangoEdad).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por rango de edad:");
        foreach (var kvp in accidentesPorRangoEdad)
        {
            Console.WriteLine($"Rango de edad: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Positivos en alcohol
    public TimeSpan Consulta7()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var positivosEnAlcohol =
            _repository.GetAll().Result.Where(p => p.PositivaAlcohol != null).ToList().Count;
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes con positivo en alcohol: " + positivosEnAlcohol);
        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Positivos en drogas
    public TimeSpan Consulta8()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var positivosEnDrogas =
            _repository.GetAll().Result.Where(p => p.PositivaDroga == true).ToList().Count;

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes con positivo en drogas: " + positivosEnDrogas);
        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por día de la semana
    public TimeSpan Consulta9()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorDiaSemana =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.DayOfWeek).ToDictionary(
                g => g.Key,
                g => g.ToList()
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por día de la semana:");
        foreach (var kvp in accidentesPorDiaSemana)
        {
            Console.WriteLine($"Día de la semana: {kvp.Key}: {kvp.Value.Count}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }


    // Accidentes por mes
    public TimeSpan Consulta10()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorMes =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Month).ToDictionary(
                g => g.Key,
                g => g.ToList()
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por mes:");
        foreach (var kvp in accidentesPorMes)
        {
            Console.WriteLine($"Mes: {kvp.Key}: {kvp.Value.Count}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Hora con más accidentes
    public TimeSpan Consulta11()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var horaConMasAccidentes =
            _repository.GetAll().Result.GroupBy(p => p.Hora).OrderByDescending(g => g.Count()).Select(g => new
            {
                key = g.Key,
                Count = g.Count()
            }).FirstOrDefault();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Hora con más accidentes: " + horaConMasAccidentes?.key + " con " +
                          horaConMasAccidentes?.Count + " accidentes");
        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Lesiones más frecuentes
    public TimeSpan Consulta12()
    {
        var startTime = DateTime.UtcNow.Ticks;

        var lesionesMasFrecuentes =
            _repository.GetAll().Result.GroupBy(p => p.Lesividad).OrderBy(g => g.Count()).Select(g => new
            {
                key = g.Key,
                Count = g.Count()
            }).Take(20);

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Lesiones más frecuentes:");
        foreach (var lesion in lesionesMasFrecuentes)
        {
            Console.WriteLine($"Lesión: {lesion.key}: {lesion.Count}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Tipo de vehículo más implicado
    public TimeSpan Consulta13()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var tipoVehiculoMasImplicado =
            _repository.GetAll().Result.GroupBy(p => p.TipoVehiculo).OrderBy(g => g.Count()).Select(g => new
            {
                key = g.Key,
                Count = g.Count()
            }).FirstOrDefault();

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine(
            $"Tipo de vehículo más implicado: {tipoVehiculoMasImplicado?.key} con {tipoVehiculoMasImplicado?.Count} accidentes");

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes con peatones
    public TimeSpan Consulta14()
    {
        var startTime = DateTime.UtcNow.Ticks;

        var acidentesConPeatones =
            _repository.GetAll().Result.Where(p => p.TipoPersona == "Pasajero").ToList().Count;
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine($"Accidentes con peatones: {acidentesConPeatones}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Proporción hombre/mujer
    public TimeSpan Consulta15()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorSexo =
            _repository.GetAll().Result.GroupBy(p => p.Sexo).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var accidentesTotales = accidentesPorSexo.Values.Sum();

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Proporción hombre/mujer:");
        foreach (var kvp in accidentesPorSexo)
        {
            Console.WriteLine($"Sexo: {kvp.Key}: {kvp.Value / (double)accidentesTotales * 100}%");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Distritos con más peatones
    public TimeSpan Consulta16()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var distritosConMasPeatones =
            _repository.GetAll().Result.Where(p => p.TipoPersona == "Pasajero").GroupBy(p => p.Distrito)
                .OrderBy(g => g.Count()).Select(g => new
                {
                    key = g.Key,
                    Count = g.Count()
                }).Take(10);

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Distritos con más peatones:");
        foreach (var distrito in distritosConMasPeatones)
        {
            Console.WriteLine($"Distrito: {distrito.key}: {distrito.Count}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Fin de semana vs entre semana
    public TimeSpan Consulta17()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorDiaSemana =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.DayOfWeek).ToDictionary(
                g => g.Key,
                g => g.ToList()
            );
        var accidentesFinDeSemana = accidentesPorDiaSemana
            .Where(x => x.Key == DayOfWeek.Saturday || x.Key == DayOfWeek.Sunday).Sum(x => x.Value.Count);
        var accidentesEntreSemana = accidentesPorDiaSemana
            .Where(x => x.Key != DayOfWeek.Saturday && x.Key != DayOfWeek.Sunday).Sum(x => x.Value.Count);
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por día de la semana:");
        Console.WriteLine($"Finde: {accidentesFinDeSemana} | Entre semana: {accidentesEntreSemana}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Media de accidentes por día
    public TimeSpan Consulta18()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorDia =
            _repository.GetAll().Result.GroupBy(p => p.Fecha).ToDictionary(
                g => g.Key,
                g => g.ToList()
            ).Values.Average(x => x.Count);
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine($"Media de accidentes por día: {accidentesPorDia}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes con alcohol + droga
    public TimeSpan Consulta19()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesConAlcoholYDrogas =
            _repository.GetAll().Result.Where(p => p.PositivaAlcohol != null && p.PositivaDroga == true).ToList().Count;
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine($"Accidentes con alcohol y droga: {accidentesConAlcoholYDrogas}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Rangos de edad más vulnerables (peatones)
    public TimeSpan Consulta20()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var rangosEdadMasVulnerables =
            _repository.GetAll().Result.Where(p => p.TipoPersona == "Pasajero").GroupBy(p => p.RangoEdad)
                .OrderBy(g => g.Count()).Select(g => new
                {
                    key = g.Key,
                    Count = g.Count()
                }).Take(10);
        var endTime = DateTime.UtcNow.Ticks;
        var totalAccidentesPeatones = _repository.GetAll().Result.Count(p => p.TipoPersona == "Pasajero");
        Console.WriteLine("Rangos de edad más vulnerables (peatones):");
        foreach (var rango in rangosEdadMasVulnerables)
        {
            Console.WriteLine($" {rango.key}: con {rango.Count / totalAccidentesPeatones * 100}% de accidentes");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Distritos con más positivos en alcohol
    public TimeSpan Consulta21()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var distritosConMasPositivosEnAlcohol =
            _repository.GetAll().Result.Where(p => p.PositivaAlcohol != null).GroupBy(p => p.Distrito)
                .OrderBy(g => g.Count()).Select(g => new
                {
                    key = g.Key,
                    Count = g.Count()
                }).Take(10);
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Distritos con más positivos en alcohol:");
        foreach (var distrito in distritosConMasPositivosEnAlcohol)
        {
            Console.WriteLine($" {distrito.key}: {distrito.Count}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por código de distrito
    public TimeSpan Consulta22()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorCodigoDistrito =
            _repository.GetAll().Result.GroupBy(p => p.CodDistrito).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por código de distrito:");
        foreach (var kvp in accidentesPorCodigoDistrito)
        {
            Console.WriteLine($" {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por año
    public TimeSpan Consulta23()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentesPorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por año:");
        foreach (var kvp in accidentesPorAno)
        {
            Console.WriteLine($" {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Evolución mensual por año
    public TimeSpan Consulta24()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var evolucionMensualPorAno =
            _repository.GetAll().Result.GroupBy(p => new { p.Fecha.Year, p.Fecha.Month }).ToDictionary(
                g => g.Key,
                g => g.ToList().Count
            );
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Evolución mensual por año:");
        foreach (var kvp in evolucionMensualPorAno)
        {
            Console.WriteLine($" {kvp.Key.Year}-{kvp.Key.Month}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Distrito con más accidentes por año
    public TimeSpan Consulta25()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var distritoConMasAccidentesPorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).Select(g => new
            {
                año = g.Key,
                DistritoConMasAccidentes = g.GroupBy(p => p.Distrito).OrderByDescending(h => h.Count()).Select(h => new
                {
                    h.Key,
                    accidentes = h.Count()
                }).FirstOrDefault()
            }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Distrito con más accidentes por año:");
        foreach (var distrito in distritoConMasAccidentesPorAno)
        {
            Console.WriteLine(
                $" Año: {distrito.año} | Distrito: {distrito.DistritoConMasAccidentes.Key} | Accidentes: {distrito.DistritoConMasAccidentes.accidentes}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Tendencia de alcohol por año
    public TimeSpan Consulta26()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var tendenciaAlcoholPorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).Select(g => new
            {
                año = g.Key,
                Tendencia = g.Average(p => p.PositivaAlcohol != null ? p.PositivaAlcohol : 0)
            }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Tendencia de alcohol por año:");
        foreach (var tendencia in tendenciaAlcoholPorAno)
        {
            Console.WriteLine($" Año: {tendencia.año} | Tendencia: {tendencia.Tendencia}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Comparativa fin de semana vs entre semana por año
    public TimeSpan Consulta27()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var comparativaFinDeSemanaVsEntreSemanaPorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).Select(g => new
            {
                año = g.Key,
                FinDeSemana = g.Count(p =>
                    p.Fecha.DayOfWeek == DayOfWeek.Saturday || p.Fecha.DayOfWeek == DayOfWeek.Sunday),
                EntreSemana = g.Count(p =>
                    p.Fecha.DayOfWeek != DayOfWeek.Saturday && p.Fecha.DayOfWeek != DayOfWeek.Sunday)
            }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Comparativa fin de semana vs entre semana por año:");
        foreach (var comparativa in comparativaFinDeSemanaVsEntreSemanaPorAno)
        {
            Console.WriteLine(
                $" Año: {comparativa.año} | Fin de semana: {comparativa.FinDeSemana} | Entre semana: {comparativa.EntreSemana}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Hora pico por año
    public TimeSpan Consulta28()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var horaPicoPorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).Select(g => new
            {
                año = g.Key,
                HoraPico = g.GroupBy(p => p.Hora.Hour).OrderBy(h => h.Count()).Select(h => new
                {
                    h.Key,
                    accidentes = h.Count()
                }).FirstOrDefault()
            }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Hora pico por año:");
        foreach (var horaPico in horaPicoPorAno)
        {
            Console.WriteLine(
                $" Año: {horaPico.año} | Hora pico: {horaPico.HoraPico.Key}:00 | Accidentes: {horaPico.HoraPico.accidentes}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Lesión más frecuente por año
    public TimeSpan Consulta29()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var lesionMasFrecuentePorAno =
            _repository.GetAll().Result.GroupBy(p => p.Fecha.Year).Select(g => new
            {
                año = g.Key,
                LesionMasFrecuente = g.GroupBy(p => p.Lesividad).OrderBy(h => h.Count()).Select(h => new
                {
                    h.Key,
                    accidentes = h.Count()
                }).FirstOrDefault()
            }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Lesión más frecuente por año:");
        foreach (var lesion in lesionMasFrecuentePorAno)
        {
            Console.WriteLine(
                $" Año: {lesion.año} | Lesión más frecuente: {lesion.LesionMasFrecuente.Key} | Accidentes: {lesion.LesionMasFrecuente.accidentes}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Evolución de peatones por año
    public TimeSpan Consulta30()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var evolucionPeatonesPorAno =
            _repository.GetAll().Result.Where(p => p.TipoPersona == "Pasajero").GroupBy(p => p.Fecha.Year).Select(g =>
                new
                {
                    año = g.Key,
                    Peatones = g.Count()
                }).ToList();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Evolución de peatones por año:");
        foreach (var peaton in evolucionPeatonesPorAno)
        {
            Console.WriteLine($" Año: {peaton.año} | Peatones: {peaton.Peatones}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    public Dictionary<int, TimeSpan> ImprimirConsultasDataFrame()
    {
        var resultados = new Dictionary<int, TimeSpan>();
        var consultas = new List<Func<TimeSpan>>
        {
            Consulta1DataFrame,
            Consulta2DataFrame,
            Consulta3DataFrame,
            Consulta4DataFrame,
            Consulta5DataFrame,
            Consulta6DataFrame,
            Consulta7DataFrame,
            Consulta8DataFrame,
            Consulta9DataFrame,
            Consulta10DataFrame,
            Consulta11DataFrame,
            Consulta12DataFrame,
            Consulta13DataFrame,
            Consulta14DataFrame,
            Consulta15DataFrame,
            Consulta16DataFrame,
            Consulta17DataFrame,
            Consulta18DataFrame,
            Consulta19DataFrame,
            Consulta20DataFrame,
            Consulta21DataFrame,
            Consulta22DataFrame,
            Consulta23DataFrame,
            Consulta24DataFrame,
            Consulta25DataFrame,
            Consulta26DataFrame,
            Consulta27DataFrame,
            Consulta28DataFrame,
            Consulta29DataFrame,
            Consulta30DataFrame
        };
        Console.WriteLine("================================");
        foreach (var consulta in consultas)
        {
            Console.WriteLine($"Ejecutando consulta en Data Frame {consultas.IndexOf(consulta) + 1}");
            var tiempo = consulta();
            resultados[consultas.IndexOf(consulta) + 1] = tiempo;
            Console.WriteLine($"Tiempo de ejecución: {tiempo.TotalMilliseconds} ms");
            Console.WriteLine("================================");
        }

        return resultados;
    }

    //========================================
    //Consultas con DataFrame
    //========================================  
    // Total de accidentes
    public TimeSpan Consulta1DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var accidentes = _repositoryDt.GetAll().Result.Rows.Count;
        Console.Write($"Total de accidentes: {accidentes}\n");
        var endTime = DateTime.UtcNow.Ticks;

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Accidentes por distrito (top 5)
    public TimeSpan Consulta2DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var ciudad = row["Distrito"].ToString();

            if (!grupos.ContainsKey(ciudad))
                grupos[ciudad] = 0;

            grupos[ciudad]++;
        }

        var cinco = grupos.Take(5);
        var porCiudad = _repositoryDt.GetAll().Result;
        Console.Write($"Accidentes por distrito");
        foreach (var uno in cinco)
        {
            Console.Write($"distrito: {uno.Key}, num: {uno.Value}");
        }
        var endTime = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por tipo
    public TimeSpan Consulta3DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["TipoAccidente"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;

        Console.WriteLine("Accidentes por tipo:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por estado meteorológico
    public TimeSpan Consulta4DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["EstadoMeteorológico"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por estado meteorológico:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Estado meteorológico: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por sexo
    public TimeSpan Consulta5DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["Sexo"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por sexo:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Sexo: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por rango de edad
    public TimeSpan Consulta6DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["RangoEdad"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por rango de edad:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Rango de edad: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Positivos en alcohol
    public TimeSpan Consulta7DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var positivosEnAlcohol =
            _repositoryDt.GetAll().Result.Columns["PositivaAlcohol"].ElementwiseIsNotNull().Count();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes con positivo en alcohol: " + positivosEnAlcohol);
        return TimeSpan.FromTicks(endTime - startTime);
    }

// Positivos en drogas
    public TimeSpan Consulta8DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var positivosEnDrogas =
            _repositoryDt.GetAll().Result.Columns["PositivaAlcohol"].ElementwiseEquals("S").Count();

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes con positivo en drogas: " + positivosEnDrogas);
        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por día de la semana
    public TimeSpan Consulta9DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<DayOfWeek, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = DateTime.Parse(row["Fecha"].ToString()).DayOfWeek;

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por día de la semana:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Día de la semana: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por mes
    public TimeSpan Consulta10DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<int, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = DateTime.Parse(row["Fecha"].ToString()).Month;

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por mes:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Mes: {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Hora con más accidentes
    public TimeSpan Consulta11DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<int, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = TimeOnly.Parse(row["Hora"].ToString()).Hour;

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }

        var horaConMasAccidentes = grupos.OrderBy(v => v.Value).First();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Hora con más accidentes: " + horaConMasAccidentes.Key + " con " +
                          horaConMasAccidentes.Value + " accidentes");
        return TimeSpan.FromTicks(endTime - startTime);
    }

// Lesiones más frecuentes
    public TimeSpan Consulta12DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;

        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["Lesividad"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var lesionesMasFrecuentes = grupos.OrderBy(v => v.Value).Take(10);

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Lesiones más frecuentes:");
        foreach (var lesion in lesionesMasFrecuentes)
        {
            Console.WriteLine($"Lesión: {lesion.Key}: {lesion.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Tipo de vehículo más implicado
    public TimeSpan Consulta13DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["TipoVehiculo"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }

        var tipoVehiculoMasImplicado = grupos.OrderBy(v => v.Value).First();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine(
            $"Tipo de vehículo más implicado: {tipoVehiculoMasImplicado.Key} con {tipoVehiculoMasImplicado.Value} accidentes");

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes con peatones
    public TimeSpan Consulta14DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;

        var acidentesConPeatones =
            _repositoryDt.GetAll().Result.Columns["TipoPersona"].ElementwiseEquals("Peaton").Count();
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine($"Accidentes con peatones: {acidentesConPeatones}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Proporción hombre/mujer
    public TimeSpan Consulta15DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<string, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = row["Sexo"].ToString();

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }
        var accidentesTotales = grupos.Values.Sum();

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Proporción hombre/mujer:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($"Sexo: {kvp.Key}: {kvp.Value / (double)accidentesTotales * 100}%");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Distritos con más peatones
    public TimeSpan Consulta16DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<string, int>();
        foreach (var row in df.Rows)
        {
            var tipoPersona = row["TipoPersona"]?.ToString();

            // Filtrar peatones
            if (tipoPersona == "Pasajero")
            {
                var distrito = row["Distrito"]?.ToString();

                if (!grupos.ContainsKey(distrito))
                    grupos[distrito] = 0;

                grupos[distrito]++;
            }
        }

        // Ordenar por número de peatones (ascendente)
        var top10 = grupos.OrderBy(x => x.Value).Take(10);

        var endTime = DateTime.UtcNow.Ticks;

        Console.WriteLine("Distritos con más peatones:");
        foreach (var kv in top10)
        {
            Console.WriteLine($"Distrito: {kv.Key}: {kv.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

    // Fin de semana vs entre semana
    public TimeSpan Consulta17DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<DayOfWeek, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = DateTime.Parse(row["Fecha"].ToString()).DayOfWeek;

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }

        var accidentesFinDeSemana = grupos.Where(p => p.Key == DayOfWeek.Friday || p.Key == DayOfWeek.Saturday).Sum(p => p.Value);
        var accidentesEntreSemana = grupos.Where(p => p.Key != DayOfWeek.Friday && p.Key != DayOfWeek.Saturday).Sum(p => p.Value);
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por día de la semana:");
        Console.WriteLine($"Finde: {accidentesFinDeSemana} | Entre semana: {accidentesEntreSemana}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Media de accidentes por día
    public TimeSpan Consulta18DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var grupos = new Dictionary<int, int>();
        foreach (var row in _repositoryDt.GetAll().Result.Rows)
        {
            var Tipo = DateTime.Parse(row["Fecha"].ToString()).DayOfYear;

            if (!grupos.ContainsKey(Tipo))
                grupos[Tipo] = 0;

            grupos[Tipo]++;
        }

        var accidentesPorDia = grupos.Average(g => g.Value);
        var endTime = DateTime.UtcNow.Ticks;
        
        Console.WriteLine($"Media de accidentes por día: {accidentesPorDia}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes con alcohol + droga
    public TimeSpan Consulta19DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var cout = 0;
        foreach (var row in df.Rows)
        {
            var positivoDroga = row["PositivaDroga"]?.ToString();

            // Filtrar peatones
            if (positivoDroga == "S")
            {
                var PositivaAlcohol = row["PositivaAlcohol"]?.ToString();

                if (PositivaAlcohol != "" || PositivaAlcohol !=null )
                    cout++;
            }
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine($"Accidentes con alcohol y droga: {cout}");

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Rangos de edad más vulnerables (peatones)
    public TimeSpan Consulta20DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<string, int>();
        foreach (var row in df.Rows)
        {
            var distrito = row["RangoEdad"]?.ToString();
            if (!grupos.ContainsKey(distrito)) 
                grupos[distrito] = 0;

            grupos[distrito]++;
        }
        
        var top10 = grupos.OrderBy(x => x.Value).Take(10);

        var endTime = DateTime.UtcNow.Ticks;
        var totalAccidentesPeatones = _repositoryDt.GetAll().Result.Columns.Count;
        Console.WriteLine("Rangos de edad más vulnerables (peatones):");
        foreach (var rango in top10)
        {
            Console.WriteLine($" {rango.Key}: con {rango.Value / totalAccidentesPeatones * 100}% de accidentes");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Distritos con más positivos en alcohol
    public TimeSpan Consulta21DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<string, int>();
        foreach (var row in df.Rows)
        {
            var positivoAlcohol = row["PositivaAlcohol"]?.ToString();

            // Filtrar peatones
            if (positivoAlcohol != "" || positivoAlcohol !=null )
            {
                var distrito = row["Distrito"]?.ToString();
                if (!grupos.ContainsKey(distrito)) 
                    grupos[distrito] = 0;

                grupos[distrito]++;
            }
        }
        
        var top10 = grupos.OrderBy(x => x.Value).Take(10);

        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Distritos con más positivos en alcohol:");
        foreach (var distrito in top10)
        {
            Console.WriteLine($" {distrito.Key}: {distrito.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por código de distrito
    public TimeSpan Consulta22DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<string, int>();
        foreach (var row in df.Rows)
        {
            var distrito = row["CodDistrito"]?.ToString();
            if (!grupos.ContainsKey(distrito)) 
                grupos[distrito] = 0;

            grupos[distrito]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por código de distrito:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($" {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Accidentes por año
    public TimeSpan Consulta23DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<int, int>();
        foreach (var row in df.Rows)
        {
            var year = DateTime.Parse(row["Fecha"]?.ToString()).Year;
            if (!grupos.ContainsKey(year)) 
                grupos[year] = 0;

            grupos[year]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Accidentes por año:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($" {kvp.Key}: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Evolución mensual por año
    public TimeSpan Consulta24DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        var grupos = new Dictionary<string,int>();
        foreach (var row in df.Rows)
        {
            var year =$"{DateTime.Parse(row["Fecha"]?.ToString()).Year.ToString()},{DateTime.Parse(row["Fecha"]?.ToString()).Month.ToString()}";
            if (!grupos.ContainsKey(year)) 
                grupos[year] = 0;

            grupos[year]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Evolución mensual por año:");
        foreach (var kvp in grupos)
        {
            Console.WriteLine($" {kvp.Key }: {kvp.Value}");
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Distrito con más accidentes por año
    public TimeSpan Consulta25DataFrame()
    {
        var startTime = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;
        
        var accidentesPorAno = new Dictionary<int, Dictionary<string, int>>();

        foreach (var row in df.Rows)
        {
            int año = DateTime.Parse(row["Fecha"]?.ToString()).Year;
            string distrito = row["Distrito"]?.ToString();

            if (!accidentesPorAno.ContainsKey(año))
                accidentesPorAno[año] = new Dictionary<string, int>();

            if (!accidentesPorAno[año].ContainsKey(distrito))
                accidentesPorAno[año][distrito] = 0;

            accidentesPorAno[año][distrito]++;
        }
        var endTime = DateTime.UtcNow.Ticks;
        Console.WriteLine("Distrito con más accidentes por año:");
        foreach (var kv in accidentesPorAno)
        {
            int año = kv.Key;
            var distritos = kv.Value;

            // Ordenar por número de accidentes (descendente)
            var topDistrito = distritos.OrderByDescending(x => x.Value).First();

            Console.WriteLine(
                $" Año: {año} | Distrito: {topDistrito.Key} | Accidentes: {topDistrito.Value}"
            );
        }

        return TimeSpan.FromTicks(endTime - startTime);
    }

// Tendencia de alcohol por año
    public TimeSpan Consulta26DataFrame()
    {
        var start = DateTime.UtcNow.Ticks;

        var df = _repositoryDt.GetAll().Result; // Tu DataFrame cargado previamente

        // Diccionario: año → lista de valores de alcohol
        var alcoholPorAno = new Dictionary<int, List<double>>();

        foreach (var row in df.Rows)
        {
            int año = DateTime.Parse(row["Fecha"]?.ToString()).Year;

            // Si PositivaAlcohol es null → se considera 0
            double alcohol = 0;

            var valor = row["PositivaAlcohol"];
            if (valor != null && valor != DBNull.Value)
                alcohol = Convert.ToDouble(valor);

            if (!alcoholPorAno.ContainsKey(año))
                alcoholPorAno[año] = new List<double>();

            alcoholPorAno[año].Add(alcohol);
        }

        Console.WriteLine("Tendencia de alcohol por año:");

        foreach (var kv in alcoholPorAno)
        {
            int año = kv.Key;
            double tendencia = kv.Value.Average();

            Console.WriteLine($" Año: {año} | Tendencia: {tendencia}");
        }

        var end = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(end - start);
    }


// Comparativa fin de semana vs entre semana por año
    public TimeSpan Consulta27DataFrame()
    {
        var start = DateTime.UtcNow.Ticks;

        var df = _repositoryDt.GetAll().Result; // Tu DataFrame cargado previamente

        // Diccionario: año → (finDeSemana, entreSemana)
        var comparativa = new Dictionary<int, (int FinDeSemana, int EntreSemana)>();

        foreach (var row in df.Rows)
        {
            var fecha = DateTime.Parse(row["Fecha"]?.ToString());
            int año = fecha.Year;
            var dia = fecha.DayOfWeek;

            if (!comparativa.ContainsKey(año))
                comparativa[año] = (0, 0);

            bool esFinde = dia == DayOfWeek.Saturday || dia == DayOfWeek.Sunday;

            if (esFinde)
                comparativa[año] = (comparativa[año].FinDeSemana + 1, comparativa[año].EntreSemana);
            else
                comparativa[año] = (comparativa[año].FinDeSemana, comparativa[año].EntreSemana + 1);
        }

        Console.WriteLine("Comparativa fin de semana vs entre semana por año:");

        foreach (var kv in comparativa)
        {
            Console.WriteLine(
                $" Año: {kv.Key} | Fin de semana: {kv.Value.FinDeSemana} | Entre semana: {kv.Value.EntreSemana}"
            );
        }

        var end = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(end - start);
    }


    // Hora pico por año
    public TimeSpan Consulta28DataFrame()
    {
        var start = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;

        var accidentesPorAno = new Dictionary<int, Dictionary<int, int>>();

        foreach (var row in df.Rows)
        {
            int año = DateTime.Parse(row["Fecha"]?.ToString()).Year;
            int hora = TimeOnly.Parse(row["Hora"]?.ToString()).Hour;

            if (!accidentesPorAno.ContainsKey(año))
                accidentesPorAno[año] = new Dictionary<int, int>();

            if (!accidentesPorAno[año].ContainsKey(hora))
                accidentesPorAno[año][hora] = 0;

            accidentesPorAno[año][hora]++;
        }

        Console.WriteLine("Hora pico por año:");

        foreach (var kv in accidentesPorAno)
        {
            int año = kv.Key;
            var horas = kv.Value;

            var horaPico = horas.OrderByDescending(x => x.Value).First();

            Console.WriteLine(
                $" Año: {año} | Hora pico: {horaPico.Key}:00 | Accidentes: {horaPico.Value}"
            );
        }

        var end = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(end - start);
    }


    // Lesión más frecuente por año
    public TimeSpan Consulta29DataFrame()
    {
        var start = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;

        var lesionesPorAno = new Dictionary<int, Dictionary<string, int>>();

        foreach (var row in df.Rows)
        {
            int año =  DateTime.Parse(row["Fecha"]?.ToString()).Year;
            string lesion = row["Lesividad"]?.ToString();

            if (!lesionesPorAno.ContainsKey(año))
                lesionesPorAno[año] = new Dictionary<string, int>();

            if (!lesionesPorAno[año].ContainsKey(lesion))
                lesionesPorAno[año][lesion] = 0;

            lesionesPorAno[año][lesion]++;
        }

        Console.WriteLine("Lesión más frecuente por año:");

        foreach (var kv in lesionesPorAno)
        {
            int año = kv.Key;
            var lesiones = kv.Value;

            var lesionMasFrecuente = lesiones.OrderByDescending(x => x.Value).First();

            Console.WriteLine(
                $" Año: {año} | Lesión más frecuente: {lesionMasFrecuente.Key} | Accidentes: {lesionMasFrecuente.Value}"
            );
        }

        var end = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(end - start);
    }


    // Evolución de peatones por año
    public TimeSpan Consulta30DataFrame()
    {
        var start = DateTime.UtcNow.Ticks;
        var df = _repositoryDt.GetAll().Result;

        // año → peatones
        var peatonesPorAno = new Dictionary<int, int>();

        foreach (var row in df.Rows)
        {
            string tipoPersona = row["TipoPersona"]?.ToString();

            if (tipoPersona == "Pasajero")
            {
                int año = DateTime.Parse(row["Fecha"]?.ToString()).Year;

                if (!peatonesPorAno.ContainsKey(año))
                    peatonesPorAno[año] = 0;

                peatonesPorAno[año]++;
            }
        }

        Console.WriteLine("Evolución de peatones por año:");

        foreach (var kv in peatonesPorAno)
        {
            Console.WriteLine($" Año: {kv.Key} | Peatones: {kv.Value}");
        }

        var end = DateTime.UtcNow.Ticks;
        return TimeSpan.FromTicks(end - start);
    }

}