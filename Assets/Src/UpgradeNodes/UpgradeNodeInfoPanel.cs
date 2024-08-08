using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.UpgradeNodes
{
    public class UpgradeNodeInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image image;
        
        public void SetDataForNode(UpgradeNode node)
        {
            costText.gameObject.SetActive(true);
            var data = node.Data;
            descriptionText.text = data.Description;
            costText.text = "$" + data.Cost + "K";
            nameText.text = data.Name;
            image.material.mainTexture = node.Data.Image;
            if (node.IsBought)
            {
                costText.gameObject.SetActive(false);
            }
        }
        
    }
}