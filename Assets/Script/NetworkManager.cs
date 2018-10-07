using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : NetworkLobbyManager
{
    [Space]
    [SerializeField] private bool gameIsRunning = false;

    [Space]
    public int ControllerP1;
    public int ControllerP2;
    public int ControllerP3;
    public int ControllerP4;


    private void Start()
    {
        CreateGame();
    }

    private void CreateGame()
    {
        networkAddress = "localhost";
        networkPort = 4444;
        StartHost();

        foreach (string s in Input.GetJoystickNames())
            print(s);
    }

    private void Update()
    {
        if (gameIsRunning)
            return;

        //Check if a new controller or a keyboard is used and add a player
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            int Controller = 0;

            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                Controller = 1;
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                Controller = 2;
            if (Input.GetKeyDown(KeyCode.Joystick3Button0))
                Controller = 3;
            if (Input.GetKeyDown(KeyCode.Joystick4Button0))
                Controller = 4;
            if (Input.GetKeyDown(KeyCode.Joystick5Button0))
                Controller = 5;
            if (Input.GetKeyDown(KeyCode.Joystick6Button0))
                Controller = 6;
            if (Input.GetKeyDown(KeyCode.Joystick7Button0))
                Controller = 7;
            if (Input.GetKeyDown(KeyCode.Joystick8Button0))
                Controller = 8;
            if (Input.GetKeyDown(KeyCode.Space))
                Controller = 9;

            CheckControllerToAdd(Controller);
        }
        //Check if user want to remove his player
        if ((Input.GetKey(KeyCode.JoystickButton1) && Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5)) || Input.GetKey(KeyCode.Escape))
        {
            int Controller = 0;

            if (Input.GetKey(KeyCode.Joystick1Button1) && Input.GetKey(KeyCode.Joystick1Button4) && Input.GetKey(KeyCode.Joystick1Button5))
                Controller = 1;
            if (Input.GetKey(KeyCode.Joystick2Button1) && Input.GetKey(KeyCode.Joystick2Button4) && Input.GetKey(KeyCode.Joystick2Button5))
                Controller = 2;
            if (Input.GetKey(KeyCode.Joystick3Button1) && Input.GetKey(KeyCode.Joystick3Button4) && Input.GetKey(KeyCode.Joystick3Button5))
                Controller = 3;
            if (Input.GetKey(KeyCode.Joystick4Button1) && Input.GetKey(KeyCode.Joystick4Button4) && Input.GetKey(KeyCode.Joystick4Button5))
                Controller = 4;
            if (Input.GetKey(KeyCode.Joystick5Button1) && Input.GetKey(KeyCode.Joystick5Button4) && Input.GetKey(KeyCode.Joystick5Button5))
                Controller = 5;
            if (Input.GetKey(KeyCode.Joystick6Button1) && Input.GetKey(KeyCode.Joystick6Button4) && Input.GetKey(KeyCode.Joystick6Button5))
                Controller = 6;
            if (Input.GetKey(KeyCode.Joystick7Button1) && Input.GetKey(KeyCode.Joystick7Button4) && Input.GetKey(KeyCode.Joystick7Button5))
                Controller = 7;
            if (Input.GetKey(KeyCode.Joystick8Button1) && Input.GetKey(KeyCode.Joystick8Button4) && Input.GetKey(KeyCode.Joystick8Button5))
                Controller = 8;
            if (Input.GetKeyDown(KeyCode.Escape))
                Controller = 9;

            CheckControllerToRemove(Controller);
        }
        //Check for ready calls
        if ((Input.GetKey(KeyCode.JoystickButton0) && Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5)) || Input.GetKey(KeyCode.Return))
        {
            int Controller = 0;

            if (Input.GetKey(KeyCode.Joystick1Button0) && Input.GetKey(KeyCode.Joystick1Button4) && Input.GetKey(KeyCode.Joystick1Button5))
                Controller = 1;
            if (Input.GetKey(KeyCode.Joystick2Button0) && Input.GetKey(KeyCode.Joystick2Button4) && Input.GetKey(KeyCode.Joystick2Button5))
                Controller = 2;
            if (Input.GetKey(KeyCode.Joystick3Button0) && Input.GetKey(KeyCode.Joystick3Button4) && Input.GetKey(KeyCode.Joystick3Button5))
                Controller = 3;
            if (Input.GetKey(KeyCode.Joystick4Button0) && Input.GetKey(KeyCode.Joystick4Button4) && Input.GetKey(KeyCode.Joystick4Button5))
                Controller = 4;
            if (Input.GetKey(KeyCode.Joystick5Button0) && Input.GetKey(KeyCode.Joystick5Button4) && Input.GetKey(KeyCode.Joystick5Button5))
                Controller = 5;
            if (Input.GetKey(KeyCode.Joystick6Button0) && Input.GetKey(KeyCode.Joystick6Button4) && Input.GetKey(KeyCode.Joystick6Button5))
                Controller = 6;
            if (Input.GetKey(KeyCode.Joystick7Button0) && Input.GetKey(KeyCode.Joystick7Button4) && Input.GetKey(KeyCode.Joystick7Button5))
                Controller = 7;
            if (Input.GetKey(KeyCode.Joystick8Button0) && Input.GetKey(KeyCode.Joystick8Button4) && Input.GetKey(KeyCode.Joystick8Button5))
                Controller = 8;
            if (Input.GetKeyDown(KeyCode.Return))
                Controller = 9;

            CheckControllerToSetReady(Controller);
        }
    }

    private void CheckControllerToAdd(int Controller)
    {
        if (Controller == ControllerP1 || Controller == ControllerP2 || Controller == ControllerP3 || Controller == ControllerP4)
            return;


        if (ControllerP1 == 0)
        {
            ControllerP1 = Controller;
            AddLocalPlayer(1);
            return;
        }
        if (ControllerP2 == 0)
        {
            ControllerP2 = Controller;
            AddLocalPlayer(2);
            return;
        }
        if (ControllerP3 == 0)
        {
            ControllerP3 = Controller;
            AddLocalPlayer(3);
            return;
        }
        if (ControllerP4 == 0)
        {
            ControllerP4 = Controller;
            AddLocalPlayer(4);
            return;
        }
    }

    private void AddLocalPlayer(int player)
    {
        if (player == 1 && GameObject.Find("LobbyPlayer (1)") != null)
        {
            SetupPlayerController(GameObject.Find("LobbyPlayer (1)").GetComponent<PlayerController>(), 1);
            return;
        }

        TryToAddPlayer();
    }

    private void CheckControllerToRemove(int Controller)
    {
        if (Controller == 0)
            return;

        if (Controller == ControllerP1)
        {
            ControllerP1 = 0;
            RemoveLocalPlayer(1);
            return;
        }
        if (Controller == ControllerP2)
        {
            ControllerP2 = 0;
            RemoveLocalPlayer(2);
            return;
        }
        if (Controller == ControllerP3)
        {
            ControllerP3 = 0;
            RemoveLocalPlayer(3);
            return;
        }
        if (Controller == ControllerP4)
        {
            ControllerP4 = 0;
            RemoveLocalPlayer(4);
            return;
        }
    }

    private void RemoveLocalPlayer(int player)
    {
        LobbyPlayer lobbyPlayer = GameObject.Find("LobbyPlayer (" + player + ")").GetComponent<LobbyPlayer>();
        lobbyPlayer.RemovePlayer();
    }

    private void CheckControllerToSetReady(int Controller)
    {
        if (Controller == 0)
            return;

        if (Controller == ControllerP1 && !GameObject.Find("LobbyPlayer (1)").GetComponent<LobbyPlayer>().readyToBegin)
        {
            SetPlayerReady(1);
            return;
        }
        if (Controller == ControllerP2 && !GameObject.Find("LobbyPlayer (2)").GetComponent<LobbyPlayer>().readyToBegin)
        {
            SetPlayerReady(2);
            return;
        }
        if (Controller == ControllerP3 && !GameObject.Find("LobbyPlayer (3)").GetComponent<LobbyPlayer>().readyToBegin)
        {
            SetPlayerReady(3);
            return;
        }
        if (Controller == ControllerP4 && !GameObject.Find("LobbyPlayer (4)").GetComponent<LobbyPlayer>().readyToBegin)
        {
            SetPlayerReady(4);
            return;
        }
    }

    private void SetPlayerReady(int player)
    {
        GameObject.Find("LobbyPlayer (" + player + ")").GetComponent<LobbyPlayer>().SendReadyToBeginMessage();
    }

    public void SetupPlayerController(PlayerController pController, int player)
    {
        if (player == 1)
        {
            if (ControllerP1 == 0)
                return;

            if (ControllerP1 == 9)
            {
                pController.Horizontal = "Horizontal_Keyboard";
                pController.Vertical = "Vertical_Keyboard";
                pController.JumpKey = KeyCode.Space;
                pController.UseKey = KeyCode.LeftShift;
                pController.TakeKey = KeyCode.F;
            }
            else
            {
                pController.Horizontal = "Horizontal_J" + ControllerP1;
                pController.Vertical = "Vertical_J" + ControllerP1;
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP1 + "Button0");
                pController.UseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP1 + "Button2");
                pController.TakeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP1 + "Button1");
            }
        }
        else if (player == 2)
        {
            if (ControllerP2 == 0)
                return;

            if (ControllerP2 == 9)
            {
                pController.Horizontal = "Horizontal_Keyboard";
                pController.Vertical = "Vertical_Keyboard";
                pController.JumpKey = KeyCode.Space;
                pController.UseKey = KeyCode.LeftShift;
                pController.TakeKey = KeyCode.F;
            }
            else
            {
                pController.Horizontal = "Horizontal_J" + ControllerP2;
                pController.Vertical = "Vertical_J" + ControllerP2;
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP2 + "Button0");
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP2 + "Button0");
                pController.UseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP2 + "Button2");
                pController.TakeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP2 + "Button1");
            }
        }
        else if (player == 3)
        {
            if (ControllerP3 == 0)
                return;

            if (ControllerP3 == 9)
            {
                pController.Horizontal = "Horizontal_Keyboard";
                pController.Vertical = "Vertical_Keyboard";
                pController.JumpKey = KeyCode.Space;
                pController.UseKey = KeyCode.LeftShift;
                pController.TakeKey = KeyCode.F;
            }
            else
            {
                pController.Horizontal = "Horizontal_J" + ControllerP3;
                pController.Vertical = "Vertical_J" + ControllerP3;
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP3 + "Button0");
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP3 + "Button0");
                pController.UseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP3 + "Button2");
                pController.TakeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP3 + "Button1");
            }
        }
        else if (player == 4)
        {
            if (ControllerP4 == 0)
                return;

            if (ControllerP4 == 9)
            {
                pController.Horizontal = "Horizontal_Keyboard";
                pController.Vertical = "Vertical_Keyboard";
                pController.JumpKey = KeyCode.Space;
                pController.UseKey = KeyCode.LeftShift;
                pController.TakeKey = KeyCode.F;
            }
            else
            {
                pController.Horizontal = "Horizontal_J" + ControllerP4;
                pController.Vertical = "Vertical_J" + ControllerP4;
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP4 + "Button0");
                pController.JumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP4 + "Button0");
                pController.UseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP4 + "Button2");
                pController.TakeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + ControllerP4 + "Button1");
            }
        }

        pController.setuped = true;
        if (!gameIsRunning)
            pController.transform.position = new Vector3(player * 2, 2, 0);
        else
        {
            GameObject spawnPoint = GameObject.Find("SpawnPoint(Clone)");
            pController.transform.position = spawnPoint.transform.position;
            Destroy(spawnPoint);
        }
        pController.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 5, 0);
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        int playerNumber = int.Parse(lobbyPlayer.name.Substring(lobbyPlayer.name.IndexOf("(") + 1, 1));

        gamePlayer.name = "GamePlayer (" + playerNumber + ")";
        SetupPlayerController(gamePlayer.GetComponent<PlayerController>(), playerNumber);

        lobbyPlayer.GetComponent<LobbyPlayer>().GameEntered();

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }
}
