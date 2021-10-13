using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _lastTimeCollisionEnter;
    [SerializeField] private float _timeDelayBetweenCollisionEnter = 0.1f;
    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (Time.time < _lastTimeCollisionEnter) return;

        if (collision.gameObject.TryGetComponent(out Finish finishPlatform)) {
            print("WIN!");
            return;
        }

        if (collision.gameObject.TryGetComponent(out DangerousSegment dangerousSegment)) {
            print("Danger!");
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.TryGetComponent(out PlatformSegment platformSegment)) {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _lastTimeCollisionEnter = Time.time + _timeDelayBetweenCollisionEnter;
        }
    }
}
