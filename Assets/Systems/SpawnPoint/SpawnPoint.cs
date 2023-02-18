using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] prefabs;

    public void Spawn()
    {
        int index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index], transform.position, transform.rotation, transform);
    }
}
