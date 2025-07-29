using System.Collections.Generic;

namespace Fanex.BetCount.Business.Models;

public class BetCountModel
{
    public string Site { get; set; } = string.Empty;

    public int Total { get; set; }

    public List<RawData> Platforms { get; set; } = [];
}