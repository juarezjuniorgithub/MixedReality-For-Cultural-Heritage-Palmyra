using UnityEngine;

public class Themeing : MonoBehaviour
{
    public Material uiMaterial;

    private void OnValidate() {
        ApplyTheme[] themeables = FindObjectsOfType<ApplyTheme>();
        foreach (var item in themeables) {
            if(item.type == ApplyTheme.Type.UI) {
                item.TryGetComponent(out MeshRenderer renderer);
                if(renderer != null) {
                    renderer.material = uiMaterial;
                }
            }
        }
    }
}
