using System;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    private const string PackageTag = "Package";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(PackageTag)) return;

        var package = other.gameObject.GetComponent<Package>();
        if (package is null) throw new InvalidOperationException("Invalid Trigger");

        package.CurrentContainer = this;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(PackageTag)) return;

        var package = other.gameObject.GetComponent<Package>();
        if (package is null) throw new InvalidOperationException("Invalid Trigger");

        package.CurrentContainer = null;
    }

    public abstract void OnPackageDropped(Package package);
}
