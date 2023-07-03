using System.Globalization;

namespace Fanex.BetCount.Business.Services;

public interface FormatNumberService
{
    string FormatNumber(double number);
}

public class FormatNumberServiceImp : FormatNumberService
{
    public string FormatNumber(double number)
    {
        return number < 1000 ? number.ToString(CultureInfo.InvariantCulture) : $"{number / 1000:#,##0}k";
    }
}