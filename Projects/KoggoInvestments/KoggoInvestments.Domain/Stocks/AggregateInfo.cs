using Newtonsoft.Json;

namespace KoggoInvestments.Domain.Stocks;

public class StockBarInfo
{
    [JsonProperty("v")]
    public long Volume { get; set; }
    [JsonProperty("vw")]
    public decimal VolumeWeighted { get; set; }
    [JsonProperty("o")]
    public decimal OpeningPrice { get; set; }
    [JsonProperty("c")]
    public decimal ClosingPrice { get; set; }
    [JsonProperty("h")]
    public decimal HighestPrice { get; set; }
    [JsonProperty("l")]
    public decimal LowesPrice { get; set; }
    [JsonProperty("t")]
    public long Timestamp { get; set; }
    [JsonProperty("n")]
    public int NumberOfTrades { get; set; }
}


public class Stock
{
    public string Ticker { get; set; }
    public int QueryCount { get; set; }
    public int ResultsCount { get; set; }
    public bool Adjusted { get; set; }
    public List<StockBarInfo> Results { get; set; }
    public string Status { get; set; }
    public string RequestId { get; set; }
    public int Count { get; set; }
    public string NextUrl { get; set; }
}