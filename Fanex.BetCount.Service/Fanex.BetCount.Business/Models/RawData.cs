namespace Fanex.BetCount.Business.Models;

public class RawData
{
    public string Site { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public int BetCount { get; set; }
}