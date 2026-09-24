using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;
using Microsoft.Data.Analysis;

namespace AccidentesMadrid.Repository;

public class AccidentesRepositoryDataFrame: IRepository<Accidentes, DataFrame>
{
    private DataFrame _accidentes = new DataFrame();
    private readonly IMapper<Accidentes> _mapper;
    
    public AccidentesRepositoryDataFrame(IMapper<Accidentes> mapper)
    {
        _mapper = mapper;
    }

    public async Task Create()
    {
        var tasks = new List<Task<DataFrame>>
        {
            Task.Run(() => _mapper.CsvToDT("300228-1-accidentes-trafico-detalle-csv.csv")),
            Task.Run(() => _mapper.CsvToDT("300228-2-accidentes-trafico-detalle-csv.csv")),
            Task.Run(() => _mapper.CsvToDT("300228-34-accidentes-trafico-detalle.csv"))
        };
        await Task.Run(async () =>  {
            foreach (var task in tasks)
            {
                await Task.Run(async () =>
                {
                    foreach (var row in task.Result.Rows)
                    {
                        await Task.Run(() => _accidentes.Append(row));
                    }
                });
            }
        });
    }

    public async Task<DataFrame> GetAll()
    {
        return await Task.FromResult(_accidentes);
    }
}