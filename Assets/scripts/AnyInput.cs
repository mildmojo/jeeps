using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AnyInput : MonoBehaviour {
  public static AnyInput instance;

  private bool isRunning;
  private bool isAnyDown;
  private readonly List<KeyCode> wasPressedInputs = new List<KeyCode>();
  private readonly List<KeyCode> wasReleasedInputs = new List<KeyCode>();
  private readonly Dictionary<KeyCode, bool> inputStates = new Dictionary<KeyCode, bool>();

  void Awake() {
    if (instance == null) instance = this;

    // Automatically start.
    StartPolling();
  }

  void Update() {
    if (isRunning) PollControls();
  }

  // Resets current state, so any currently-held buttons will register presses
  // on the next frame.
  public void StartPolling() {
    wasPressedInputs.Clear();
    wasReleasedInputs.Clear();
    inputStates.Clear();
    foreach (var code in AllButtons.values.Cast<KeyCode>()) {
      inputStates[code] = false;
    }

    isRunning = true;
  }

  public bool AnyDown() {
    return isAnyDown;
  }

  public bool WasPressed() {
    return wasPressedInputs.Any();
  }

  public bool WasReleased() {
    return wasReleasedInputs.Any();
  }

  public List<KeyCode> GetAllPressed() {
    return wasPressedInputs;
  }

  public List<KeyCode> GetAllReleased() {
    return wasReleasedInputs;
  }

  private void PollControls() {
    bool isDown;
    bool oldIsDown;
    wasPressedInputs.Clear();
    wasReleasedInputs.Clear();

    // Poll every damn button we know about.
    foreach (var code in AllButtons.values.Cast<KeyCode>()) {
      // Read current and previous button states.
      isDown = Input.GetKey(code);
      oldIsDown = inputStates[code];
      // Just pressed?
      if (!oldIsDown && isDown) wasPressedInputs.Add(code);
      // Just released?
      if (oldIsDown && !isDown) wasReleasedInputs.Add(code);
      // Store state.
      inputStates[code] = isDown;
      isAnyDown = isAnyDown || isDown;
    }
  }
}
