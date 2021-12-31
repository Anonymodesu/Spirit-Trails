using UnityEngine;
using Battle.Entities;
using Battle.Entities.Stats;
using Battle.Skills;
using System.Collections.Generic;

namespace Global {

public class PlayerStats : MonoBehaviour {

    public List<Entity> entities;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MultiSceneData");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
        }

        entities = new List<Entity> {
            new Entity("Anonymo", 
                new EntityStats(
                    100, 80, 80, 100, 80, 10, 15, 15, 10
                ), 
                new List<Skill> {
                    new MagicBolt(),
                    new PhysicalStrike(),
                    new TripleStrike()
                }),
            new Entity("Andaru", 
                new EntityStats(
                    100, 80, 80, 100, 80, 10, 15, 15, 10
                ), 
                new List<Skill> {
                }),
            new Entity("Sean", 
                new EntityStats(
                    100, 80, 80, 100, 80, 10, 15, 15, 10
                ), 
                new List<Skill> {
                }),
            new Entity("Wumah", 
                new EntityStats(
                    100, 80, 80, 100, 80, 10, 15, 15, 10
                ), 
                new List<Skill> {
                    new MagicBolt()
                })
        };

    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
}

}