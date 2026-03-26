using System.Linq;
using UnityEngine;

public class BasicComboAttack : MonoBehaviour, IAttack
{
    /// <summary>
    /// The window of time in seconds that the player has to use the second basic attack. If they use the first basic attack button and then
    /// wait longer than the comboWindow time, the next time they use the attack button it will be once again basic attack 1.
    /// </summary>
    [SerializeField] float comboWindow = 1.5f;

    [SerializeField] AnimationClip[] basicAttacks;
    private int currentComboAttack = 0;
    private Animator playerAnimator;
    private float comboTimer = 0;

    public AttackMechanic GetAttackImplementation()
    {
        return UseComboAttacks;
    }

    private void Start()
    {
        //TODO: get rid of this hack
        playerAnimator = transform.parent.parent.GetComponent<Animator>();
    }

    void Update()
    {

        if (currentComboAttack > 0 && comboTimer < Time.time)
        {
            currentComboAttack = 0;
        }
    }

    //  Used for when the player attacks while equipped with the spear.
    private void UseComboAttacks()
    {
        playerAnimator.Play(basicAttacks[currentComboAttack].name);
        if (currentComboAttack < basicAttacks.Count() - 1)
        {
            currentComboAttack += 1;
            comboTimer = Time.time + comboWindow;
        }
        else
        {
            // Reset the attacks back to the start of the combo.
            currentComboAttack = 0;
        }
    }
}
