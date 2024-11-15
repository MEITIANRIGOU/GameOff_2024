using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;

    LayerMask layerHit;

    private void Start()
    {
        layerHit = LayerMask.NameToLayer("Hit");
    }

    public void OnShoot()
    {
        GameObject spawnedProj = Instantiate(projectile, transform);

        spawnedProj.layer = layerHit;
        spawnedProj.transform.rotation = transform.parent.rotation;
        spawnedProj.transform.parent = null;
        spawnedProj.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }
}