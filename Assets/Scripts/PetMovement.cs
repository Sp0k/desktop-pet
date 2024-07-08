using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
  // VARIABLES //
  [SerializeField] private float _horizontalSpeed = 3.5f;
  [SerializeField] private float _verticalSpeed = 3.5f;
  [SerializeField] private float _horizontalLimit = 8.0f;
  [SerializeField] private float _limitUp = 4.22f;
  [SerializeField] private float _limitDown = -4.0f;

  private bool isWandering = false;
  private bool isWalking = false;
  private int xDirection;
  private int yDirection;

  // Start is called before the first frame update
  void Start()
  {
    // Set initial position
    transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    // Start wandering is not doing so already
    if (!isWandering)
    {
      StartCoroutine(Wander());
    }

    // Check if currently walking
    if (isWalking)
    {
      /* Horizontal movement
       *
       * -1 => move left
       *  1 => move right
       *  0 => no movement
       */
      if (xDirection < 0 && transform.position.x > -_horizontalLimit)
      {
        transform.Translate(Vector3.left * _horizontalSpeed * Time.deltaTime);
      }
      else if (xDirection > 0 && transform.position.x < _horizontalLimit)
      {
        transform.Translate(Vector3.right * _horizontalSpeed * Time.deltaTime);
      }

      /* Vertical movement
       *
       * -1 => move down
       *  1 => move up
       *  0 => don't move
       */
      if (yDirection < 0 && transform.position.y > _limitDown) {
        transform.Translate(Vector3.down * _verticalSpeed * Time.deltaTime);
      }
      else if (yDirection > 0 && transform.position.y < _limitUp) {
        transform.Translate(Vector3.up * _verticalSpeed * Time.deltaTime);
      }
    }
  }

  // Wandering script
  IEnumerator Wander()
  {
    // Setting directions and speeds
    xDirection = Random.Range(-1, 2);
    yDirection = Random.Range(-1, 2);
    _horizontalSpeed = Random.Range(2.0f, 3.5f);
    _verticalSpeed = Random.Range(2.0f, 3.5f);

    // Setting timers
    int idleTime = Random.Range(1, 3);
    int walkTime = Random.Range(1, 3);

    // Start the wandering process and an idle timer
    isWandering = true;
    yield return new WaitForSeconds(idleTime);

    // Start walking in the chosen direction
    isWalking = true;
    yield return new WaitForSeconds(walkTime);

    // Finish walking
    isWalking = false;

    // Finish wandering
    isWandering = false;
  }
}
