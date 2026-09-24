using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentesMadrid.Models;
using Microsoft.Data.Analysis;

namespace AccidentesMadrid.Mappers;

public interface IMapper <T>
{
    IAsyncEnumerable<Accidentes> CsvToModel(string source);
    Task<DataFrame> CsvToDT(string source);
}