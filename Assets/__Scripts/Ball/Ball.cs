using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball S;

    public static int _counter = 0;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformSegment platformSegment))
        {
            print(_counter + "Triggered");
            other.GetComponentInParent<Platform>().Break();
            _counter++;
        }
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }
}
