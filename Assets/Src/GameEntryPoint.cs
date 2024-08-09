using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Src.GameOverConditions;
using Src.UpgradeNodes;
using UnityEngine;

namespace Src
{
   public class GameEntryPoint : MonoBehaviour
   {
      private const string serviceAccountEmail = "ai-or-die-service-account@ai-or-die.iam.gserviceaccount.com";
      private const string spreadsheetId = "1frM0gL_PZMigqTeWsxSNAw0lJO4wS3eMmiMXXnZTtSk";
      private const string credentailsFileName = "ai-or-die-f85e0531fb19";
      
      [SerializeField] private GameObject loadingScreen;
      [Header("Initial game state values")]
      [SerializeField] private int initialCash;
      [SerializeField] private int initialPublicTrust;
      [SerializeField] private int initialAiDevelopment;
      [SerializeField] private int initialSafety;
      [SerializeField] private string initialDate;
      [Header("Initial change speed values")]
      [SerializeField] private int cashDecreaseSpeed;
      [SerializeField] private int publicTrustDecreaseSpeed;
      [SerializeField] private int aiDevelopmentDecreaseSpeed;
      [SerializeField] private int safetyDecreaseSpeed;
      [SerializeField] private int oneDayInSeconds;
      [Header("Stats max values")]
      [SerializeField] private int publicTrustMaxValue;
      [SerializeField] private int aiDevelopmentMaxValue;
      [SerializeField] private int safetyMaxValue;
      [Header("Dependencies")]
      [SerializeField] private GameStateUpdater gameStateUpdater;
      [SerializeField] private GameStateView gameStateView;
      [Header("UI Builders")]
      [SerializeField] private UpgradesTreeUIBuilder developmentUpgradesTreeUIBuilder;
      [SerializeField] private UpgradesTreeUIBuilder safetyUpgradesTreeUIBuilder;
      [SerializeField] private UpgradesTreeUIBuilder publicRelationsUpgradesTreeUIBuilder;

      private GameState _gameState;
      private UpgradeTreesBuilder _upgradeTreesBuilder;
      private List<UpgradeNodeData> _upgradeNodesData;
      private SheetsService _sheetsService;
      
      //Repositories
      private UpgradesRepository _upgradesRepository;
      private GameOverScenariosRepository _gameOverScenariosRepository;

      private async void Start()
      {
         loadingScreen.SetActive(true);
         InitializeSheetsService();
         InitializeGameState();
         InitializeGameStateUpdater();
         gameStateUpdater.Pause();
         InitializeGameStateView();
         await LoadAndBuildUpgradeTrees();
         _gameOverScenariosRepository = new GameOverScenariosRepository(_sheetsService, 
            spreadsheetId, 
            aiDevelopmentMaxValue, 
            safetyMaxValue, 
            publicTrustMaxValue, 
            10000000);
         var scenarios = await _gameOverScenariosRepository.GetScenarios("Finale");
         foreach (var scenario in scenarios)
         {
            Debug.Log(scenario);
         }
         loadingScreen.SetActive(false);
         gameStateUpdater.Resume();
      }

      private void InitializeSheetsService()
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

      private void InitializeGameState()
      {
         _gameState = new GameState
         {
            Cash = initialCash,
            PublicTrust = initialPublicTrust,
            AiDevelopment = initialAiDevelopment,
            Safety = initialSafety,
            Date = DateTime.ParseExact(initialDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)
         };
      }

      private void InitializeGameStateUpdater()
      {
         gameStateUpdater.Init(_gameState, 
            cashDecreaseSpeed, 
            publicTrustDecreaseSpeed, 
            aiDevelopmentDecreaseSpeed, 
            safetyDecreaseSpeed, 
            oneDayInSeconds);
      }

      private void InitializeGameStateView()
      {
         gameStateView.Init(_gameState, 
            publicTrustMaxValue, 
            aiDevelopmentMaxValue, 
            safetyMaxValue);
      }

      private async Task LoadAndBuildUpgradeTrees()
      {
         _upgradeTreesBuilder = new UpgradeTreesBuilder(_gameState, gameStateUpdater);
         _upgradesRepository = new UpgradesRepository(_sheetsService, spreadsheetId);
         var developmentUpgradesNodesData = await _upgradesRepository.GetUpgradeNodesData("Development");
         var developmentTree = _upgradeTreesBuilder.GetUpgradesTree(developmentUpgradesNodesData);
         var safetyUpgradesNodesData = await _upgradesRepository.GetUpgradeNodesData("Safety");
         var safetyTree = _upgradeTreesBuilder.GetUpgradesTree(safetyUpgradesNodesData);
         var publicRelationsUpgradesNodesData = await _upgradesRepository.GetUpgradeNodesData("Public Relations");
         var publicRelationsTree = _upgradeTreesBuilder.GetUpgradesTree(publicRelationsUpgradesNodesData);
         _upgradeNodesData = new List<UpgradeNodeData>();
         _upgradeNodesData.AddRange(developmentUpgradesNodesData);
         _upgradeNodesData.AddRange(safetyUpgradesNodesData);
         _upgradeNodesData.AddRange(publicRelationsUpgradesNodesData);
         developmentUpgradesTreeUIBuilder.BuildTree(developmentTree);
         safetyUpgradesTreeUIBuilder.BuildTree(safetyTree);
         publicRelationsUpgradesTreeUIBuilder.BuildTree(publicRelationsTree);
      }

      private void OnDestroy()
      {
         foreach (var upgradeNodeData in _upgradeNodesData)
         {
            Destroy(upgradeNodeData.Image);
            Destroy(upgradeNodeData.Icon);
         }
      }
   }
}
 