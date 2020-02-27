using System;
using System.Net.Mime;
using EventArgs;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PackageSpawnItem
{
    public GameObject package;

    /// <summary>
    /// How often this package gets spawned.
    /// </summary>
    public int priority = 10;

    public int maxSpawns = -1;

    public int TimesSpawned { get; private set; } 

    public bool CanSpawn => maxSpawns == -1 || TimesSpawned < maxSpawns;

    public PackageSpawnItem() {}

    public PackageSpawnItem(GameObject package)
    {
        this.package = package;
    }

    public void OnSpawn(object sender, PackageSpawnedEventArgs e)
    {
        TimesSpawned++;
    }
}