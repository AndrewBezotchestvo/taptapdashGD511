using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _cameraOffset;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = _playerTransform.position + _cameraOffset;
    }
}
