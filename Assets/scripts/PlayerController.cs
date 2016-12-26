using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {
  public bool isMoving;
  public Vector3 direction = Vector3.up;

  public System.Action<KeyCode> OnRebind;
  public string buttonName;

  private bool isRebinding = false;
  private bool startedJump;
  private bool isJumping;
  private float travelLength;
  private float travelPct;
  private KeyCode buttonCode = KeyCode.None;
  private AnyInput anyInput;
  private Vector3 originalScale;
  private Animator animator;

  private static List<KeyCode> inputButtonRegistry = new List<KeyCode>();

  private static bool registerButton(KeyCode button) {
    if (inputButtonRegistry.Contains(button)) return false;
    inputButtonRegistry.Add(button);
    return true;
  }

  private static void unregisterButton(KeyCode button) {
    inputButtonRegistry.Remove(button);
  }

  void Start () {
    anyInput = AnyInput.instance;
    originalScale = transform.localScale;
    animator = transform.FindChild("JeepSprite").gameObject.GetComponent<Animator>();
  }

  void Update () {
    UpdateJump();
    UpdateRebind();
  }

  void UpdateJump() {
    startedJump = false;
    if (buttonCode != KeyCode.None && Input.GetKey(buttonCode)) {
      startedJump = !isJumping;
      isJumping = true;
    }

    if (isJumping) {
      isJumping = travelPct <= 1f;
      // foreach (AnimationState state in jumpAnimation) {
      //   state.normalizedTime = travelPct;
      // }
      animator.Play("JeepJump", -1, travelPct);
    }
  }

  void UpdateRebind() {
    if (!isRebinding) return;

    var pressedInputs = anyInput.GetAllPressed();
// Debug.Log("Saw keys: " + pressedInputs.Select(x => x.ToString()).Aggregate("", (sum, str) => sum + "," + str));
    for (var i = 0; i < pressedInputs.Count; i++) {
      if (buttonCode == KeyCode.None && registerButton(pressedInputs[i])) {
        BindInputButton(pressedInputs[i]);
        GameManager.instance.AddPlayer(this);
Debug.Log("Player controller added " + buttonCode + " (" + Time.frameCount + " " + i + ")");
        break;
      } else if (buttonCode == pressedInputs[i]) {
Debug.Log("Player controller resumed " + buttonCode + " (" + Time.frameCount + " " + i + ")");
        GameManager.instance.AddPlayer(this);
      }
    }

    var releasedInputs = anyInput.GetAllReleased();
    if (releasedInputs.Contains(buttonCode)) {
      // unregisterButton(buttonCode);
      // UnbindInputButton();
      GameManager.instance.RemovePlayer(this);
    }
  }

  public void OnArrive() {
    isJumping = false;
  }

  public void BindInputButton(KeyCode code) {
    buttonCode = code;
    buttonName = AllButtons.namesByValue[code];
    if (OnRebind != null) OnRebind(code);
  }

  public void UnbindInputButton() {
    unregisterButton(buttonCode);
    buttonCode = KeyCode.None;
    buttonName = "";
  }

  public void StartInputRebind() {
    unregisterButton(buttonCode);
    buttonCode = KeyCode.None;
    isRebinding = true;
    BroadcastMessage("RandomizeSprite", SendMessageOptions.DontRequireReceiver);
  }

  public void StopInputRebind() {
    isRebinding = false;
  }

  public void SetDirection(Vector3 newDirection) {
    direction = newDirection;
    transform.up = direction;
  }

  public void SetTravelDist(float dist) {
    travelLength = dist;
    travelPct = 0f;
  }

  public void Travel(float dist) {
    travelPct += dist / travelLength;
  }

  public bool StartedJump() {
    return startedJump;
  }

  public bool IsJumping() {
    return isJumping;
  }
}


