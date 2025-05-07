// joins played on main scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public GameObject CanvasReference;
    public bool hasInitialized;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.scene.IsValid()) return;
        CanvasReference.GetComponent<CharacterSelectScript>().PlayerJoin(this.gameObject);
    }
}
