using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    // TODO Make property with private setter;
    public Package ControlledPackage;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void PackagePickup([NotNull] Package package)
    {
        if (ControlledPackage != null && package != null) Debug.LogWarning("Overriding controlled package");
        ControlledPackage = package;
        package.OnPickUp();

    }

    public void PackageDrop(Package package)
    {
        if (package != ControlledPackage)
        {
            Debug.LogWarning("Dropped uncontrolled package");
            return;
        }

        ControlledPackage.TargetJoint.enabled = false;
        ControlledPackage.OnDropDown();
        ControlledPackage = null;
    }

    private void FixedUpdate()
    {
        if (ControlledPackage is null) return;

        var currentMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        ControlledPackage.TargetJoint.enabled = true;
        ControlledPackage.TargetJoint.target = currentMousePos;
    }
}
