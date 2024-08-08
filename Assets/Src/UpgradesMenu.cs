using UnityEngine;

namespace Src
{
    public class UpgradesMenu : MonoBehaviour
    {
        [SerializeField] private GameObject developmentSection;
        [SerializeField] private GameObject safetySection;
        [SerializeField] private GameObject publicTrustSection;
        [SerializeField] private GameObject developmentButtonActiveBackground;
        [SerializeField] private GameObject safetyButtonActiveBackground;
        [SerializeField] private GameObject publicTrustButtonActiveBackground;
    
        public void ShowDevelopmentSection()
        {
            developmentSection.SetActive(true);
            safetySection.SetActive(false);
            publicTrustSection.SetActive(false);
            developmentButtonActiveBackground.SetActive(true);
            safetyButtonActiveBackground.SetActive(false);
            publicTrustButtonActiveBackground.SetActive(false);
        }
    
        public void ShowSafetySection()
        {
            developmentSection.SetActive(false);
            safetySection.SetActive(true);
            publicTrustSection.SetActive(false);
            developmentButtonActiveBackground.SetActive(false);
            safetyButtonActiveBackground.SetActive(true);
            publicTrustButtonActiveBackground.SetActive(false);
        }
    
        public void ShowPublicTrustSection()
        {
            developmentSection.SetActive(false);
            safetySection.SetActive(false);
            publicTrustSection.SetActive(true);
            developmentButtonActiveBackground.SetActive(false);
            safetyButtonActiveBackground.SetActive(false);
            publicTrustButtonActiveBackground.SetActive(true);
        }
    }
}
