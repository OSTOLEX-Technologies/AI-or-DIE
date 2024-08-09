using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Src.UpgradeNodes;
using UnityEngine;

namespace Src
{
    public class UpgradesRepository
    {
        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;
        
        public UpgradesRepository(SheetsService sheetsService, string spreadsheetId)
        {
            _sheetsService = sheetsService;
            _spreadsheetId = spreadsheetId;
        }

        public async Task<List<UpgradeNodeData>> GetUpgradeNodesData(string tableName)
        {
            Debug.Log("Loading upgrade nodes data...");
            SpreadsheetsResource.ValuesResource.GetRequest request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, tableName);
            ValueRange response = await request.ExecuteAsync();
            IList<IList<object>> values = response.Values;
            List<UpgradeNodeData> upgradeNodesData = new List<UpgradeNodeData>();
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
                        var node = new UpgradeNodeData();
                        node.Id = row[0].ToString();
                        node.PreviousNodeId = row[1].ToString();
                        node.Name = row[2].ToString();
                        Debug.Log(row[3].ToString());
                        node.Cost = int.Parse(row[3].ToString().Replace(",", ""));
                        node.PublicTrustOneTimeChange = int.Parse(row[4].ToString());
                        node.AiDevelopmentOneTimeChange = int.Parse(row[5].ToString());
                        node.SafetyOneTimeChange = int.Parse(row[6].ToString());
                        node.PublicTrustDecreaseSpeedDelta = int.Parse(row[7].ToString());
                        node.AiDevelopmentDecreaseSpeedDelta = int.Parse(row[8].ToString());
                        node.SafetyDecreaseSpeedDelta = int.Parse(row[9].ToString());
                        node.Description = row[10].ToString();
                        var imageUrl = row[11].ToString();
                        Debug.Log(imageUrl);
                        node.Image = await UrlTexturesLoader.LoadTexture(imageUrl);
                        var iconUrl = row[12].ToString();
                        Debug.Log(iconUrl);
                        node.Icon = await UrlTexturesLoader.LoadTexture(iconUrl);
                        upgradeNodesData.Add(node);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                }
            }
            return upgradeNodesData;
        }
    }
}
