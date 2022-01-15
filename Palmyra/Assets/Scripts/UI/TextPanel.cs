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
    [SerializeField] private MeshRenderer foreground;
    private Color foregroundColor;
    [SerializeField] private Animator animator;

    public void Initialize(string _title, string _body, Texture2D _image, Transform _parentScaleReference)
    {
        title.text = _title;
        body.text = _body;
        image.texture = _image;
        parentScaleReference = _parentScaleReference;
        backgroundColor = background.material.color;
        foregroundColor = foreground.material.color;
        textColor = body.color;
        StartCoroutine(WaitBeforeSetingInitialScale(_parentScaleReference));
    }

    IEnumerator WaitBeforeSetingInitialScale(Transform _parentScaleReference)
    {
        yield return new WaitForSeconds(2);
        initialScale = _parentScaleReference.localScale.x;
        maxDelta = initialScale * 3;
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
        float delta = parentScaleReference.localScale.x - initialScale;

        if (delta > 0)
        {
            float newAlpha = Mathf.InverseLerp(maxDelta, 0, delta);
            color.a = newAlpha;
            backgroundColor.a = newAlpha;
            foregroundColor.a = newAlpha;
            textColor.a = newAlpha;
            image.material.color = color;
            foreground.material.color = foregroundColor;
            background.material.color = backgroundColor;
            title.color = textColor;
            body.color = textColor;
        }
    }
}
