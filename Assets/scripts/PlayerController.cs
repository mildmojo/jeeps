using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {
  public bool isJumping;
  public bool isMoving;
  public Vector3 direction = Vector3.up;

  public System.Action<KeyCode> OnRebind;
  public string buttonName;

  private bool isRebinding = false;
  private KeyCode buttonCode;
  private AnyInput anyInput;

  private static List<KeyCode> inputButtonRegistry = new List<KeyCode>();

  // Use this for initialization
  void Start () {
    anyInput = AnyInput.instance;
  }

  // Update is called once per frame
  void Update () {
    if (isRebinding) {
      var pressedInputs = anyInput.GetAllPressed().Where(code => !inputButtonRegistry.Contains(code));
      if (pressedInputs.Any()) {
        var code = pressedInputs.First();
        inputButtonRegistry.Add(code);
        SetInputButton(code);
        StopInputRebind();
      }
    }
  }

  public void SetInputButton(KeyCode code) {
    buttonCode = code;
    buttonName = AllButtons.namesByValue[code];
    if (OnRebind != null) OnRebind(code);
  }

  public void StartInputRebind() {
    isRebinding = true;
  }

  public void StopInputRebind() {
    isRebinding = false;
  }
}
