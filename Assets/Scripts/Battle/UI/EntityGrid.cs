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
    private EntityContainer physicalEntityPrefab;
    [SerializeField]
    private EntityContainer emptyEntityPrefab;

    [SerializeField]
    private int numEntities = 4;

    const int gridCellSize = 5;

    private PlayerStats playerData;
    private PositionalList<EntityContainer> friendlyEntities;
    private PositionalList<EntityContainer> hostileEntities;


    void Awake() {
        playerData = GameObject.FindWithTag("MultiSceneData").GetComponent<PlayerStats>();
    }

    public void RefreshEntities() {
        friendlyEntities = InitialiseEntities(playerData.friendlyEntities, true);
        hostileEntities = InitialiseEntities(playerData.hostileEntities, false);
    }

    private PositionalList<EntityContainer> InitialiseEntities(PositionalList<Battle.Entities.Entity> entityData, bool isFriendly) {
        PositionalList<EntityContainer> gridEntities = new PositionalList<EntityContainer>((i) => {
            return EntityContainer.Instantiate(
                this.gameObject, 
                this.emptyEntityPrefab, 
                new EmptyEntity(), 
                getEntityPosition(i, isFriendly));
        }, numEntities);

        for(int i = 0; i < numEntities; i++) {

            // Replace the Empty Entities created above with Physical Entities
            if(entityData.IsNotable(i)) {
                GameObject.Destroy(gridEntities[i]);

                PhysicalEntity entity = new PhysicalEntity(isFriendly, entityData[i]);
                    
                gridEntities[i] = EntityContainer.Instantiate(
                    this.gameObject, 
                    physicalEntityPrefab, 
                    entity, 
                    getEntityPosition(i, isFriendly)
                );
            }
        }
        return gridEntities;
    }

    public void AddPhysicalEntityOnClick(Action<PhysicalEntity> entityFunc) {
        foreach(var container in PhysicalEntityEnumerator()) {
            var entity = (PhysicalEntity) container.Entity;
            container.AddOnClickCallback(() => entityFunc(entity));
        }
    }

    public void AddContainerOnClick(Action<EntityContainer> entityFunc) {
        foreach(var container in AbstractEntityEnumerator()) {
            container.AddOnClickCallback(() => entityFunc(container));
        }
    }


    public IEnumerator<PhysicalEntity> GetEnumerator() {
        foreach(EntityContainer container in PhysicalEntityEnumerator()) {
            yield return (PhysicalEntity) container.Entity;
        }
    }

    public IEnumerable<EntityContainer> PhysicalEntityEnumerator() {
        foreach(EntityContainer container in friendlyEntities) {
            yield return container;
        }
        foreach(EntityContainer container in hostileEntities) {
            yield return container;
        }
    }

    public IEnumerable<EntityContainer> AbstractEntityEnumerator() {
        foreach(EntityContainer container in friendlyEntities.GetAll()) {
            yield return container;
        }
        foreach(EntityContainer container in hostileEntities.GetAll()) {
            yield return container;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    private Vector3 getEntityPosition(int i, bool isFriendly) {
        float entityHeight = isFriendly ? 0 : 5;
        return new Vector3(0, entityHeight, 0) + i * gridCellSize * Vector3.right;
    }

    public IEnumerable<PhysicalEntity> GetEntities(bool isFriendly, int startPos, int endPos) {
        var entities = isFriendly ? friendlyEntities : hostileEntities;
        foreach(EntityContainer container in entities) {
            if(startPos < container.Position && container.Position < endPos) {
                yield return (PhysicalEntity) container.Entity;
            }
        }
    }

}

}