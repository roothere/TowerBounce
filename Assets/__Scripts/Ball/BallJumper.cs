using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _lastTimeCollisionEnter;
    [SerializeField] private float _timeDelayBetweenCollisionEnter = 0.2f;

    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (Time.time < _lastTimeCollisionEnter) return;

        if (collision.gameObject.TryGetComponent(out DangerousSegment dangerousSegment)) {
            if (!DestroyBySuperPower(collision.gameObject)) Ball.S.DestroyBall();
            return;
        }

        if (collision.gameObject.TryGetComponent(out FinishPlatform finishPlatform)) {
            FinishPlatform.S.isFinished = true;
            Manager.S.DelayedRestart();
            return;
        }

        if (collision.gameObject.TryGetComponent(out PlatformSegment platformSegment)) {
            DestroyBySuperPower(collision.gameObject);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _lastTimeCollisionEnter = Time.time + _timeDelayBetweenCollisionEnter;
            return;
        }
    }

    private bool DestroyBySuperPower(GameObject go)
    {
        if (Ball._counter < 3)
        {
            Ball._counter = 0;
            return false;
        }

        go.GetComponentInParent<Platform>().Break();
        
        Ball._counter = 0;
        return true;
    }

    /*private bool CheckPlatform<T>(GameObject go)
    {
        if (go.TryGetComponent(out T platform) == false) return false;

        switch (platform)
        {
            case Finish type:
                Manager.S.DelayedRestart();
                break;
            case DangerousSegment type:
                Ball.S.DestroyBall();
                break;
            case PlatformSegment type:
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _lastTimeCollisionEnter = Time.time + _timeDelayBetweenCollisionEnter;
                break;
        }

        return true;
    }*/
}
