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
    for (var i = 0; i < pressedInputs.Count; i++) {
      if (registerButton(pressedInputs[i])) {
        SetInputButton(pressedInputs[i]);
        StopInputRebind();
        break;
      }
    }
  }

  public void OnArrive() {
    isJumping = false;
  }

  public void SetInputButton(KeyCode code) {
    buttonCode = code;
    buttonName = AllButtons.namesByValue[code];
    if (OnRebind != null) OnRebind(code);
  }

  public void StartInputRebind() {
    unregisterButton(buttonCode);
    buttonCode = KeyCode.None;
    isRebinding = true;
    BroadcastMessage("RandomizeSprite", SendMessageOptions.DontRequireReceiver);
    UpdateRebind();
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


