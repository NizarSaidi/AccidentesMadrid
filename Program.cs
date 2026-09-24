using System;
using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;
using AccidentesMadrid.Repository;
using AccidentesMadrid.Service;
using Microsoft.Data.Analysis;

//iniciamos dependencias

IMapper<Accidentes> mapper = new AccidentesMapper();
IRepository<Accidentes,List<Accidentes>> repositoryLq = new AccidentesRepository(mapper);
IRepository<Accidentes,DataFrame> repositoryDf = new AccidentesRepositoryDataFrame(mapper);
IService<Accidentes> servicio = new ServiceAccidentes(repositoryLq, repositoryDf);

//Creamos los datos

//Iniciamos datos de tiempo de consultas

var tiemposLinq = servicio.ImprimirConsultasLinq();
var tiemposDataFrame = servicio.ImprimirConsultasDataFrame();

//Comparativa de tiempos
Console.WriteLine("=============================================================");
Console.WriteLine("Tiempos comparados");
Console.WriteLine("=============================================================");
Console.WriteLine("|==DataFrame==|==LinQ==|");
foreach (var tiempodf in tiemposDataFrame)
{
    Console.Write("|  ");
    Console.Write(tiempodf.Value);
    Console.Write("  |");
    Console.Write("|  ");
    Console.Write(tiemposLinq[tiempodf.Key]);
    Console.Write("  |");
}
Console.WriteLine("|==DataFrame==|==LinQ==|");

