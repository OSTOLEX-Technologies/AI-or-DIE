using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Src.UpgradeNodes
{
    public class UpgradeNodeView : MonoBehaviour
    {
        [SerializeField] private Color lockedColor;
        [SerializeField] private Color availableColor;
        [SerializeField] private Color boughtColor;
        [SerializeField] private Image image;
        [SerializeField] private Image icon;

        private UpgradeNode _upgradeNode;
        private UpgradeNodeInfoPanel _infoPanel;
        private UpgradeNodeBuyButton _buyButton;
        private Material _iconMaterial;
        
        public List<UpgradeNodeView> Children { get; } = new List<UpgradeNodeView>();
        
        public void Init(UpgradeNode upgradeNode, UpgradeNodeInfoPanel infoPanel, UpgradeNodeBuyButton buyButton)
        {
            _infoPanel = infoPanel;
            _buyButton = buyButton;
            _upgradeNode = upgradeNode;
            _upgradeNode.Bought += OnBought;
            _upgradeNode.MadeAvailable += OnMadeAvailable;
            _buyButton.SetUpgradeNode(_upgradeNode);
            _iconMaterial = Instantiate(icon.material);
            _iconMaterial.mainTexture = upgradeNode.Data.Icon;
            icon.material = _iconMaterial;
            UpdateView();
        }
        
        private void OnMadeAvailable()
        {
            UpdateView();
        }

        private void OnBought(UpgradeNode upgradeNode)
        {
            UpdateInfoPanel();
            UpdateView();
        }
        private void UpdateView()
        {
            if (_upgradeNode.IsBought)
            {
                image.color = boughtColor;
                icon.color = boughtColor;
            }
            else if (_upgradeNode.IsAvailable)
            {
                image.color = availableColor;
                icon.color = availableColor;
            }
            else
            {
                image.color = lockedColor;
                icon.color = lockedColor;
            }
        }
        
        public void OnClick()
        {
            UpdateInfoPanel();
        }

        private void UpdateInfoPanel()
        {
            _buyButton.gameObject.SetActive(true);
            _infoPanel.gameObject.SetActive(true);
            _infoPanel.SetDataForNode(_upgradeNode);
            _buyButton.SetUpgradeNode(_upgradeNode);
            if (_upgradeNode.IsBought || !_upgradeNode.IsAvailable)
            {
                _buyButton.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _upgradeNode.Bought -= OnBought;
            _upgradeNode.MadeAvailable -= OnMadeAvailable;
            Destroy(_iconMaterial);
        }
    }
}