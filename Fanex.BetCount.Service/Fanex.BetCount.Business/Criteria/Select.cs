using Fanex.Data.Repository;
using System;

namespace Fanex.BetCount.Business.Criteria;

public class Select : CriteriaBase
{
    public DateTime RptDate { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_Betcount_Platform_Site";

    public override bool IsValid() => true;
}