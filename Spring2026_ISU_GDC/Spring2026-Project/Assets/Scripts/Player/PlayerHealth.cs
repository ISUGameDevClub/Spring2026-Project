using UnityEngine;

public class PlayerHealth : Health {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        setHP(100);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            Damage();
        }

        if (getHP() <= 0) {
            Debug.Log("You dead. Thanks for playing.");
        }
    }

    public void Damage() {
        int x = getHP();
        Debug.Log("Ow! What was that for?");
        setHP(x - 5);
        Debug.Log("Player Health is now " + getHP());
    }
}
