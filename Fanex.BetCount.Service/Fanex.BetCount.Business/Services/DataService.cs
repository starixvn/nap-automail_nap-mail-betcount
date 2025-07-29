using Fanex.BetCount.Business.Criteria;
using Fanex.BetCount.Business.Models;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.BetCount.Business.Services;

public interface DataService
{
    List<BetCountModel> Query();

    bool IsDataAvailable();
}

public class DataServiceImp(IDynamicRepository dynamicRepository, DateService dateService)
    : DataService
{
    public bool IsDataAvailable()
    {
        if (AppConfigService.Config.IsManual)
        {
            return true;
        }

        var result = QueryDwStatus().DataIsReady;
        return result;
    }

    public List<BetCountModel> Query()
    {
        var result = new List<BetCountModel>();
        var rawData = QueryRawData();
        rawData.Select(s => s.Site)
            .Distinct()
            .ToList()
            .ForEach(site =>
            {
                var platformBySite = rawData.Where(data => data.Site == site).ToList();
                result.Add(new BetCountModel
                {
                    Site = site,
                    Platforms = platformBySite,
                    Total = platformBySite.Sum(t => t.BetCount)
                });
            });

        return result;
    }

    private TaskStatusModel QueryDwStatus()
    {
        return dynamicRepository.Get<TaskStatusModel>(new GetDWStatusCriteria { TaskIDList = "17" });
    }

    private List<RawData> QueryRawData()
    {
        var criteria = new Select
        {
            RptDate = dateService.RptDate
        };

        var rawData = dynamicRepository.Fetch<RawData>(criteria).ToList();
        rawData.ForEach(row => { row.BetCount /= 1000; });
        return rawData;
    }
}