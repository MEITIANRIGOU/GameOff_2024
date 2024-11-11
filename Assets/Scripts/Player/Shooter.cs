using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;

    public void OnShoot()
    {
        GameObject spawnedProj = Instantiate(projectile, transform);

        spawnedProj.transform.rotation = transform.parent.rotation;
        spawnedProj.transform.parent = null;
        spawnedProj.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }
}