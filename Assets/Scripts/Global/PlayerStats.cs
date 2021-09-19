using UnityEngine;
using Battle.Entities;
using Battle.Entities.Stats;

namespace Global {

public class PlayerStats : MonoBehaviour {

    public Entity entity;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MultiSceneData");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
        }

        entity = new Entity("Anonymo", new EntityStats(
            100, 80, 80, 100, 80, 10, 15, 15, 10
        ));
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
}

}