using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;

namespace AccidentesMadrid.Repository;

public class AccidentesRepository: IRepository<Accidentes , List<Accidentes>>
{
    private List<Accidentes> _accidentes = new List<Accidentes>();
    private readonly IMapper<Accidentes> _mapper;
    
    public AccidentesRepository(IMapper<Accidentes> mapper)
    {
        _mapper = mapper;
        Create();
    }
    
    public async Task Create()
    {
        var task = Task.Run( async ()=> 
        {
            await foreach (var dato in _mapper.CsvToModel("300228-1-accidentes-trafico-detalle-csv.csv"))
            {
                _accidentes.Add(dato);
            }

            await foreach (var dato in _mapper.CsvToModel("300228-2-accidentes-trafico-detalle-csv.csv"))
            {
                _accidentes.Add(dato);
            }

            await foreach (var dato in _mapper.CsvToModel("300228-34-accidentes-trafico-detalle.csv"))
            {
                _accidentes.Add(dato);
            }
        });

        Task.WaitAll(task);
    }

    public async Task<List<Accidentes?>> GetAll()
    {
        return _accidentes;
    }
}