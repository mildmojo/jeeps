using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
  public Sprite stoneTileSprite;
  public Sprite iceTileSprite;
  public int boardWidth;
  public int boardHeight;

  private Grid board;
  private enum GameState {ATTRACT, MENU, SETUP, INIT, PLAYING, GAMEOVER};
  private GameState state;

  void Awake() {
  }

  void Start() {
    // Create board
    /*

    */
    // Distribute arrows
    // Distribute powerups
    // Init game timer engine
    // Set game state to 'playing'
  }

  void Update() {

  }

  void setState(GameState newState) {
    // TODO: Add state machine here
    state = newState;
  }
}
