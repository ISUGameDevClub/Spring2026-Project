using System.Collections.Generic;
using UnityEngine;

public class FlashSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [Tooltip("Flash marker prefab. Should implement IFlashMarker if you want direction arrows.")]
    [SerializeField] private GameObject flashMarkerPrefab;

    [Header("Spawn VFX (optional)")]
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private ParticleSystem spawnVFX;

    private AudioSource _audio;
    private readonly List<GameObject> _activeMarkers = new();

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Instantiate a flash marker at <paramref name="worldPos"/>.
    /// </summary>
    /// <param name="worldPos">Where in the arena to place it.</param>
    /// <param name="facingRight">The direction Loki will slash when he arrives.</param>
    /// <param name="order">1-based index shown on the marker so the player can read the sequence.</param>
    public void SpawnFlash(Vector3 worldPos, bool facingRight, int order)
    {
        if (flashMarkerPrefab == null)
        {
            Debug.LogWarning("[FlashMarkerSpawner] No flash marker prefab assigned.");
            _activeMarkers.Add(null); // keep index alignment with the attack list
            return;
        }

        GameObject marker = Instantiate(flashMarkerPrefab, worldPos, Quaternion.identity);

        if (marker.TryGetComponent<IFlashMarker>(out var fm))
            fm.Initialise(facingRight, order);

        if (spawnVFX != null)
            Instantiate(spawnVFX, worldPos, Quaternion.identity).Play();

        if (_audio != null && spawnClip != null)
            _audio.PlayOneShot(spawnClip);

        _activeMarkers.Add(marker);
    }

    /// <summary>Destroy the marker at the given index (call when Loki arrives at that flash).</summary>
    public void DismissFlash(int index)
    {
        if (index < 0 || index >= _activeMarkers.Count) return;
        if (_activeMarkers[index] != null)
            Destroy(_activeMarkers[index]);
        _activeMarkers[index] = null;
    }

    /// <summary>Destroy all remaining markers (end of sequence or interrupted attack).</summary>
    public void CleanUpAll()
    {
        foreach (var marker in _activeMarkers)
            if (marker != null) Destroy(marker);
        _activeMarkers.Clear();
    }

    private void OnDestroy() => CleanUpAll();
}

public interface IFlashMarker
{
    /// <param name="facingRight">True = Loki will slash right; false = slash left.</param>
    /// <param name="order">Sequential number so the player can read the attack order.</param>
    void Initialise(bool facingRight, int order);
}
