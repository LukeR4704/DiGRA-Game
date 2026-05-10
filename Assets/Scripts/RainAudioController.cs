using UnityEngine;

public class RainAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateRainState();
    }

    private void OnEnable()
    {
        WorldStateManager.OnWorldChanged += UpdateRainState;
    }

    private void OnDisable()
    {
        WorldStateManager.OnWorldChanged -= UpdateRainState;
    }

    private void UpdateRainState()
    {
        if (WorldStateManager.Instance.isPurpleWorld)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}