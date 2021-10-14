using UnityEngine;


public class Platform : MonoBehaviour
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    public void Break()
    {
        PlatformSegment[] platformSegments = GetComponentsInChildren<PlatformSegment>();
        foreach (var segment in platformSegments)
        {
            Collider coll = segment.GetComponent<Collider>();
            if (coll.isTrigger) {
                Destroy(segment.gameObject);
            }
            else {
                if (segment.gameObject.TryGetComponent(out DangerousSegment component)) Destroy(component);
                segment.Bounce(_bounceForce, transform.position, _bounceRadius);
            }
        }
        Invoke("DeletePlatform", 2f);
    }

    private void DeletePlatform()
    {
        Destroy(this.gameObject);
    }
}
