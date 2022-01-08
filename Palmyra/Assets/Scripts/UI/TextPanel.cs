using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    public RawImage image;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject foreground;
    [SerializeField] private Animator animator;

    public void Initialize(string _title, string _body, Texture2D _image)
    {
        title.text = _title;
        body.text = _body;
        image.texture = _image;
    }

    public void PlayAnimation()
    {
        animator.Play("TextPanelAppear");
    }
}
