using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDissolveMaterials : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> renderers = new List<MeshRenderer>();
    private Dictionary<MeshRenderer, Material> materials = new Dictionary<MeshRenderer, Material>();
    [SerializeField] Material simpleLitMat;
    [SerializeField] DissolveEffect dissolveEffect;

    void Start()
    {
        foreach (var item in renderers) {
            materials.Add(item, item.sharedMaterial);
        }
        dissolveEffect.onDisappearStarting += SetMaterialsToDissolve;
        dissolveEffect.onAppearEnded += SetMaterialsToSimpleLit;
    }

    private void SetMaterialsToSimpleLit() {
        foreach (var item in renderers) {
            item.material.shader = simpleLitMat.shader;
            item.material.mainTexture = materials[item].mainTexture;
        }
    }

    private void SetMaterialsToDissolve() {
        foreach (var item in renderers) {
            item.material.shader = materials[item].shader;
        }
    }

    private void OnDestroy() {
        dissolveEffect.onDisappearStarting -= SetMaterialsToDissolve;
        dissolveEffect.onAppearEnded -= SetMaterialsToSimpleLit;
    }
}
