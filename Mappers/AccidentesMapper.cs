using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccidentesMadrid.Models;
using Microsoft.Data.Analysis;

namespace AccidentesMadrid.Mappers;

public class AccidentesMapper: IMapper<Accidentes>
{
    public async IAsyncEnumerable<Accidentes> CsvToModel(string source)
    {
        string path = Path.Combine(AppContext.BaseDirectory, source);

        if (!File.Exists(path))
        {
            Console.WriteLine($"El fichero no existe: {path}");
        }

        using var reader = new StreamReader(path);
        reader.ReadLine(); // Salta la primera línea (cabecera)
        var final = false;
        for (int i = 0; i < 39; i++)
        {
            
            var line = await reader.ReadLineAsync();
            if (line == null)
            {
                final = false;
                break;
            }

            var accidente = FromCsv(line);
            yield return accidente;
        }
    }

    public async Task<DataFrame> CsvToDT(string source)
    {
        var dataFrame = DataFrame.LoadCsv(source, separator: ';', header: true);
        return await Task.FromResult(dataFrame);
    }

    public static Accidentes FromCsv(string line)
    {
        var row = line.Split(';');

        // Helper para evitar repetir código
        string Get(int i) => i < row.Length ? row[i].Trim() : "";

        // 1) num_expediente
        string numExpediente = Get(0);

        // 2) fecha dd/MM/yyyy
        DateTime fecha = DateTime.ParseExact(Get(1), "dd/MM/yyyy", null);

        // 3) hora → TimeOnly
        TimeOnly hora = TimeOnly.Parse(Get(2));

        // 4) localizacion
        string localizacion = Get(3);

        // 5) numero → puede ser texto, vacío o no numérico
        int? numero = null;
        if (int.TryParse(Get(4), out int n))
            numero = n;

        // 6) cod_distrito
        int codDistrito = int.Parse(Get(5));

        // 7) distrito
        string distrito = Get(6);

        // 8) tipo_accidente → enum
        TipoAccidente tipoAccidente =
            Enum.TryParse<TipoAccidente>(Get(7), out var ta) ? ta : TipoAccidente.OtrasCausas;

        // 9) estado_meteorológico → puede ser vacío
        string? estadoMeteorologico = string.IsNullOrWhiteSpace(Get(8)) ? null : Get(8);

        // 10) tipo_vehiculo
        string tipoVehiculo = Get(9);

        // 11) tipo_persona
        string tipoPersona = Get(10);

        // 12) rango_edad
        string rangoEdad = Get(11);

        // 13) sexo → enum
        TipoSexo sexo =
            Enum.TryParse<TipoSexo>(Get(12), out var sx) ? sx : TipoSexo.NoAsignado;

        // 14) cod_lesividad → puede ser vacío
        int? codLesividad = null;
        if (int.TryParse(Get(13), out int cl))
            codLesividad = cl;

        // 15) lesividad
        string lesividad = Get(14);

        // 16) coordenada_x_utm
        double coordX = double.Parse(Get(15));

        // 17) coordenada_y_utm
        double coordY = double.Parse(Get(16));

        // 18) positiva_alcohol → "S" / "N" / vacío
        int? positivaAlcohol = null;
        string alc = Get(17);
        if (alc == "S") positivaAlcohol = 1;
        if (alc == "N") positivaAlcohol = 0;

        // 19) positiva_droga → "S" / "N" / vacío
        bool? positivaDroga = null;
        string drg = Get(18);
        if (drg == "S") positivaDroga = true;
        if (drg == "N") positivaDroga = false;

        return new Accidentes
        (
            NumExpediente : numExpediente,
            Fecha : fecha,
            Hora : hora,
            Localizacion : localizacion,
            Numero : numero,
            CodDistrito : codDistrito,
            Distrito : distrito,
            TipoAccidente : tipoAccidente,
            EstadoMeteorológico : estadoMeteorologico,
            TipoVehiculo : tipoVehiculo,
            TipoPersona : tipoPersona,
            RangoEdad : rangoEdad,
            Sexo : sexo,
            CodLesividad : codLesividad,
            Lesividad : lesividad,
            CoordenadaXUtm : coordX,
            CoordenadaYUtm : coordY,
            PositivaAlcohol : positivaAlcohol,
            PositivaDroga : positivaDroga
        );
    }
}
