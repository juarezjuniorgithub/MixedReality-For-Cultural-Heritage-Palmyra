﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaalHider : MonoBehaviour
{
    MeshRenderer _renderer;
    //[SerializeField] MeshRenderer groundRenderer;
    [SerializeField] SpriteRenderer[] images;
    [SerializeField] Transform parentTransform;
    private float initialScale;
    Color color = Color.white;
    [SerializeField] float maxDelta = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //_renderer = GetComponent<MeshRenderer>();
        initialScale = parentTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = parentTransform.localScale.x - initialScale;

        if(delta > 0) {
            color.a = Mathf.InverseLerp(maxDelta, 0, delta);

            foreach (var item in images) {
                item.material.color = color;
            }
        }
    }
}
