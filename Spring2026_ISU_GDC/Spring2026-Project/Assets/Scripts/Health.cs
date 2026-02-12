using UnityEngine;

public abstract class Health : MonoBehaviour {
    [SerializeField] private int hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int getHP() {
        return hp;
    }

    public void setHP(int newHP) {
        hp = newHP;
    }
}