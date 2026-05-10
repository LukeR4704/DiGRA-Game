using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PurpleRain : MonoBehaviour
{
    [Header("Animation")]
    public Sprite[] frames;
    public float frameRate = 12f;

    private Image image;
    private AudioSource audioSource;

    private Coroutine animCoroutine;
    private bool isPlaying = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        // Start hidden
        image.enabled = false;
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
        if (WorldStateManager.Instance == null)
            return;

        bool isPurple = WorldStateManager.Instance.isPurpleWorld;

        if (isPurple)
        {
            PlayRain();
        }
        else
        {
            StopRain();
        }
    }

    private void PlayRain()
    {
        if (isPlaying)
            return;

        isPlaying = true;

        // Show rain image
        image.enabled = true;

        // Start animation
        animCoroutine = StartCoroutine(Animate());

        // Start sound
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void StopRain()
    {
        if (!isPlaying)
            return;

        isPlaying = false;

        // Hide rain image
        image.enabled = false;

        // Stop animation
        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine);
            animCoroutine = null;
        }

        // Stop sound
        audioSource.Stop();
    }

    private IEnumerator Animate()
    {
        int currentFrame = 0;
        float delay = 1f / frameRate;

        while (true)
        {
            image.sprite = frames[currentFrame];

            currentFrame = (currentFrame + 1) % frames.Length;

            yield return new WaitForSeconds(delay);
        }
    }
}