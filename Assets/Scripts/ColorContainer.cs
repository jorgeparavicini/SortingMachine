using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using UnityEngine;
using UnityEngine.UI;

public class ColorContainer : Container
{

    public Color color;
    public int score;

    private Text _scoreText;

    private void Start()
    {
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = color;
        }

        _scoreText = GetComponentInChildren<Text>();
    }

    public override void OnPackageDropped(Package package)
    {
        if (!(package is ColorPackage colorPackage))
        {
            Debug.Log($"Got unrecognized package in color container: {package}");
            return;
        }

        if (colorPackage.color == color)
        {
            score++;
            _scoreText.text = $"Score: {score}";
        }
        Destroy(colorPackage.gameObject);
    }
}
