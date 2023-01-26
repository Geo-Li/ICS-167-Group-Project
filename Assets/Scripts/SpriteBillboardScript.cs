using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Paper Mouse Games at 2D Sprites in 3D World - Unity Sprite Billboarding Tutorial 
// link : https://www.youtube.com/watch?v=FjJJ_I9zqJo
public class SpriteBillboardScript : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;

    // Update is called once per frame
    void Update()
    {
        if (freezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        else
            transform.rotation = Camera.main.transform.rotation;
    }
}
