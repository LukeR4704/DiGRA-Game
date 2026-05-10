using UnityEngine;
using UnityEngine.Tilemaps;

public class ColourChanger : MonoBehaviour
{
    private Tilemap tile;

    public Color greenColor = Color.green;
    public Color purpleColor = Color.magenta;

    private void Start()
    {
        tile = GetComponent<Tilemap>();
        UpdateColor();
    }

    private void Update()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (WorldStateManager.Instance.isPurpleWorld)
            tile.color = purpleColor;
        else
            tile.color = greenColor;
    }

    private void OnEnable()
    {
        WorldStateManager.OnWorldChanged += UpdateColor;
    }

    private void OnDisable()
    {
        WorldStateManager.OnWorldChanged -= UpdateColor;
    }
}