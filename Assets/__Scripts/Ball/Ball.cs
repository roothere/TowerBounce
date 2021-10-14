using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball S;

    public static int powerCounter = 0;

    [SerializeField] private Color finishColor = Color.red;
    [SerializeField] private Color startColor;
    [SerializeField] private float step = 0f;

    public ParticleSystem _particle;
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
        startColor = _renderer.material.color;

    }

    private void Update()
    {
        if (powerCounter > 0 && _rigidbody.velocity.y < 0)
        {
            float lerp = Mathf.Lerp(0, 1, step);
            if (step < 1) step += 1f * Time.deltaTime;
            _renderer.material.color = Color.Lerp(startColor, finishColor, lerp);
        }
        else
        {
            _renderer.material.color = startColor;
            step = 0;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformSegment platformSegment))
        {
            other.GetComponentInParent<Platform>().Break();
            powerCounter++;
            if (powerCounter == 3) 
                _particle.Play();
        }
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }
}
