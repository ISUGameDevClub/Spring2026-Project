using System.Collections;
using ISUGameDev.SpearGame.Player.PlayerState;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player
{
    public class PlayerDashVFX : MonoBehaviour
    {
        [Header("Afterimage Settings")]
        [SerializeField] private float timeBetweenAfterimages = 0.05f;
        [SerializeField] private float afterimageDuration = 0.3f;
        [SerializeField] private Color afterimageColor = new Color(0.5f, 0.8f, 1f, 0.7f);
        [SerializeField] private Sprite afterimageSprite; 

        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem dashParticles;

        private SpriteRenderer _playerSpriteRenderer;
        private PlayerEventManager _eventManager;
        private bool _isDashing;
        private Coroutine _afterimageCoroutine;

        private void Start()
        {
            _playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _eventManager = GetComponent<PlayerEventManager>();
            _eventManager.OnPlayerStateChanged.AddListener(OnStateChanged);
        }

        private void OnDestroy()
        {
            _eventManager.OnPlayerStateChanged.RemoveListener(OnStateChanged);
        }

        private void OnStateChanged(BasePlayerState newState)
        {
            if (newState.playerStateType == PlayerStateType.DashingTowardsSpear)
                StartDashVFX();
            else if (_isDashing)
                StopDashVFX();
        }

        private void StartDashVFX()
        {
            _isDashing = true;

            if (dashParticles != null)
                dashParticles.Play();

            if (_afterimageCoroutine != null)
                StopCoroutine(_afterimageCoroutine);

            _afterimageCoroutine = StartCoroutine(SpawnAfterimages());
        }

        private void StopDashVFX()
        {
            _isDashing = false;

            if (dashParticles != null)
                dashParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            if (_afterimageCoroutine != null)
            {
                StopCoroutine(_afterimageCoroutine);
                _afterimageCoroutine = null;
            }
        }

        private IEnumerator SpawnAfterimages()
        {
            while (_isDashing)
            {
                SpawnSingleAfterimage();
                yield return new WaitForSeconds(timeBetweenAfterimages);
            }
        }

        private void SpawnSingleAfterimage()
        {
            if (_playerSpriteRenderer == null || _playerSpriteRenderer.sprite == null) return;

            GameObject ghost = new GameObject("DashAfterimage");
            ghost.transform.position = _playerSpriteRenderer.transform.position;
            ghost.transform.rotation = _playerSpriteRenderer.transform.rotation;
            ghost.transform.localScale = _playerSpriteRenderer.transform.lossyScale;

            SpriteRenderer ghostRenderer = ghost.AddComponent<SpriteRenderer>();
            ghostRenderer.sprite = afterimageSprite != null ? afterimageSprite : _playerSpriteRenderer.sprite;           
            ghostRenderer.sortingLayerName = _playerSpriteRenderer.sortingLayerName;
            ghostRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder - 1;
            ghostRenderer.color = afterimageColor;

            StartCoroutine(FadeAfterimage(ghostRenderer));
        }

        private IEnumerator FadeAfterimage(SpriteRenderer ghostRenderer)
        {
            // Pixel art style: discrete alpha steps instead of smooth lerp
            float[] alphaSteps = { 0.6f, 0.35f, 0.15f, 0f };
            float timePerStep = afterimageDuration / alphaSteps.Length;

            Color c = ghostRenderer.color;
            foreach (float alpha in alphaSteps)
            {
                yield return new WaitForSeconds(timePerStep);
                if (ghostRenderer == null) yield break;
                c.a = alpha;
                ghostRenderer.color = c;
            }

            Destroy(ghostRenderer.gameObject);
        }
    }
}