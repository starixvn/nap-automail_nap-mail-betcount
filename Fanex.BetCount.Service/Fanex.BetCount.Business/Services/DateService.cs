using System;

namespace Fanex.BetCount.Business.Services;

public interface DateService
{
    public DateTime RptDate { get; set; }
}

public class DateServiceImp : DateService
{
    public DateTime RptDate { get; set; } = AppConfigService.Config.IsManual
        ? AppConfigService.Config.RptDate
        : DateTime.Today.AddDays(-1);
}