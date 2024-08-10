using Src.GameOverConditions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI labelTextMesh;
        [SerializeField] private TextMeshProUGUI descriptionTextMesh;
        [SerializeField] private Image background;

        public void ShowGameOver(GameOverScenario scenario)
        {
            gameOverScreen.SetActive(true);
            labelTextMesh.text = "<b>Game Over:</b> " + scenario.Name;
            descriptionTextMesh.text = scenario.Description;
            background.material.mainTexture = scenario.Image;
        }
    }
}