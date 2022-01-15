using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TextPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    private Color textColor;
    public RawImage image;
    [SerializeField] private MeshRenderer background;
    private Color backgroundColor;
    [SerializeField] private Animator animator;

    bool fading;

    public void Initialize(string _title, string _body, Texture2D _image, Transform _parentScaleReference)
    {
        title.text = _title;
        body.text = _body;
        image.texture = _image;
        parentScaleReference = _parentScaleReference;
        backgroundColor = background.material.color;
        textColor = body.color;
        StartCoroutine(WaitBeforeFadingStart(_parentScaleReference));
    }

    IEnumerator WaitBeforeFadingStart(Transform _parentScaleReference)
    {
        yield return new WaitForSeconds(2);
        initialScale = _parentScaleReference.localScale.x;
        maxDelta = initialScale * 3;
        fading = true;
    }

    public void PlayAnimation()
    {
        animator.Play("TextPanelAppear");
    }


    //Hide logic
    Color color = Color.white;
    Transform parentScaleReference;
    private float initialScale;
    [SerializeField] float maxDelta = 0.2f;

    void Update()
    {
        if (!fading) return;

        float delta = parentScaleReference.localScale.x - initialScale;

        if (delta > 0)
        {
            float newAlpha = Mathf.InverseLerp(maxDelta, 0, delta);
            color.a = newAlpha;
            backgroundColor.a = newAlpha;
            textColor.a = newAlpha;
            image.material.color = color;
            background.material.color = backgroundColor;
            title.color = textColor;
            body.color = textColor;
        }
    }
}
