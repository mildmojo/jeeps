using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
  public Sprite stoneTileSprite;
  public Sprite iceTileSprite;
  public int boardWidth;
  public int boardHeight;
  public float gameSpeed = 0.5f;

public GameObject tilePrefab;

  private List<PlayerController> players = new List<PlayerController>();
  // private  board;

  void Awake() {
  }

  void Start() {
    players.Add(GameObject.Find("jeep_pink").GetComponent<PlayerController>());

    players.First().OnRebind += code => {
      Debug.Log("Player using " + AllButtons.namesByValue[code]);
      GameState.SetState(GameState.PLAYING);
    };
    players.First().StartInputRebind();
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
    players = new List<PlayerController>();
  }

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
    if (GameState.State == GameState.PLAYING) {
      foreach(var player in players) {
        player.gameObject.transform.Translate(Vector3.up * gameSpeed * Time.deltaTime);
      }
    }
  }

}
