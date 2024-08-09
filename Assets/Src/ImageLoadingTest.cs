using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class ImageLoadingTest : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private string url;
        private Texture2D _texture;

        private async void Start()
        {
            _texture = await UrlTexturesLoader.LoadTexture(url);
            if (_texture == null)
            {
                Debug.LogError("Failed to load texture");
            }
            else
            {
                Debug.Log("Texture loaded: " + _texture.width + "x" + _texture.height + "px");
                image.material.mainTexture = _texture;
            }
        }
        
        private void OnDestroy()
        {
            Destroy(_texture);
        }
    }
}