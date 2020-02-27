using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventArgs;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class PackageSpawner : MonoBehaviour
{
    public List<PackageSpawnItem> packages = new List<PackageSpawnItem>();
    public List<PackageSpawnItem> SpawnablePackages => packages.Where(x => x.CanSpawn).ToList();
    public int TotalPriority => SpawnablePackages.Sum(x => x.priority);
    public bool autoStart;
    public bool Spawning { get; protected set; }

    private IEnumerator _coroutine;

    protected virtual void Start()
    {
       if (autoStart) StartSpawner();
    }

    private PackageSpawnItem GetPackage()
    {
        var random = Random.Range(0f, 1f);
        var priorityCounter = 0f;
        foreach (var package in SpawnablePackages)
        {
            var nextPriority = priorityCounter + (float)package.priority / TotalPriority;
            if (priorityCounter < random && random < nextPriority)
            {
                return package;
            }

            priorityCounter = nextPriority;
        }
        throw new InvalidOperationException("Failed to get a random priority based package");
    }

    protected virtual void Spawn()
    {
        if (SpawnablePackages.Count == 0)
        {
            Spawning = false;
            return;
        }
        var package = GetPackage();
        var localTransform = transform;
        var position = localTransform.position;
        var instance = Instantiate(package.package, position, localTransform.rotation, localTransform);
        package.OnSpawn(this, new PackageSpawnedEventArgs(instance, position));
    }

    private IEnumerator SpawnerCoroutine()
    {
        Debug.Log("Spawner Started");
        while (Spawning)
        {
            yield return StartCoroutine(SpawnDelay());
            Spawn();
        }

        _coroutine = null;
    }

    public void StartSpawner()
    {
        Debug.Log("Starting Spawner");
        if (_coroutine != null)
        {
            Debug.LogWarning($"Spawner coroutine already running on object: {this}");
            return;
        }
        _coroutine = SpawnerCoroutine();
        Spawning = true;
        StartCoroutine(_coroutine);
    }

    public void StopSpawner()
    {
        Debug.Log("Stopping Spawner");
        if (_coroutine is null)
        {
            Debug.LogWarning($"No spawner coroutine to stop on object: {this}");
            return;
        }

        Spawning = false;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    protected abstract IEnumerator SpawnDelay();
}
