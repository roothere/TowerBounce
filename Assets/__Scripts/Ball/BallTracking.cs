using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class BallTracking : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _length;

    private Ball _ball;
    private Beam _beam;
    private Vector3 _cameraPosition;
    private Vector3 _minBallPosition;

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _beam = FindObjectOfType<Beam>();

        _cameraPosition = _ball.transform.position;
        _minBallPosition = _ball.transform.position;

        TrackBall();
    }

    private void Update()
    {
        if (_ball == null)
        {
            SceneManager.LoadScene("_Scene_0");
            return;
        }
        if (_ball.transform.position.y < _minBallPosition.y)
        {
            TrackBall();
            _minBallPosition = _ball.transform.position;
        }
    }

    private void TrackBall()
    {
        Vector3 beamPosition = _beam.transform.position;
        beamPosition.y = _ball.transform.position.y;
        _cameraPosition = _ball.transform.position;
        Vector3 direction = (beamPosition - _ball.transform.position).normalized + _directionOffset;
        _cameraPosition -= direction * _length;
        transform.position = _cameraPosition;
        transform.LookAt(_ball.transform);
    }
}
