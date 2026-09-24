namespace AccidentesMadrid.Service;

public interface IService<T>
{
    Dictionary<int, TimeSpan> ImprimirConsultasLinq();
    Dictionary<int, TimeSpan> ImprimirConsultasDataFrame();
}