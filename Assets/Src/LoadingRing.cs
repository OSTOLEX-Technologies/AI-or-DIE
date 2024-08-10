using UnityEngine;

namespace Src
{
    public class LoadingRing : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
    
        private void Update()
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
