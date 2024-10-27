using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currnetVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currnetVelocity, smoothTime);
    }
}
