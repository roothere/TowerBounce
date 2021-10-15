using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    public float turn; 

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateTower();
    }

    private void RotateTower()
    {
        turn = -Input.GetAxisRaw("Horizontal");
        _rigidbody.angularVelocity = (turn * Vector3.down * Time.deltaTime * _rotateSpeed * 2);
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                float torque = touch.deltaPosition.x * Time.deltaTime * _rotateSpeed;
                _rigidbody.angularVelocity = (Vector3.down * torque);
            }
        }
    }
}
