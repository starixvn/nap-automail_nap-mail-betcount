using System;
using System.Globalization;

namespace Fanex.BetCount.Business.Services;

public interface FormatNumberService
{
    string CutNumberOff(decimal value);
}

public class FormatNumberServiceImp : FormatNumberService
{
    public string CutNumberOff(decimal value)
    {
        switch (Math.Abs(value))
        {
            case < 0.001m:
                return "0";

            case < 1000m:
                {
                    var rounded = Math.Round(value, 0);
                    if (Math.Abs(value - rounded) < 0.001m)
                    {
                        return ((int)rounded).ToString();
                    }

                    var decimalRounded = Math.Round(value, 2);
                    var result = decimalRounded.ToString("F2", CultureInfo.InvariantCulture);
                    return result.TrimEnd('0').TrimEnd('.');
                }
            case < 1000000m:
                {
                    var divided = value / 1000m;
                    var rounded = Math.Round(divided, 1);
                    return rounded.ToString("F1", CultureInfo.InvariantCulture) + "k";
                }
            case < 1000000000m:
                {
                    var divided = value / 1000000m;
                    var rounded = Math.Round(divided, 2);
                    return rounded.ToString("F2", CultureInfo.InvariantCulture) + "m";
                }
        }

        var billionDivided = value / 1000000000m;
        var billionRounded = Math.Round(billionDivided, 2);
        return billionRounded.ToString("F2", CultureInfo.InvariantCulture) + "b";
    }
}