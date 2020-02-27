using System.Collections;
using UnityEngine;

public class RandomTimePackageSpawner: PackageSpawner
{
    public float minRespawnTime = 3f;
    public float maxRespawnTime = 10f;

    protected override IEnumerator SpawnDelay()
    {
        
        var random = Random.Range(minRespawnTime, maxRespawnTime);
        yield return new WaitForSeconds(random);
    }
}