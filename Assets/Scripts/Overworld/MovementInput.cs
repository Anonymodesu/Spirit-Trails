using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour {
    //numbers correspond to animation transition triggers
    public enum WalkingDirection { Up=4, Down=1, Left=3, Right=2, None=0 }

    private List<WalkingDirection> walkingDirections;
    
    void Start() {
        walkingDirections = new List<WalkingDirection>();
    }

    // Update is called once per frame
    void Update() {

        // update list of buttons currently pressed
        Array.ForEach(new WalkingDirection[] {
            WalkingDirection.Down,
            WalkingDirection.Up,
            WalkingDirection.Left,
            WalkingDirection.Right
        }, walkDir => {
            if (Input.GetButtonDown(walkDir.ToString())) {
                walkingDirections.Add(walkDir);
            } else if (Input.GetButtonUp(walkDir.ToString())) {
                walkingDirections.RemoveAll(dir => dir == walkDir);
            }
        });        
    }

    public WalkingDirection GetAnimationDirection() {
        WalkingDirection facingDirection = 0;
        if(walkingDirections.Count > 0) {
            // most recently pressed button determines facing direction of sprite
            facingDirection = walkingDirections[walkingDirections.Count - 1];
        }
        return facingDirection;
    }

    public Vector3 GetMovementDirection() {
        float verticalDirection = 0;
        float horizontalDirection = 0;

        walkingDirections.ForEach(walkDir => {
            switch(walkDir) {
                case WalkingDirection.Down: 
                    verticalDirection = -1;
                    break;
                case WalkingDirection.Up: 
                    verticalDirection = 1;
                    break;  
                case WalkingDirection.Left: 
                    horizontalDirection = -1;
                    break;
                case WalkingDirection.Right: 
                    horizontalDirection = 1;
                    break;  
                default: break;
            }
        });

        return new Vector3(horizontalDirection, verticalDirection, 0).normalized;
    }
}
