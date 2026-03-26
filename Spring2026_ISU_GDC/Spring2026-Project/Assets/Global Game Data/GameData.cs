using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    // ─────────────────────────────────────────
    //  PLAYER
    // ─────────────────────────────────────────

    [Space(10)]
    [Header("══════════  PLAYER  ══════════")]

    [Space(5)]
    [Header("// Health")]
    public float playerMaxHealth = 100f;

    [Space(5)]
    [Header("// Attack Damage")]
    public float basicJabDamage   = 10f;
    public float spearThrowDamage = 25f;
    public float spearDashDamage  = 20f;


    // ─────────────────────────────────────────
    //  ENEMIES — STANDARD
    // ─────────────────────────────────────────

    [Space(15)]
    [Header("══════════  ENEMIES  ══════════")]

    [Space(5)]
    [Header("// Melee Enemy")]
    public float meleeEnemyMaxHealth = 60f;
    public float meleeEnemyDamage    = 12f;

    [Space(5)]
    [Header("// Ranged Enemy")]
    public float rangedEnemyMaxHealth = 40f;
    public float rangedEnemyDamage    = 18f;


    // ─────────────────────────────────────────
    //  BOSSES
    // ─────────────────────────────────────────

    [Space(15)]
    [Header("══════════  BOSSES  ══════════")]

    [Space(5)]
    [Header("// Lindworm")]
    public float lindwormMaxHealth = 300f;
    public float lindwormDamage    = 30f;

    [Space(5)]
    [Header("// Loki")]
    public float lokiMaxHealth = 400f;
    public float lokiDamage    = 35f;

    [Space(5)]
    [Header("// Giant King")]
    public float giantKingMaxHealth = 600f;
    public float giantKingDamage    = 50f;
}