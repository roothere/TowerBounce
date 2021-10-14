using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager S;

    [SerializeField] private float delayBeforeRestart = 1f;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    public void DelayedRestart()
    {
        Invoke("Restart", delayBeforeRestart);
    }

    private void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
