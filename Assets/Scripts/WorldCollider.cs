using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class WorldCollider : MonoBehaviour
{
    public bool enabledInPurpleWorld = false;

    private TilemapCollider2D tileCollider;

    private void Awake()
    {
        tileCollider = GetComponent<TilemapCollider2D>();
    }

    private void Start()
    {
        UpdateCollider();
    }

    private void OnEnable()
    {
        WorldStateManager.OnWorldChanged += UpdateCollider;
    }

    private void OnDisable()
    {
        WorldStateManager.OnWorldChanged -= UpdateCollider;
    }

    private void UpdateCollider()
    {
        bool isPurple = WorldStateManager.Instance.isPurpleWorld;

        // Enable collider depending on your setting
        tileCollider.enabled = (isPurple == enabledInPurpleWorld);
    }
}