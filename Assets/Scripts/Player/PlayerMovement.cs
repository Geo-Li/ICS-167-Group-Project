

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Geo Li

/*
 * The player controller movement
 */ 
public class PlayerMovement : MonoBehaviour, EntityMovement
{
    // Player top speed
    [SerializeField]
    private float m_PlayerSpeed = 10f;

    // Determines if the player is guided by WASD or Arrow Keys
    [SerializeField] 
    private string m_WhichKeyboardController;

    // Checks if the player is summoned on local multiplayer
    [SerializeField]
    private bool m_IsOnLocalMultiplayer;

    // List of attacks of the player
    [SerializeField]
    private GameObject[] m_AttackObjects;

    // Camera used by the player
    [SerializeField]
    private Camera m_PlayerCamera;

    private Quaternion mouseWorldRotation;

    private const string HORIZONTAL_WASD = "Horizontal_WASD", VERTICAL_WASD = "Vertical_WASD", 
                        HORIZONTAL_ARROWS = "Horizontal_Arrows", VERTICAL_ARROWS = "Vertical_Arrows",
                        ATTACK_GENERAL = "Attack",
                        ATTACK_WASD = "Attack_WASD", ATTACK_ARROWS = "Attack_Arrows";

    private const string WASD_NAME = "WASD", ARROWS_NAME = "Arrows";

    // Names of the all player inputs
    private string m_HorizontalInput, m_VerticalInput, m_AttackInput;

    private float m_CurrentSpeed;

    private Vector3 startPosition;

    private AttackConditions[] m_AttackConditions = null;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        Entity e = GetComponent<Entity>();

        if (e != null)
            m_AttackConditions = e.GetAttackConditions();

        UpdateInputs();

        if (m_PlayerCamera == null)
            m_PlayerCamera = Camera.main;
    }

    public float GetCurrentSpeed()
    {
        return m_CurrentSpeed;
    }

    public void UpdateRotation(Vector3 lookingPosition)
    {
        if (Mathf.Abs(m_CurrentSpeed) > 0.001)
            transform.rotation = Quaternion.LookRotation(lookingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        RespawnFromVoidBorders();
    }

    // Checks if user has fallen the table, if so make them go back to the start position
    private void RespawnFromVoidBorders()
    {
        if (transform.position.y < 0)
            transform.position = startPosition;
    }

    void FixedUpdate()
    {
        EnactMovement();
        DoAttack();
    }

    // Updates player inputs
    public void UpdateInputs()
    {
        if (m_IsOnLocalMultiplayer)
        {
            if (m_WhichKeyboardController == ARROWS_NAME)
            {
                m_HorizontalInput = HORIZONTAL_ARROWS;
                m_VerticalInput = VERTICAL_ARROWS;
                m_AttackInput = ATTACK_ARROWS;
            }
            else if (m_WhichKeyboardController == WASD_NAME)
            {
                m_HorizontalInput = HORIZONTAL_WASD;
                m_VerticalInput = VERTICAL_WASD;
                m_AttackInput = ATTACK_WASD;
            }
        }
        else
        {
            m_HorizontalInput = HORIZONTAL_WASD;
            m_VerticalInput = VERTICAL_WASD;
            m_AttackInput = ATTACK_GENERAL;
        }  
    }

    private void EnactMovement()
    {
        float inputX = Input.GetAxis(m_HorizontalInput);
        float inputZ = Input.GetAxis(m_VerticalInput);

        Vector3 movementVector = new Vector3(inputX, 0f, inputZ);

        if (movementVector.magnitude > 1)
            movementVector.Normalize();

        movementVector *= m_PlayerSpeed;

        float newSpeed = movementVector.magnitude;

        movementVector *= Time.deltaTime;

        transform.position += movementVector;

        if (Mathf.Abs(m_CurrentSpeed - newSpeed) >= 0.001)
            m_CurrentSpeed = newSpeed;

        UpdateRotation(movementVector);
    }

    private void DoAttack()
    {
        if (m_AttackConditions == null)
            return;

        float attackInput = Input.GetAxis(m_AttackInput);
        int attackNum = 1;
        int attackNumIndex = attackNum - 1;
        Entity e = GetComponent<Entity>();
        bool IsNotOnCooldown = m_AttackConditions[attackNumIndex].IsNotOnCooldown();

        if (e != null && attackInput >= 1 && m_AttackConditions.Length > 0 && IsNotOnCooldown)
        {
            if (!m_IsOnLocalMultiplayer)
            {
                Vector3 mouseWorldPosition = Vector3.zero;

                Vector3 mousePosition = Input.mousePosition;
                var ray = m_PlayerCamera.ScreenPointToRay(mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                    mouseWorldPosition = hit.point;
                mouseWorldPosition.y = transform.position.y;

                mouseWorldRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position);
            }
            
            e.StartAttack(attackNum);
        }

        if (!m_IsOnLocalMultiplayer && mouseWorldRotation != null && !IsNotOnCooldown)
        {
            m_AttackObjects[attackNumIndex].transform.rotation = RotationWorldToLocal(mouseWorldRotation, m_AttackObjects[attackNumIndex]);
        }
    }

    private Quaternion RotationWorldToLocal(Quaternion worldRotation, GameObject target)
    {
        Transform targetTransform = target.transform;

        Quaternion rotOffset = targetTransform.rotation * Quaternion.Inverse(targetTransform.localRotation);
        Quaternion rotWorld = mouseWorldRotation * rotOffset;
        return Quaternion.Inverse(rotOffset) * rotWorld;
    }
}