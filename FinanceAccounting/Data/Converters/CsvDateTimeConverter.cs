using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FinanceAccounting.Data.Converters;

public class CsvDateTimeConverter : DefaultTypeConverter
{
    private const string Format = "yyyy-MM-dd HH:mm:ss";
    
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return DateTime.ParseExact(text, Format, CultureInfo.InvariantCulture);
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return ((DateTime)value).ToString(Format, CultureInfo.InvariantCulture);
    }
}