using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
  public Sprite stoneTileSprite;
  public Sprite iceTileSprite;
  public int boardWidth;
  public int boardHeight;
  public float gameSpeed = 0.5f;
  public float countdownLength;
  public Text timerText;

public GameObject tilePrefab;
  private CanvasGroup cgTimerText;
  private List<PlayerController> _players = new List<PlayerController>();
  private float _countdownTimer;
  // private  board;

  private Dictionary<int, System.Action> _updateActions;

  void Awake() {
    _players = GameObject.FindObjectsOfType<PlayerController>().ToList();
    cgTimerText = timerText.GetComponent<CanvasGroup>();

    _updateActions = new Dictionary<int, System.Action> {
      { GameState.ATTRACT,  () => {} },
      { GameState.MENU,     () => {} },
      { GameState.SETUP,    CountdownToStart },
      { GameState.INIT,     () => {} },
      { GameState.PLAYING,  GameplayTick },
      { GameState.GAMEOVER, () => {} },
    };
  }

  void Start() {
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
    _players = new List<PlayerController>();
  }

  // Was used to generate board prefab.
  void HackGenerateGrid() {
    GameObject tile = GameObject.Find("Tile");
    var tile2d = new TwoDee(tile.transform.FindChild("TileBaseLayer").gameObject);

    for (var i = 0; i < boardWidth * boardHeight; i++) {
      var newTile = Instantiate(tilePrefab);
      var x = i % boardHeight;
      var y = i / boardHeight;
      newTile.transform.position = tile2d.position + new Vector3(x * tile2d.width, -y * tile2d.height, 0);
    }
  }

  void Update() {
    _updateActions[GameState.State]();
  }

  void GameplayTick() {
    foreach(var player in _players) {
      player.gameObject.transform.Translate(player.direction * gameSpeed * Time.deltaTime);
    }
  }

  void CountdownToStart() {
    _countdownTimer -= Time.deltaTime;

    // All buttons released? Return to attract mode.
    if (!AnyInput.instance.AnyDown()) {
      GameState.SetState(GameState.ATTRACT);
    }

    // Any input pressed in the last frame? Reset the counter.
    if (AnyInput.instance.WasPressed()) {
      _countdownTimer = countdownLength;
    }

    if (_countdownTimer > 0f) {
      timerText.text = "Race begins in " + Mathf.Ceil(_countdownTimer);
    } else {
      timerText.text = "GO!!";
      LeanTween.value(gameObject, value => cgTimerText.alpha = value, 1f, 0f, 2f);
      GameState.SetState(GameState.PLAYING);
    }


  }
}
