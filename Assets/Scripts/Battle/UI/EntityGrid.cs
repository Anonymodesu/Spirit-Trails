using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UI.Entities;
using Global;

namespace Battle.UI
{

class EntityGrid : MonoBehaviour, IEnumerable<PhysicalEntity> {

    [SerializeField]
    private PhysicalEntity physicalEntityPrefab;
    [SerializeField]
    private EmptyEntity emptyEntityPrefab;

    [SerializeField]
    private int numEntities = 4;

    const int gridCellSize = 5;

    private PlayerStats playerData;
    private PositionalList<AbstractEntity> friendlyEntities;
    private PositionalList<AbstractEntity> hostileEntities;

    void Awake() {
        playerData = GameObject.FindWithTag("MultiSceneData").GetComponent<PlayerStats>();
    }

    public void RefreshEntities() {
        friendlyEntities = InitialiseEntities(playerData.friendlyEntities, true);
        hostileEntities = InitialiseEntities(playerData.hostileEntities, false);
    }

    private PositionalList<AbstractEntity> InitialiseEntities(PositionalList<Battle.Entities.Entity> entityData, bool isFriendly) {
        PositionalList<AbstractEntity> gridEntities = new PositionalList<AbstractEntity>((i) => {
            EmptyEntity entity = Instantiate(this.emptyEntityPrefab, this.transform, false);
            entity.transform.localPosition = getEntityPosition(i, isFriendly);
            return entity;
        }, numEntities);

        for(int i = 0; i < numEntities; i++) {
            if(entityData.IsNotable(i)) {
                PhysicalEntity entity = Instantiate(physicalEntityPrefab, this.transform, false);
                entity.PlayerControlled = isFriendly;
                entity.EntityData = entityData[i];
                entity.transform.localPosition = getEntityPosition(i, isFriendly);

                GameObject.Destroy(gridEntities[i]);
                gridEntities[i] = entity;
            }
        }
        return gridEntities;
    }

    public void AddEntityOnClick(Action<PhysicalEntity> entityFunc) {
        foreach(PhysicalEntity entity in this) {
            entity.AddOnClickCallback(() => entityFunc(entity));
        }
    }

    public IEnumerator<PhysicalEntity> GetEnumerator() {
        foreach(AbstractEntity entity in friendlyEntities) {
            yield return (PhysicalEntity) entity;
        }
        foreach(AbstractEntity entity in hostileEntities) {
            yield return (PhysicalEntity) entity;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    private Vector3 getEntityPosition(int i, bool isFriendly) {
        float entityHeight = isFriendly ? 0 : 5;
        return new Vector3(0, entityHeight, 0) + i * gridCellSize * Vector3.right;
    }

    public void SetEntity(bool isFriendly, int index, AbstractEntity entity) {
        PositionalList<AbstractEntity> entities = isFriendly ? friendlyEntities : hostileEntities;
        GameObject.Destroy(entities[index]);
        entities[index] = entity;
        entity.transform.localPosition = getEntityPosition(index, isFriendly);
    }

}

}