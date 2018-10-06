using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "FallingBlock")
            return;

        if (collision.transform.tag == "Player")
        {
            return;
        }

        EnvironementManager environement = GameObject.Find("GameManager").GetComponent<EnvironementManager>();
        Tilemap tilemap = environement.tilemap;
        environement.tilemap.SetTile(tilemap.WorldToCell(transform.position), environement.GetDirtTile(tilemap.WorldToCell(transform.position)));
        Destroy(gameObject);
    }
}
