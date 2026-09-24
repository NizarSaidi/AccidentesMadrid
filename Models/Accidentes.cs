using System;

namespace AccidentesMadrid.Models;

public record Accidentes(
    string NumExpediente,
    /*AAAASNNNNNN, donde:
    • AAAA es el año del accidente.
    • S cuando se trata de un expediente con
      accidente.
    */
    DateTime Fecha,
    /* formato dd/mm/aaaa */
    TimeOnly Hora,
    /* se establece en rangos horarios
     de 1 hora*/
    string Localizacion,
    /*calle 1 ‐ calle 2 (cruce) o una calle */
    int? Numero,
    /*Número de la calle, cuando tiene sentido */
    int CodDistrito,
    /*Código del distrito*/
    string Distrito,
    /*Nombre del Distrito */
    TipoAccidente TipoAccidente,
    /*
     * ColisionDoble,
     * ColisionMultiple,
     * Alcance,
     * ChoqueContraObstaculo,
     * AtropelloAPersona,
     * Vuelco,
     * Caida,
     * OtrasCausas
     */
    string? EstadoMeteorológico,
    /* Descripción climatología */
    string TipoVehiculo,
    /* tipo vehículo implicado */
    string TipoPersona,
    /* tipo persona implicada */
    string RangoEdad,
    /* tramo edad persona afectada */
    TipoSexo Sexo,
    /* Puede ser :hombre Mujer o no asignado */
    int? CodLesividad,
    /*
     * 01 Atención en urgencias sin posterior ingreso. - LEVE
     * 02 Ingreso inferior o igual a 24 horas - LEVE
     * 03 Ingreso superior a 24 horas. - GRAVE
     * 04 Fallecido 24 horas - FALLECIDO
     * 05 Asistencia sanitaria ambulatoria con posterioridad - LEVE
     * 06 Asistencia sanitaria inmediata en centro de salud o mutua - LEVE
     * 07 Asistencia sanitaria solo en el lugar del accidente - LEVE
     * 14 Sin asistencia sanitaria
     * 77 Se desconoce
     * En blanco Sin asistencia sanitaria
     */
    string Lesividad,
    /* Descripción lesividad */
    double CoordenadaXUtm,
    /* ubicación coordenada x */
    double CoordenadaYUtm,
    /* ubicación coordenada y */
    int? PositivaAlcohol,
    /* Puede ser NULL o 1 */
    bool? PositivaDroga
    /* Puede ser N o S */
) {};