using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from https://github.com/RetryEntry/UnityAnimatorChangeSprite
public class TextureSwapper : MonoBehaviour {

    [SerializeField]
    private Texture2D swappedTexture;

    private Shader swapShader;
    private SpriteRenderer thisRenderer;
    void Awake()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
        if (thisRenderer) {
            swapShader = Shader.Find("Custom/SwapTwo");
            if (!swapShader) {
                Debug.LogError("You dont have shader... ");
            }
        }
        else {
            Debug.LogError("There is NO spriterenderer attached to gameobject " + this.name);
        }

        // Obtain texture from current sprite if not defined
        if(!swappedTexture) {
            swappedTexture = thisRenderer.sprite.texture;
        }
    }

    /// <summary>
    /// this will swap our used animator texture to another one
    /// </summary>
    /// <param name="_toWhat"></param>
    public void SwapTexture(Texture2D _toWhat) {
        swappedTexture = _toWhat;
    }

    private void SwapTexture() {
        if (thisRenderer) {
            if (!swapShader) {
                Debug.LogError("You dont have shader... ");
            } else {
                Material _newMat = new Material(swapShader);
                thisRenderer.material = _newMat;
                thisRenderer.material.SetTexture("_MainTex2", swappedTexture);
            }

        } else {
            Debug.LogError("There is NO spriterenderer attached to gameobject " + this.name);
        }
    }

    void Update() {
        if (swappedTexture != null) {
            SwapTexture();
            swappedTexture = null;
        }
    }
}