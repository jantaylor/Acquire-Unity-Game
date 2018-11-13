public static class Constants {

    #region Player

    public const int DefaultNumberOfPlayers = 2;
    public const int MaxNumberOfPlayers = 6;

    #endregion

    #region Options

    public const bool DefaultSinglePlayer = false;

    #endregion

    #region Corporation

    public const int NumberOfTilesForSafeCorporation = 11;
    public const int NumberOfStocksPerCorporation = 24;
    public const int NumberOftilesForGameOver = 41;

    #endregion

    #region Board

    public const int NumberOfTiles = 100;

    #endregion

    #region Rules

    public const bool ShowGameLog = true;
    public const int ShowPurchasedStocks = 1; // 1 - Yes, 2 - Only amount (no corp names), 3 - No

    #endregion

    #region AI

    public const int DefaultAiDifficulty = 1;
    public const int DefaultNumberOfAi = 1;
    public const int MaxNumberOfAi = 5;

    #endregion

    #region Networking

    public const int DefaultPort = 25001;
    public const string MasterServerIpAddress = "127.0.0.1";

    #endregion

    #region HUD

    public const int PlayerHUDPosX = 65;
    public const int PlayerHUDPosY = -50;

    #endregion
}
