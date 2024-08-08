using System;
using System.Globalization;
using UnityEngine;

namespace Src
{
   public class GameEntryPoint : MonoBehaviour
   {
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

      private GameState _gameState;

      private void Start()
      {
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
      }
   }
}
 