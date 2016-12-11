public static class GameState {
  public const int ATTRACT = 0;
  public const int MENU = 1;
  public const int SETUP = 2;
  public const int INIT = 3;
  public const int PLAYING = 4;
  public const int GAMEOVER = 5;

  public static System.Action<int, int> OnStateChange;

  public static int State {
    get { return _state; }
  }

  private static int _state = ATTRACT;

  public static void SetState(int newState) {
    var oldState = _state;
    _state = newState;
    if (OnStateChange != null) OnStateChange(oldState,  newState);
  }
}