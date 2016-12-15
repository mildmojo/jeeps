using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
  public Sprite stoneTileSprite;
  public Sprite iceTileSprite;
  public GameObject stoneTilePrefab;
  public GameObject iceTilePrefab;
  public GameObject playerPrefab;
  [RangeAttribute(1, 10)]
  public int maxPlayers;
  public int boardWidth;
  public int boardHeight;
  [RangeAttribute(0f, 1f)]
  public float boardDensity;
  public float gameSpeed = 0.5f;
  public float countdownLength;
  public Text timerText;
  public GameObject playerRootObject;
  public GameObject boardRootObject;

  public List<GameObject> tilePrefabs;

  private CanvasGroup cgTimerText;
  private List<PlayerController> _players = new List<PlayerController>();
  private float _countdownTimer;
  private List<Tile> board;
  private ShuffleDeck tileDeck;

  private Dictionary<int, System.Action> _updateActions;

  private List<Vector3> playersSrc = new List<Vector3>();
  private List<Vector3> playersDest = new List<Vector3>();
  private List<Tile> playersSrcTile = new List<Tile>();
  private List<Tile> playersDestTile = new List<Tile>();

  void Awake() {
    for (var i = 0; i < maxPlayers; i++) {
      var newPlayer = Instantiate(playerPrefab);
      _players.Add(newPlayer.GetComponent<PlayerController>());
      newPlayer.transform.parent = playerRootObject.transform;
    }

    cgTimerText = timerText.GetComponent<CanvasGroup>();

    _updateActions = new Dictionary<int, System.Action> {
      { GameState.ATTRACT,  CheckStart },
      { GameState.SETUP,    CountdownToStart },
      { GameState.INIT,     () => {} },
      { GameState.PLAYING,  GameplayTick },
      { GameState.GAMEOVER, () => {} },
    };

    tileDeck = new ShuffleDeck(tilePrefabs.Concat(tilePrefabs));
  }

  void Start() {
    FindBoard();
    ResetPlayers();

    GameState.OnStateChange += OnStateChange;
    // players.Add(GameObject.Find("jeep_pink").GetComponent<PlayerController>());

    // players.First().OnRebind += code => {
    //   Debug.Log("Player using " + AllButtons.namesByValue[code]);
    //   GameState.SetState(GameState.PLAYING);
    // };
    // players.First().StartInputRebind();
    // HackGenerateGrid();

    // board = new Grid(boardWidth, boardHeight, stoneTileSprite);


    // Create board
    /*

    */
    // Distribute arrows
    // Distribute powerups
    // Init game timer engine
    // Set game state to 'playing'
  }

  void Reset() {
    ResetPlayers();
  }

  void ResetPlayers() {
    var marginLeft = Mathf.FloorToInt((boardWidth - _players.Count) / 2f);
    for (var i = 0; i < _players.Count; i++) {
      var player = _players[i];
      var player2d = new TwoDee(player.gameObject.transform.FindChild("JeepSprite").gameObject);
      player.transform.position = new Vector2(board[marginLeft + i].transform.position.x, -player2d.height);
Debug.Log(player.transform.position);
    }
  }

  void OnStateChange(int oldState, int newState) {
    switch (newState) {
      case GameState.SETUP:
        _countdownTimer = countdownLength;
        foreach (var player in _players) {
          player.StartInputRebind();
          // Set up an OnRebind listener that remembers which player is on which
          // button if that button is released during setup, hide player and start
          // player's rebind again
        }
        PopulateBoard();

        break;
    }
  }

  // Was used to generate board prefab.
  // void HackGenerateGrid() {
  //   GameObject tile = GameObject.Find("Tile");
  //   var tile2d = new TwoDee(tile.transform.FindChild("TileBaseLayer").gameObject);

  //   for (var i = 0; i < boardWidth * boardHeight; i++) {
  //     var newTile = Instantiate(tilePrefab);
  //     var x = i % boardHeight;
  //     var y = i / boardHeight;
  //     newTile.transform.position = tile2d.position + new Vector3(x * tile2d.width, -y * tile2d.height, 0);
  //   }
  // }

  void Update() {
    _updateActions[GameState.State]();
  }

  void FindBoard() {
    var children = new List<GameObject>();
    for (var i = 0; i < boardRootObject.transform.childCount; i++) {
      children.Add(boardRootObject.transform.GetChild(i).gameObject);
    }

    children.Sort((v1, v2) => CompareVectors(v1.transform.position, v2.transform.position));
    board = children
      .Select(child => child.GetComponent<Tile>())
      .ToList();
  }

  void PopulateBoard() {
    GameObject newTileObject;

    for (var i = 0; i < board.Count; i++) {
      var tile = board[i];
      if (Random.Range(0f, 1f) < boardDensity) {
        newTileObject = (GameObject) Instantiate((GameObject) tileDeck.Draw(), tile.transform.position, Quaternion.identity);
      } else {
        newTileObject = (GameObject) Instantiate(stoneTilePrefab, tile.transform.position, Quaternion.identity);
      }
      newTileObject.transform.parent = tile.transform.parent;
      var newTile = newTileObject.GetComponent<Tile>();
      board[i] = newTile;
      newTile.x = i % boardWidth;
      newTile.y = i / boardWidth;
      Destroy(tile);
    }
  }

  void CheckStart() {
    if (AnyInput.instance.WasPressed()) {
      GameState.SetState(GameState.SETUP);
    }
  }

  void CountdownToStart() {
    _countdownTimer -= Time.deltaTime;

    // All buttons released? Return to attract mode.
    if (!AnyInput.instance.AnyDown()) {
      timerText.text = "";
      GameState.SetState(GameState.ATTRACT);
    }

    // Any input pressed in the last frame? Reset the timer.
    if (AnyInput.instance.WasPressed()) {
      _countdownTimer = countdownLength;
    }

    if (_countdownTimer > 0f) {
      timerText.text = "Race begins in " + Mathf.Ceil(_countdownTimer);
    } else {
      timerText.text = "GO!!";
      LeanTween.value(gameObject, value => cgTimerText.alpha = value, 1f, 0f, 2f);

      playersSrc.Clear();
      playersDest.Clear();
      for (var i = 0; i < _players.Count; i++) {
        var player = _players[i];
        var startingPos = player.transform.position;
        var startingTile = getTile(i + 2, boardHeight - 1);
        playersSrc.Add(new Vector2(startingPos.x, startingPos.y));
        playersDest.Add(new Vector2(startingTile.transform.position.x, startingTile.transform.position.y));
        playersSrcTile.Add(null);
        playersDestTile.Add(startingTile);
        player.SetDirection(playersDest[i] - playersSrc[i]);
      }

      GameState.SetState(GameState.PLAYING);
    }
  }

  void GameplayTick() {
    var tileWidth = new TwoDee(board[0].gameObject).width;
    var sqrTileWidth = Mathf.Pow(tileWidth, 2);

    for (var i = 0; i < _players.Count; i++) {
      var player = _players[i];

      var sqrBeforeDist = (player.transform.position - (Vector3) playersSrc[i]).sqrMagnitude;
      var vecTravel = player.direction * gameSpeed * Time.deltaTime;
      player.transform.Translate(vecTravel, Space.World);
      var sqrAfterDist = (player.transform.position - (Vector3) playersSrc[i]).sqrMagnitude;

Debug.DrawRay(player.transform.position, (Vector3) playersDest[i] - player.transform.position);

      // This may mess up if jump happens before tile midpoint; animation may
      // jump to 0.25, then go back to 0, then up to 1.0.
      if (player.StartedJump()) {
        player.SetTravelDist(tileWidth);
      } else if (player.IsJumping()) {
        player.Travel(vecTravel.magnitude);
      }

      // Check for exit/enter.
      if (sqrBeforeDist < sqrTileWidth/2f && sqrAfterDist >= sqrTileWidth/2f) {
        // Don't interact with tile if jumping.
        if (!player.IsJumping()) {
          if (playersSrcTile[i] != null) playersSrcTile[i].SendMessage("OnPlayerExit", player);
          if (playersDestTile[i] != null) playersDestTile[i].SendMessage("OnPlayerEnter", player);
        }
        // Need to wrap?
        if (Vector3.Dot(playersDest[i] - playersSrc[i], player.direction) < 0) {
          // Place player on the outer edge of the tile on the opposite side of the board.
          player.transform.position = playersDest[i] + -player.direction.normalized * tileWidth/2f;
          // Pretend player is coming from midpoint of non-existent tile off the edge of the board.
          playersSrc[i] = playersDest[i] + -player.direction.normalized * tileWidth;
        }
      }

      // Check for arrive.
      if (sqrBeforeDist < sqrTileWidth && sqrAfterDist >= sqrTileWidth) {
        player.transform.position = playersDest[i];

        // Don't interact with tile if jumping.
        if (!player.IsJumping()) {
          if (playersDestTile[i] != null) playersDestTile[i].SendMessage("OnPlayerArrive", player);
        }

        playersSrcTile[i] = playersDestTile[i];
        playersDestTile[i] = getNextTile(playersSrcTile[i], player.direction);
        playersSrc[i] = player.transform.position;
        playersDest[i] = playersDestTile[i].transform.position;
      }
    }
  }

  // Sort top-to-bottom, with Unity origin at bottom-left of screen.
  int CompareVectors(Vector3 v1, Vector3 v2) {
    if (v1.y > v2.y)
        return -1;
    if (v1.y == v2.y)
    {
        if (v1.x == v2.x)
            return 0;
        if (v1.x < v2.x)
            return -1;
    }
    return 1;
  }

  Tile getTile(int x, int y) {
    return board[y * boardWidth + x];
  }

  Tile getNextTile(Tile fromTile, Vector3 direction) {
    var x = fromTile.x;
    var y = fromTile.y;
    var normDir = direction.normalized;
    var dirX = Vector3.Dot(direction, Vector3.right);
    var dirY = Vector3.Dot(direction, Vector3.up);
Debug.Log("dirX: " + dirX + ", dirY:" + dirY);
    x += dirX > 0.3f ? 1 : dirX < -0.3f ? -1 : 0;
    y += dirY > 0.3f ? -1 : dirY < -0.3f ? 1 : 0;

    // Wrap X and Y.
Debug.Log("pre X: " + x + ", pre Y:" + y);
    if (x < 0) x = boardWidth + x;
    if (x >= boardWidth) x = x % boardWidth;
    if (y < 0) y = boardHeight + y;
    if (y >= boardHeight) y = y % boardHeight;
Debug.Log("Go to " + x + ", " + y);
    return board[y * boardWidth + x];
  }
}
