using Fanex.BetCount.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.BetCount.Business.Services;

public interface MessageContentBuilder
{
    string BuildContent(List<BetCountModel> model);

    string BuildSubject(DateTime rptDate);
}

public class MessageContentBuilderImp : MessageContentBuilder
{
    private readonly FormatNumberService formatService;

    public MessageContentBuilderImp(FormatNumberService formatService)
    {
        this.formatService = formatService;
    }

    private static Dictionary<string, string> TextNeedToReplace => new()
    {
        { "MBC", "C" },
        { "789Y", "Y" },
        { "CN88", "C8" },
        { "Licensee", "L" },
        { "LIC", "L" }
    };

    public string BuildContent(List<BetCountModel> model)
    {
        var array = new List<string>();
        var texts = TextNeedToReplace;
        var result = model.Where(s => !s.Site.Equals("CN88")).ToList();

        result.ForEach(site =>
        {
            var siteName = site.Site.ToUpper();
            var newText = texts.ContainsKey(siteName) ? texts[siteName] : siteName;
            var siteTotal = $"{newText} - TOTAL";
            var totalSpace = AddTotalSpace(siteTotal);
            array.Add($"{siteTotal}: {totalSpace}{formatService.FormatNumber(site.Total)}");
            site.Platforms.ForEach(platform =>
            {
                var firstSpace = AddFistSpacePlatform(platform.Platform);
                var secondSpace = AddSecondSpacePlatform(platform.Platform);
                var percent = Convert.ToDecimal(platform.BetCount) / Convert.ToDecimal(site.Total);

                array.Add($"{firstSpace}{platform.Platform}: {secondSpace}{formatService.FormatNumber(platform.BetCount)} ({percent:#,##0%})");
            });

            array.Add("--------------------");
        });

        if (array.Count > 0)
        {
            array.RemoveAt(array.Count - 1);
        }

        return string.Join(Environment.NewLine, array);
    }

    private string AddFistSpacePlatform(string platform)
    {
        var result = new StringBuilder();
        var spaceSetting = AppConfigService.Config.PlatformSpaceSettings.FirstOrDefault(s => string.Equals(s.Platform, platform, StringComparison.CurrentCultureIgnoreCase));
        if (spaceSetting == null)
        {
            return result.ToString();
        }

        for (var i = 0; i < spaceSetting.Space1; i++)
        {
            result.Append(" ");
        }

        return result.ToString();
    }

    private string AddSecondSpacePlatform(string platform)
    {
        var result = new StringBuilder();
        var spaceSetting = AppConfigService.Config.PlatformSpaceSettings.FirstOrDefault(s => string.Equals(s.Platform, platform, StringComparison.CurrentCultureIgnoreCase));
        if (spaceSetting == null)
        {
            return result.ToString();
        }

        for (var i = 0; i < spaceSetting.Space2; i++)
        {
            result.Append(" ");
        }

        return result.ToString();
    }

    private string AddTotalSpace(string siteTotal)
    {
        var result = new StringBuilder();
        var spaceSetting = AppConfigService.Config.TotalSpaceSettings.FirstOrDefault(s => string.Equals(s.SiteTotal, siteTotal, StringComparison.CurrentCultureIgnoreCase));
        if (spaceSetting == null)
        {
            return result.ToString();
        }

        for (var i = 0; i < spaceSetting.Space; i++)
        {
            result.Append(" ");
        }

        return result.ToString();
    }

    public string BuildSubject(DateTime rptDate)
    {
        return $"S3 Txn Count: {rptDate:yyyyMMdd}";
    }
}