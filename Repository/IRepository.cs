using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentesMadrid.Models;

namespace AccidentesMadrid.Repository;

public interface IRepository<T, TContenedor>
{
    Task Create();

    Task<TContenedor> GetAll();
}