using System.Runtime.InteropServices.WindowsRuntime;
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

        if (CheckPlatformType<FinishPlatform>(collision.gameObject)) return;
        if (CheckPlatformType<DangerousSegment>(collision.gameObject)) return;
        if (CheckPlatformType<PlatformSegment>(collision.gameObject)) return;

    }

    private bool DestroyBySuperPower(GameObject go)
    {
        if (Ball.powerCounter < 3)
        {
            Ball.powerCounter = 0;
            return false;
        }

        Ball.DisableFallingBallParticles();
        go.GetComponentInParent<Platform>().Break();
        Ball.powerCounter = 0;

        return true;
    }

    private bool CheckPlatformType<T>(GameObject go)
    {
        if (go.TryGetComponent(out T platform) == false) return false;

        switch (platform)
        {
            case FinishPlatform type:
                Ball.DisableFallingBallParticles();
                Ball.powerCounter = 0;
                FinishPlatform.S.isFinished = true;
                Manager.S.DelayedRestart();
                break;
            case DangerousSegment type:
                if (!DestroyBySuperPower(go)) Ball.S.DestroyBall();
                break;
            case PlatformSegment type:
                DestroyBySuperPower(go);
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _lastTimeCollisionEnter = Time.time + _timeDelayBetweenCollisionEnter;
                break;
        }

        return true;
    }
}
