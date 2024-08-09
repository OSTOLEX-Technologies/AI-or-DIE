using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace Src.GameOverConditions
{
    public class GameOverScenariosRepository
    {
        private readonly int _cashMaxValue;
        private readonly int _aiDevelopmentMaxValue;
        private readonly int _safetyMaxValue;
        private readonly int _publicTrustMaxValue;
        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;

        public GameOverScenariosRepository(SheetsService sheetsService, 
            string spreadsheetId, 
            int aiDevelopmentMaxValue, 
            int safetyMaxValue, 
            int publicTrustMaxValue, int cashMaxValue)
        {
            _sheetsService = sheetsService;
            _spreadsheetId = spreadsheetId;
            _aiDevelopmentMaxValue = aiDevelopmentMaxValue;
            _safetyMaxValue = safetyMaxValue;
            _publicTrustMaxValue = publicTrustMaxValue;
            _cashMaxValue = cashMaxValue;
        }

        public async Task<List<GameOverScenario>> GetScenarios(string tableName)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, tableName);
            ValueRange response = await request.ExecuteAsync();
            IList<IList<object>> values = response.Values;
            List<GameOverScenario> scenarios = new List<GameOverScenario>();
            if (values != null && values.Count > 0)
            {
                bool firstRowSkipped = false;
                foreach (IList<object> row in values)
                {
                    if (!firstRowSkipped)
                    {
                        firstRowSkipped = true;
                        continue;
                    }
                    try
                    {
                        var name = row[0].ToString();
                        var cashCondition = ParseToCondition(row[1].ToString(), _cashMaxValue);
                        var aiDevelopmentCondition = ParseToCondition(row[2].ToString(), _aiDevelopmentMaxValue);
                        var safetyCondition = ParseToCondition(row[3].ToString(), _safetyMaxValue);
                        var publicTrustCondition = ParseToCondition(row[4].ToString(), _publicTrustMaxValue);
                        var description = row[5].ToString();
                        var imageUrl = row[6].ToString();
                        var imageTexture = await UrlTexturesLoader.LoadTexture(imageUrl);
                        scenarios.Add(new GameOverScenario(aiDevelopmentCondition, safetyCondition, publicTrustCondition, cashCondition)
                        {
                            Name = name,
                            Description = description,
                            Image = imageTexture
                        });
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }

            return scenarios;
        }

        private ICondition ParseToCondition(string conditionString, int maxValue)
        {
            if (conditionString == "")
            {
                return new AlwaysTrueCondition();
            }
            if (conditionString[0] == '>')
            {
                var valueSubstring = conditionString.Substring(1);
                var percent = int.Parse(valueSubstring) / 100f;
                return new RelativeBiggerThanCondition(maxValue, percent);
            }
            if (conditionString[0] == '<')
            {
                var valueSubstring = conditionString.Substring(1);
                var percent = int.Parse(valueSubstring) / 100f;
                return new RelativeSmallerThanCondition(maxValue, percent);
            }
            var value = int.Parse(conditionString);
            return new EqualsCondition(value);
        }
    }
}