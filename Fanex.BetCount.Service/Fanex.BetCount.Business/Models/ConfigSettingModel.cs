using System;
using System.Collections.Generic;

namespace Fanex.BetCount.Business.Models;

public class ConfigSettingModel
{
    public string SentryClientKey { get; set; }

    public string CategorySentryLog { get; set; }

    public string TargetName { get; set; }

    public string Group { get; set; }

    public bool IsManual { get; set; }

    public DateTime RptDate { get; set; }

    public List<PlatformSpaceSetting> PlatformSpaceSettings { get; set; }

    public List<TotalSpaceSetting> TotalSpaceSettings { get; set; }

    public string LetstalkServiceApi { get; set; }
}