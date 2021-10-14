using System;
using UnityEngine;

public class FinishPlatform : Platform
{
    public static FinishPlatform S;

    private Renderer[] _renderer;

    [SerializeField] private float _duration = 1.0f;
    [NonSerialized] public bool isFinished = false;
    [SerializeField] private Color colorStart;
    [SerializeField] private Color colorFinish;

    void Awake()
    {
        if (S == null) S = this;
        _renderer = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (isFinished) {
            float lerp = Mathf.PingPong(Time.time, _duration) / _duration;
            foreach (var rend in _renderer)
            {
                rend.material.color = Color.Lerp(colorStart, colorFinish, lerp);
            }
        }
    }
}
