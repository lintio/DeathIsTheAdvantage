using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateManager : MonoBehaviour
{
    public List<GameObject> possessableObjects = new List<GameObject>();
    public float interactRange = 1f;

    public Rigidbody2D _rb2d;
    public BoxCollider2D _boxCollider2D;

    public GameObject playerBody;
    public GameObject tempBody;

    public TMP_Text hintText;

    public MovementController movementController;

    PlayerBaseState currentState;
    public PlayerLivingState LivingState = new PlayerLivingState();
    public PlayerGhostState GhostState = new PlayerGhostState();
    public PlayerPossesingState PossesingState = new PlayerPossesingState();

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        // get all children of possesable objects
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = LivingState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void AddBody()
    {
        tempBody =  Instantiate(playerBody, transform.position, Quaternion.identity);
    }

    public void RemoveBody()
    {
        if (tempBody != null)
        {
            Destroy(tempBody);
        }
    }

    public GameObject FindPossesableObjectInRange()
    {
        // should pass game manager the players pos then have all these checks on there
        foreach (GameObject item in possessableObjects)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance <= interactRange)
            {
                return item;
            }
        }
        return null;
    }
}
