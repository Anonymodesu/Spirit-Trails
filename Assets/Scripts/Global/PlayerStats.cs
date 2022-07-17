using UnityEngine;
using Battle.Entities;
using Battle.Entities.Stats;
using Battle.Skills;
using System.Collections.Generic;

namespace Global {

public class PlayerStats : MonoBehaviour {

    public PositionalList<Entity> friendlyEntities;
    public PositionalList<Entity> hostileEntities;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MultiSceneData");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
        }

        friendlyEntities = new PositionalList<Entity>((_) => null, 4);
        friendlyEntities[2] = new Entity("Anonymo", 
        new EntityStats(
                100, 80, 80, 100, 80, 15, 15, 15, 10, 2
            ), 
            new List<Skill> {
                new MagicBolt(),
                new PhysicalStrike(),
                new TripleStrike()
            }
        );
        friendlyEntities[1] = new Entity("Womah", 
            new EntityStats(
                100, 80, 80, 100, 80, 15, 15, 15, 10, 1
            ), 
            new List<Skill> {
                new MagicBolt(),
                new TripleStrike(),
                new ArcaneAssault()
            }
        );

        hostileEntities = new PositionalList<Entity>((_) => null, 4);
        hostileEntities[0] = new Entity("Andaru", 
            new EntityStats(
                100, 80, 80, 100, 80, 15, 15, 15, 10, 3
            ), 
            new List<Skill> {
                new MagicBolt(),
                new PhysicalStrike()
            }
        );
        hostileEntities[3] = new Entity("Sean", 
            new EntityStats(
                100, 80, 80, 100, 80, 15, 15, 15, 10, 4
            ), 
            new List<Skill> {
                new MagicBolt(),
                new PhysicalStrike()
            }
        );
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
}

}