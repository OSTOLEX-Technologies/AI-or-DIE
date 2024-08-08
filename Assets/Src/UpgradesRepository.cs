using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace Src
{
    public class UpgradesRepository : MonoBehaviour
    {
        private const string serviceAccountEmail = "ai-or-die-service-account@ai-or-die.iam.gserviceaccount.com";
        private const string spreadsheetId = "1frM0gL_PZMigqTeWsxSNAw0lJO4wS3eMmiMXXnZTtSk";
        private const string sheetNameAndRange = "Development";
        private const string credentailsFileName = "ai-or-die-a841005cf3df";

        
        
        public void Start()
        {
            TextAsset p12 = Resources.Load<TextAsset>(credentailsFileName);
            var certificate = new X509Certificate2(p12.bytes, "notasecret", X509KeyStorageFlags.Exportable);
        
            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = new[] { SheetsService.Scope.SpreadsheetsReadonly } 
                    /*
                 Without this scope, it will :
                 GoogleApiException: Google.Apis.Requests.RequestError
                 Request had invalid authentication credentials. Expected OAuth 2 access token, login cookie or other valid authentication credential.
                 lol..
                 */
                }.FromCertificate(certificate));
        
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheetNameAndRange);

            StringBuilder sb = new StringBuilder();

            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (IList<object> row in values)
                {
                    foreach(object cell in row)
                    {
                        sb.Append(cell.ToString() + " ");
                    }
                    //Concat the whole row
                    Debug.Log(sb.ToString());
                    sb.Clear();
                }
            }
            else
            {
                Debug.Log("No data found.");
            }
        
        }
    }
}
