using System;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    public static WorldStateManager Instance;

    public bool isPurpleWorld = false;

    public static event Action OnWorldChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleWorld()
    {
        isPurpleWorld = !isPurpleWorld;
        OnWorldChanged?.Invoke();
    }
}