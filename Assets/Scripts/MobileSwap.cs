using UnityEngine;

public class MobileSwap : MonoBehaviour
{
    public void SwapWorld()
    {
        WorldStateManager.Instance.ToggleWorld();
    }
}