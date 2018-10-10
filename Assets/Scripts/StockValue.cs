using System.Collections.Generic;

public class StockValue {
    public int MinSize { get; set; }
    public int MaxSize { get; set; }
    public int CorporationId { get; set; }
    public int Price { get; set; }
    public int PrimaryShare { get; set; }
    public int SecondaryShare { get; set; }
    public int TertiaryShare { get; set; }

    public StockValue() {
    }

    public StockValue(int minSize, int maxSize, int corpId, int price, int pShare, int sShare, int tShare) {
        MinSize = minSize;
        MaxSize = maxSize;
        CorporationId = corpId;
        Price = price;
        PrimaryShare = pShare;
        SecondaryShare = sShare;
        TertiaryShare = tShare;
    }
}