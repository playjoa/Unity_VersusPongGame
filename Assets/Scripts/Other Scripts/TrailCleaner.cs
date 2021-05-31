using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailCleaner : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        GetTrailRendererReferenceIfNotSet();
    }

    private void OnDisable()
    {
        ClearTrail();
    }

    void GetTrailRendererReferenceIfNotSet()
    {
        if (!trailRenderer)
            trailRenderer = GetComponent<TrailRenderer>();
    }

    void ClearTrail()
    {
        if (trailRenderer == null)
            return;

        trailRenderer.Clear();
    }
}