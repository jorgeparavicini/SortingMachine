using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorPackage : Package
{
    private SpriteRenderer _spriteRenderer;

    public Color color;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = color;
    }
}
