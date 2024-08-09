using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Src
{
    public class GameConfigRepository
    {
        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;

        public GameConfigRepository(SheetsService sheetsService, string spreadsheetId)
        {
            _sheetsService = sheetsService;
            _spreadsheetId = spreadsheetId;
        }

        public async Task<GameConfig> LoadGameConfig(string tableName)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, tableName);
            ValueRange response = await request.ExecuteAsync();
            IList<IList<object>> values = response.Values;
            var initialCash = int.Parse(values[1][1].ToString());
            var initialPublicTrust = int.Parse(values[2][1].ToString());
            var initialAiDevelopment = int.Parse(values[3][1].ToString());
            var initialSafety = int.Parse(values[4][1].ToString());
            var initialDate = DateTime.ParseExact(values[5][1].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var cashDecreaseSpeed = int.Parse(values[6][1].ToString());
            var publicTrustDecreaseSpeed = int.Parse(values[7][1].ToString());
            var aiDevelopmentDecreaseSpeed = int.Parse(values[8][1].ToString());
            var safetyDecreaseSpeed = int.Parse(values[9][1].ToString());
            var oneDayInSeconds = int.Parse(values[10][1].ToString());
            var publicTrustMaxValue = int.Parse(values[11][1].ToString());
            var aiDevelopmentMaxValue = int.Parse(values[12][1].ToString());
            var safetyMaxValue = int.Parse(values[13][1].ToString());
            var cashBubbleAmount = int.Parse(values[14][1].ToString());
            return new GameConfig()
            {
                InitialCash = initialCash,
                InitialPublicTrust = initialPublicTrust,
                InitialAiDevelopment = initialAiDevelopment,
                InitialSafety = initialSafety,
                InitialDate = initialDate,
                CashDecreaseSpeed = cashDecreaseSpeed,
                PublicTrustDecreaseSpeed = publicTrustDecreaseSpeed,
                AiDevelopmentDecreaseSpeed = aiDevelopmentDecreaseSpeed,
                SafetyDecreaseSpeed = safetyDecreaseSpeed,
                OneDayInSeconds = oneDayInSeconds,
                PublicTrustMaxValue = publicTrustMaxValue,
                AiDevelopmentMaxValue = aiDevelopmentMaxValue,
                SafetyMaxValue = safetyMaxValue,
                CashBubbleAmount = cashBubbleAmount
            };
        }
    }
}