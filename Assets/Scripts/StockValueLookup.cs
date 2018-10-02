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

    public HashSet<StockValue> GenerateStockValueTable(Corporation corp) {
        HashSet<StockValue> stockTable = new HashSet<StockValue>();

        // 0-1, 2-4, and 5-6 have the same values - just different corporations
        switch (corp.Id) {
            case 0:
            case 1:
                stockTable.Add(new StockValue(2, 2, corp.Id, 200, 2000, 1500, 1000));
                stockTable.Add(new StockValue(3, 3, corp.Id, 300, 3000, 2200, 1500));
                stockTable.Add(new StockValue(4, 4, corp.Id, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(5, 5, corp.Id, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(6, 7, corp.Id, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(8, 17, corp.Id, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(18, 27, corp.Id, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(28, 37, corp.Id, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(38, 100, corp.Id, 1000, 10000, 7000, 5000));
                break;

            case 2:
            case 3:
            case 4:
                stockTable.Add(new StockValue(2, 2, corp.Id, 300, 3000, 2200, 1500));
                stockTable.Add(new StockValue(3, 3, corp.Id, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(4, 4, corp.Id, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(5, 5, corp.Id, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(6, 7, corp.Id, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(8, 17, corp.Id, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(18, 27, corp.Id, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(28, 37, corp.Id, 1000, 10000, 7000, 5000));
                stockTable.Add(new StockValue(38, 100, corp.Id, 1100, 11000, 7700, 5500));
                break;

            case 5:
            case 6:
                stockTable.Add(new StockValue(2, 2, corp.Id, 400, 4000, 3000, 2000));
                stockTable.Add(new StockValue(3, 3, corp.Id, 500, 5000, 3700, 2500));
                stockTable.Add(new StockValue(4, 4, corp.Id, 600, 6000, 4200, 3000));
                stockTable.Add(new StockValue(5, 5, corp.Id, 700, 7000, 5000, 3500));
                stockTable.Add(new StockValue(6, 7, corp.Id, 800, 8000, 5700, 4000));
                stockTable.Add(new StockValue(8, 17, corp.Id, 900, 9000, 6200, 4500));
                stockTable.Add(new StockValue(18, 27, corp.Id, 1000, 10000, 7000, 5000));
                stockTable.Add(new StockValue(28, 37, corp.Id, 1100, 11000, 7700, 5500));
                stockTable.Add(new StockValue(38, 100, corp.Id, 1200, 12000, 8200, 6000));
                break;

            default:
                break;
        }

        return stockTable;
    }
}