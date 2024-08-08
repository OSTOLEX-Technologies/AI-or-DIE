using System;
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
        
        private UpgradeNode _upgradeNode;
        
        public void Init(UpgradeNode upgradeNode)
        {
            _upgradeNode = upgradeNode;
            _upgradeNode.Bought += OnBought;
            _upgradeNode.MadeAvailable += OnMadeAvailable;
            UpdateView();
        }
        
        private void OnMadeAvailable()
        {
            UpdateView();
        }

        private void OnBought(UpgradeNode upgradeNode)
        {
            UpdateView();
        }
        private void UpdateView()
        {
            if (_upgradeNode.IsBought)
            {
                image.color = boughtColor;
            }
            else if (_upgradeNode.IsAvailable)
            {
                image.color = availableColor;
            }
            else
            {
                image.color = lockedColor;
            }
        }
        
        public void Buy()
        {
            if (_upgradeNode.IsAvailable)
            {
                _upgradeNode.Buy();
            }
        }

        private void OnDestroy()
        {
            _upgradeNode.Bought -= OnBought;
            _upgradeNode.MadeAvailable -= OnMadeAvailable;
        }
    }
}