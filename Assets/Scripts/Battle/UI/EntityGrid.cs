using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

namespace Battle.UI
{

class EntityGrid : MonoBehaviour, IEnumerable<Entity> {
    [SerializeField]
    private Entity entityPrefab;

    [SerializeField]
    private int numEntities = 4;

    const int gridCellSize = 5;

    private PlayerStats playerData;
    private List<Entity> entities;

    public void RefreshEntities() {
        if(playerData == null) {
            playerData = GameObject.FindWithTag("MultiSceneData").GetComponent<PlayerStats>();
        }

        if(entities == null) {
            entities = new List<Entity>();
            for(int i = 0; i < numEntities; i++) {
                Entity entity = Instantiate(entityPrefab, this.transform, false);
                entity.transform.localPosition = new Vector3(0, 0, 0) + i * gridCellSize * Vector3.right;
                entities.Add(entity);
            }
        }

        for(int i = 0; i < entities.Count; i++) {
            Entity entity = entities[i]; 
            entity.EntityData = playerData.entities[i];
            entity.PlayerControlled = true;
        }
    }

    public void AddEntityOnClick(Action<Entity> entityFunc) {
        foreach(Entity entity in entities) {
            entity.OnClick.AddListener(() => entityFunc(entity));
        }
    }

    public IEnumerator<Entity> GetEnumerator() {
        return entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}

}