using Microsoft.Data.Analysis;

namespace AccidentesMadrid.AccidentesMadrid.Test;

public class features
{
    public static DataFrame CreateFakeDataFrame()
    {
        var col1 = new StringDataFrameColumn("num_expediente", new[] { "2025S000056" });
        var col2 = new Int32DataFrameColumn("cod_distrito", new[] { 3 });

        return new DataFrame(col1, col2);
    }
}