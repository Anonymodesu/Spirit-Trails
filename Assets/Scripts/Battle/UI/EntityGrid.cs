using System;
using System.Collections.Generic;
using UnityEngine;
using Global;

namespace Battle.UI
{

class EntityGrid : MonoBehaviour {
    [SerializeField]
    private Entity entityPrefab;

    [SerializeField]
    private int numEntities = 4;

    const int gridCellSize = 5;

    private PlayerStats playerData;
    private List<Entity> entities;

    public void SetEntities(Action<Entity> entityFunc) {
        if(playerData == null) {
            playerData = GameObject.FindWithTag("MultiSceneData").GetComponent<PlayerStats>();
        }

        if(entities == null) {
            entities = new List<Entity>();
            for(int i = 0; i < numEntities; i++) {
                Entity entity = Instantiate(entityPrefab, this.transform, false);
                entity.transform.localPosition = new Vector3(0, 0, 0) + i * gridCellSize * Vector3.right;
                entity.EntityData = playerData.entities[i];
                entity.PlayerControlled = true;
                entity.OnClick.AddListener(() => entityFunc(entity));
                entities.Add(entity);
            }
        }

    }

}

}