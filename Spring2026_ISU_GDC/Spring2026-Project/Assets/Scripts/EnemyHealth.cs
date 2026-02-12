using UnityEngine;

public class EnemyHealth : Health {
    void Start() {
        setHP(20);
    }

    void Update() {
        if (getHP() <= 0) {
            Debug.Log("oof");
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Damage();
        }
    }
    
    public void Damage() {
        setHP(getHP() - 5);
        Debug.Log("Enemy has been hurt. Its health is now " + getHP());
    }
}