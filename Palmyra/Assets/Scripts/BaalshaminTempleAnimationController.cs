using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaalshaminTempleAnimationController : MonoBehaviour
{
    Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     DestroyMonument();
        // }

        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     BuildMonument();
        // }
    }

    public void DestroyMonument()
    {
         anim.Play("BaalshaminFinalAnimation");
    }

    public void BuildMonument()
    {
        anim["BaalshaminFinalAnimation"].speed = -1;
        anim["BaalshaminFinalAnimation"].time = anim["BaalshaminFinalAnimation"].length;
        anim.Play("BaalshaminFinalAnimation");
    }

}
