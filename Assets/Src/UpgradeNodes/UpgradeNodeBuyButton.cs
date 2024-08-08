using UnityEngine;

namespace Src.UpgradeNodes
{
    public class UpgradeNodeBuyButton : MonoBehaviour
    {
        private UpgradeNode _upgradeNode;
        
        public void SetUpgradeNode(UpgradeNode upgradeNode)
        {
            _upgradeNode = upgradeNode;
        }
        
        public void OnClick()
        {
            _upgradeNode?.Buy();
        }
    }
}