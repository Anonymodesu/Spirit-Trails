using System.Collections.Generic;
using UnityEngine;

namespace Overworld {

public class SpriteSorter : MonoBehaviour
{
    private HashSet<Renderer> nearbySprites;

    public HashSet<Renderer> NearbySprites {
        get { return nearbySprites; }
    }
    
    // Start is called before the first frame update
    void Start() {
        nearbySprites = new HashSet<Renderer>();
        transform.position = transform.position + new Vector3(0,0.00001f, 0);
    }

    // Update is called once per frame
    void Update() {
        //adjust sorting order so that sprites above current sprite appear behind it 
        SpriteRenderer thisSprite = this.GetComponent<SpriteRenderer>();
        if(nearbySprites.Count > 0) {
            int maxSortingOrder = int.MinValue;
            foreach(Renderer otherSprite in nearbySprites) {
                
                //find max sorting order of sprites positioned above the current sprite
                if(otherSprite.bounds.min.y > thisSprite.bounds.min.y 
                    && otherSprite.sortingOrder > maxSortingOrder) {
                    maxSortingOrder = otherSprite.sortingOrder;
                }
            }

            thisSprite.sortingOrder = maxSortingOrder + 1;

        } else {
            thisSprite.sortingOrder = 0; //reset to 0 if no nearby sprites
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Renderer sprite = other.GetComponent<Renderer>();
        if(sprite != null) {
            nearbySprites.Add(sprite);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Renderer sprite = other.GetComponent<Renderer>();
        if(sprite != null) {
            nearbySprites.Remove(sprite);
        }
    }

}

}