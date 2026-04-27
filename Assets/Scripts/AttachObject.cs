using UnityEngine;

public class AttachObject : MonoBehaviour
{
    public Transform player;
    public float followDistance = 1f;

    public void Attach(Transform player)
    {
        // Parent to player
        transform.SetParent(player);
        transform.localPosition = new Vector3(0, -followDistance, 0);
        transform.rotation = Quaternion.identity; // keep world rotation
    }
}