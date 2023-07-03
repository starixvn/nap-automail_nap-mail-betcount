using Fanex.Data.Repository;

namespace Fanex.BetCount.Business.Criteria;

public class GetDWStatusCriteria : CriteriaBase
{
    public string TaskIDList { get; set; } = "17";

    public override string GetSettingKey()
    {
        return "DailyJob_CheckTasksStatus";
    }

    public override bool IsValid() => true;
}