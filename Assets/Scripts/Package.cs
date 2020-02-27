using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TargetJoint2D))]
public abstract class Package : MonoBehaviour
{
    private const string SceneObjectsTag = "Scene";

    private Player _player;
    private Collider2D _collider;
    private List<Collider2D> _sceneObjects;
    [CanBeNull] private Container _currentContainer;

    public bool IsPlayerControlled { get; private set; }
    public TargetJoint2D TargetJoint { get; private set; }
    public Container CurrentContainer
    {
        get => _currentContainer;
        set
        {
            if (_currentContainer != null && value != null) Debug.LogWarning($"Current Container being overriden by {value}");
            _currentContainer = value;

            if (!IsPlayerControlled) _currentContainer?.OnPackageDropped(this);
        }
    }
    
    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _collider = GetComponent<Collider2D>();
        TargetJoint = GetComponent<TargetJoint2D>();

        UpdateSceneObjects();
    }

    private void UpdateSceneObjects()
    {
        _sceneObjects = GameObject.FindGameObjectsWithTag(SceneObjectsTag)
            .Select(obj => obj.GetComponent<Collider2D>())
            .ToList();
    }

    private void OnMouseDown()
    {
        _player.PackagePickup(this);
    }

    private void OnMouseUp()
    {
        _player.PackageDrop(this);
    }

    private void DisableSceneCollision()
    {
        foreach (var sceneObject in _sceneObjects)
        {
            Physics2D.IgnoreCollision(sceneObject, _collider);
        }
    }

    private void EnableSceneCollision()
    {
        foreach (var sceneObject in _sceneObjects)
        {
            Physics2D.IgnoreCollision(sceneObject, _collider, false);
        }
    }

    public void OnPickUp()
    {
        DisableSceneCollision();
        IsPlayerControlled = true;
    }

    public void OnDropDown()
    {
        EnableSceneCollision();
        IsPlayerControlled = false;
        if (CurrentContainer != null) CurrentContainer.OnPackageDropped(this);
    }
}
