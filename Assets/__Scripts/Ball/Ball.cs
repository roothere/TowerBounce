using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    public static Ball S;

    public static int powerCounter = 0;

    [SerializeField] private Color _finishColor = Color.red;
    [SerializeField] private Color _startColor;
    [SerializeField] private float step;
    [SerializeField] private float increaserCoef = 0.15f;
    [SerializeField] private float colorChangerCoef = 1.0f;

    public static ParticleSystem _particle;

    private float _maxBallSize = 0.5f;
    private Vector3 _startScale;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Ball.Awake() - second Ball instantiation!");
        }

        _particle = GetComponentInChildren<ParticleSystem>();
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _startColor = _renderer.material.color;
        _startScale = transform.localScale;

    }

    private void Update()
    {
        PaintBallAfterTrigger();
        OversizeBallAfterTrigger();
    }

    private void PaintBallAfterTrigger()
    {
        if (powerCounter > 1 && _rigidbody.velocity.y < 0) {
            float lerp = Mathf.Lerp(0, 1, step);
            if (step < 1) step += colorChangerCoef * Time.deltaTime;
            _renderer.material.color = Color.Lerp(_startColor, _finishColor, lerp);
        } else {
            _renderer.material.color = _startColor;
            step = 0;
        }
    }

    private void OversizeBallAfterTrigger()
    {
        if (powerCounter > 0 && _rigidbody.velocity.y < 0) {
            Vector3 scaleIncreaser = increaserCoef * Time.deltaTime * Vector3.one;
            if (transform.localScale.y < _maxBallSize) transform.localScale += scaleIncreaser;
        } else {
            transform.localScale = _startScale;
            step = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformSegment platformSegment))
        {
            other.GetComponentInParent<Platform>().Break();
            powerCounter++;
            EnableFallingBallParticles();
        }
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }

    public static void EnableFallingBallParticles()
    {
        if (powerCounter == 3)
            _particle.Play();
    }

    public static void DisableFallingBallParticles() {
        _particle.Stop();
    }
}
