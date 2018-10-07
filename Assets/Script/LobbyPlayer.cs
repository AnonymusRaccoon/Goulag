using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{
    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();

        if (!GetComponent<PlayerController>().setuped)
        {
            NetworkManager netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

            int playerNumber = 0;

            if (netManager.ControllerP1 != 0 && GameObject.Find("LobbyPlayer (1)") == null)
                playerNumber = 1;
            if (netManager.ControllerP2 != 0 && GameObject.Find("LobbyPlayer (2)") == null)
                playerNumber = 2;
            if (netManager.ControllerP3 != 0 && GameObject.Find("LobbyPlayer (3)") == null)
                playerNumber = 3;
            if (netManager.ControllerP4 != 0 && GameObject.Find("LobbyPlayer (4)") == null)
                playerNumber = 4;

            if (playerNumber == 0)
                playerNumber = 1;

            gameObject.name = "LobbyPlayer (" + playerNumber + ")";
            netManager.SetupPlayerController(gameObject.GetComponent<PlayerController>(), playerNumber);
        }
        else
            LobbyEntered();
    }

    private void LobbyEntered()
    {
        GetComponent<PlayerController>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        transform.position = new Vector3(0, 2, 0);
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
    }

    public void GameEntered()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
