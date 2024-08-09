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
    public class UpgradesRepository : MonoBehaviour
    {
        private const string serviceAccountEmail = "ai-or-die-service-account@ai-or-die.iam.gserviceaccount.com";
        private const string spreadsheetId = "1frM0gL_PZMigqTeWsxSNAw0lJO4wS3eMmiMXXnZTtSk";
        private const string credentailsFileName = "ai-or-die-f85e0531fb19";

        private SheetsService _sheetsService;
        
        public void Init()
        {
            Debug.Log("Loading credentials...");
            TextAsset p12 = Resources.Load<TextAsset>(credentailsFileName);
            var certificate = new X509Certificate2(p12.bytes, "notasecret", X509KeyStorageFlags.Exportable);
        
            Debug.Log("Creating credentials...");
            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = new[] { SheetsService.Scope.SpreadsheetsReadonly } 
                }.FromCertificate(certificate));
            
            Debug.Log("Creating service...");
            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });
        }

        public async Task<List<UpgradeNodeData>> GetUpgradeNodesData(string tableName)
        {
            Debug.Log("Loading upgrade nodes data...");
            SpreadsheetsResource.ValuesResource.GetRequest request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, tableName);
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
