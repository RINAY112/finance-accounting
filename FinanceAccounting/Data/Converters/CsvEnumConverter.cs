using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FinanceAccounting.Data.Converters;

public class CsvEnumConverter<T> : DefaultTypeConverter where T : struct, Enum
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return Enum.Parse<T>(text);
    }
    
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value.ToString();
    }
}