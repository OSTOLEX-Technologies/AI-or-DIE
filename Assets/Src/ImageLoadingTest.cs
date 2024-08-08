using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class ImageLoadingTest : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Texture2D _texture;

        private async void Start()
        {
            var url = "https://onlinejpgtools.com/images/examples-onlinejpgtools/random-grid.jpg";
            _texture = await URLTexturesLoader.LoadTexture(url);
            Debug.Log("Texture loaded: " + _texture.width + "x" + _texture.height + "px");
            image.material.mainTexture = _texture;
        }
        
        private void OnDestroy()
        {
            Destroy(_texture);
        }
    }
}