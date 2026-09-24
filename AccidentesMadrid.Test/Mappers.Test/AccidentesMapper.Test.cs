using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;
using NUnit.Framework;

namespace AccidentesMadrid.AccidentesMadrid.Test.Mappers.Test;

[TestFixture]
public class AccidentesMapper_Test
{
    [Test]
    public void FromCsv_ParsesCorrectly()
    {
        string csv = "2025S000056;01/01/2025;0:49:00;CALLE A;NO_NUM;3;Centro;ColisionDoble;;Coche;Pasajero;30-39;Hombre;01;Lesión leve;123.45;678.90;S;N";

        var acc = AccidentesMapper.FromCsv(csv);

        Assert.AreEqual("2025S000056", acc.NumExpediente);
        Assert.AreEqual(new DateTime(2025, 1, 1), acc.Fecha);
        Assert.AreEqual(new TimeOnly(0, 49), acc.Hora);
        Assert.AreEqual("CALLE A", acc.Localizacion);

        // numero no numérico → null
        Assert.IsNull(acc.Numero);

        Assert.AreEqual(3, acc.CodDistrito);
        Assert.AreEqual("Centro", acc.Distrito);
        Assert.AreEqual(TipoAccidente.ColisionDoble, acc.TipoAccidente);

        Assert.IsNull(acc.EstadoMeteorológico);
        Assert.AreEqual("Coche", acc.TipoVehiculo);
        Assert.AreEqual("Pasajero", acc.TipoPersona);
        Assert.AreEqual("30-39", acc.RangoEdad);
        Assert.AreEqual(TipoSexo.Hombre, acc.Sexo);

        Assert.AreEqual(1, acc.CodLesividad);
        Assert.AreEqual("Lesión leve", acc.Lesividad);

        Assert.AreEqual(123.45, acc.CoordenadaXUtm);
        Assert.AreEqual(678.90, acc.CoordenadaYUtm);

        Assert.AreEqual(1, acc.PositivaAlcohol);
        Assert.AreEqual(false, acc.PositivaDroga);
    }
    [Test]
    public async Task CsvToModel_ReadsCorrectNumberOfLines()
    {
        string tempFile = Path.GetTempFileName();

        File.WriteAllLines(tempFile, new[]
        {
            "header;header;header;header;header;header;header;header;header;header;header;header;header;header;header;header;header;header;header",
            "2025S000056;01/01/2025;0:49:00;CALLE A;1;3;Centro;ColisionDoble;;Coche;Pasajero;30-39;Hombre;01;Lesión leve;123.45;678.90;S;N",
            "2025S000057;01/01/2025;1:00:00;CALLE B;2;4;Sur;Alcance;;Moto;Conductor;40-49;Mujer;02;Lesión leve;111.11;222.22;N;S"
        });

        var mapper = new AccidentesMapper();
        var result = new List<Accidentes>();

        await foreach (var item in mapper.CsvToModel(tempFile))
            result.Add(item);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("2025S000056", result[0].NumExpediente);
        Assert.AreEqual("2025S000057", result[1].NumExpediente);
    }
    [Test]
    public async Task CsvToDT_LoadsDataFrameCorrectly()
    {
        string tempFile = Path.GetTempFileName();

        File.WriteAllLines(tempFile, new[]
        {
            "num_expediente;fecha;hora;localizacion;numero;cod_distrito;distrito;tipo_accidente;estado_meteorologico;tipo_vehiculo;tipo_persona;rango_edad;sexo;cod_lesividad;lesividad;coordenada_x_utm;coordenada_y_utm;positiva_alcohol;positiva_droga",
            "2025S000056;01/01/2025;0:49:00;CALLE A;1;3;Centro;ColisionDoble;;Coche;Pasajero;30-39;Hombre;01;Lesión leve;123.45;678.90;S;N"
        });

        var mapper = new AccidentesMapper();
        var df = await mapper.CsvToDT(tempFile);

        Assert.AreEqual(1, df.Rows.Count);
        Assert.AreEqual(19, df.Columns.Count);

        Assert.AreEqual("2025S000056", df.Rows[0]["num_expediente"]);
        Assert.AreEqual("Centro", df.Rows[0]["distrito"]);
    }
}