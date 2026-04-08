using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashMarkerSpawner))]
[RequireComponent(typeof(SlashHitbox))]
[RequireComponent(typeof(LokiAnimator))]

public class SlashAttack : MonoBehaviour
{
    [Header("Attack counts & timing")]
    [SerializeField] private int flashCount = 4;
    [SerializeField] private float chargeDuration = 1.2f;
    [SerializeField] private float timeBetweenFlashes = 0.35f;
    [SerializeField] private float preSlashDelay = 0.15f;
    [SerializeField] private float slashDuration = 0.3f;
    [SerializeField] private float punishWindowDuration = 0.6f;

    [Header("Arena bounds for flash positions")]
    [SerializeField] private Vector2 arenaMin = new(-8f, -4f);
    [SerializeField] private Vector2 arenaMax = new(8f, 4f);
    [SerializeField] private float minFlashSpacing = 2.5f;

    private FlashMarkerSpawner _spawner;
    private SlashHitbox _hitbox;
    private LokiAnimator _lokiAnimator;
    private bool _running;

    public bool IsRunning => _running;

    private void Awake()
    {
        _spawner = GetComponent<FlashMarkerSpawner>();
        _hitbox = GetComponent<SlashHitbox>();
        _lokiAnimator = GetComponent<LokiAnimator>();
    }

    /// <summary>Kick off the full slash sequence. Safe to call from any state machine.</summary>
    public void TriggerSlashAttack()
    {
        if (_running) return;
        StartCoroutine(SlashSequence());
    }

    private IEnumerator SlashSequence()
    {
        _running = true;

        // 1. Charge
        _lokiAnimator.PlayCharge();
        yield return new WaitForSeconds(chargeDuration);

        // 2. Spawn flash markers, record positions and directions
        var positions = new List<Vector3>();
        var directions = new List<bool>(); // true = slash right

        for (int i = 0; i < flashCount; i++)
        {
            Vector3 pos = GetValidPosition(positions);
            bool goRight = Random.value > 0.5f;

            positions.Add(pos);
            directions.Add(goRight);

            _spawner.SpawnFlash(pos, goRight, i + 1);
            _lokiAnimator.PlayFlashSpawnSFX();

            yield return new WaitForSeconds(timeBetweenFlashes);
        }

        yield return new WaitForSeconds(0.15f); // brief read window after last flash

        // 3. Teleport-slash at each position in order
        for (int i = 0; i < positions.Count; i++)
        {
            transform.position = positions[i];
            _lokiAnimator.PlayTeleport(directions[i]);
            _spawner.DismissFlash(i);

            yield return new WaitForSeconds(preSlashDelay);

            _lokiAnimator.PlaySlash(directions[i]);
            _hitbox.Activate();
            yield return new WaitForSeconds(slashDuration);
            _hitbox.Deactivate();

            // Punish window — Loki holds still
            _lokiAnimator.PlayPunishIdle();
            yield return new WaitForSeconds(punishWindowDuration);
        }

        // 4. Clean up and return to idle
        _spawner.CleanUpAll();
        _lokiAnimator.PlayIdle();
        _running = false;
    }

    private Vector3 GetValidPosition(List<Vector3> existing, int maxTries = 30)
    {
        for (int t = 0; t < maxTries; t++)
        {
            var candidate = new Vector3(
                Random.Range(arenaMin.x, arenaMax.x),
                Random.Range(arenaMin.y, arenaMax.y),
                0f);

            bool tooClose = false;
            foreach (var e in existing)
                if (Vector3.Distance(candidate, e) < minFlashSpacing) { tooClose = true; break; }

            if (!tooClose) return candidate;
        }

        return new Vector3(Random.Range(arenaMin.x, arenaMax.x), Random.Range(arenaMin.y, arenaMax.y), 0f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
        var center = new Vector3((arenaMin.x + arenaMax.x) / 2f, (arenaMin.y + arenaMax.y) / 2f);
        var size = new Vector3(arenaMax.x - arenaMin.x, arenaMax.y - arenaMin.y, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }
#endif
}
