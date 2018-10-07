using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    GameObject PlayerPrefab;
    [SerializeField]
    float RespawnTime = 5;
    public Vector3 RespawnPosition;
    public void Respawn()
    {
        StartCoroutine("WaitForRespawn");
        Instantiate(PlayerPrefab, RespawnPosition, Quaternion.identity);
    }
    IEnumerator WaitForRespawn()
    {
    yield return new WaitForSeconds(RespawnTime);
    }
}
