using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class ShowSpritesBoundary : MonoBehaviour
{
    [SerializeField] private Color boundaryColor = Color.green;
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr)
            {
                Bounds bounds = sr.bounds;

                Gizmos.color = boundaryColor;
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
#endif
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
        {
            sr.enabled = false;
        }
#endif
    }
}
