using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalGrowAnimation : MonoBehaviour
{
    public GameObject tooltip;
    public MeshRenderer MeshrenderSyria;

    public void Grow()
    {
       StartCoroutine(CoGrow());
    }

    IEnumerator CoGrow()
    {
        yield return new WaitForSeconds(2);
        MeshrenderSyria.enabled = true;
        tooltip.SetActive(true);
    }
}
