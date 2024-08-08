using System;
using System.Collections.Generic;
using System.Globalization;
using Src.UpgradeNodes;
using UnityEngine;

namespace Src
{
   public class GameEntryPoint : MonoBehaviour
   {
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
      [SerializeField] private UpgradesRepository upgradesRepository;
      [Header("UI Builders")]
      [SerializeField] private UpgradesTreeUIBuilder developmentUpgradesTreeUIBuilder;
      [SerializeField] private UpgradesTreeUIBuilder safetyUpgradesTreeUIBuilder;
      [SerializeField] private UpgradesTreeUIBuilder publicRelationsUpgradesTreeUIBuilder;

      private GameState _gameState;
      private UpgradeTreesBuilder _upgradeTreesBuilder;
      private List<UpgradeNodeData> _upgradeNodesData;

      private async void Start()
      {
         loadingScreen.SetActive(true);
         _gameState = new GameState
         {
            Cash = initialCash,
            PublicTrust = initialPublicTrust,
            AiDevelopment = initialAiDevelopment,
            Safety = initialSafety,
            Date = DateTime.ParseExact(initialDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)
         };
         
         gameStateUpdater.Init(_gameState, 
            cashDecreaseSpeed, 
            publicTrustDecreaseSpeed, 
            aiDevelopmentDecreaseSpeed, 
            safetyDecreaseSpeed, 
            oneDayInSeconds);
         gameStateView.Init(_gameState, 
            publicTrustMaxValue, 
            aiDevelopmentMaxValue, 
            safetyMaxValue);
         _upgradeTreesBuilder = new UpgradeTreesBuilder(_gameState, gameStateUpdater);
         upgradesRepository.Init();
         var developmentUpgradesNodesData = await upgradesRepository.GetUpgradeNodesData("Development");
         var developmentTree = _upgradeTreesBuilder.GetUpgradesTree(developmentUpgradesNodesData);
         var safetyUpgradesNodesData = await upgradesRepository.GetUpgradeNodesData("Safety");
         var safetyTree = _upgradeTreesBuilder.GetUpgradesTree(safetyUpgradesNodesData);
         var publicRelationsUpgradesNodesData = await upgradesRepository.GetUpgradeNodesData("Public Relations");
         var publicRelationsTree = _upgradeTreesBuilder.GetUpgradesTree(publicRelationsUpgradesNodesData);
         _upgradeNodesData = new List<UpgradeNodeData>();
         _upgradeNodesData.AddRange(developmentUpgradesNodesData);
         _upgradeNodesData.AddRange(safetyUpgradesNodesData);
         _upgradeNodesData.AddRange(publicRelationsUpgradesNodesData);
         developmentUpgradesTreeUIBuilder.BuildTree(developmentTree);
         safetyUpgradesTreeUIBuilder.BuildTree(safetyTree);
         publicRelationsUpgradesTreeUIBuilder.BuildTree(publicRelationsTree);
         loadingScreen.SetActive(false);
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
 