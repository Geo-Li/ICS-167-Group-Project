
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // Geo Li

// /*
//  * The player controller movement
//  */
// public class PlayerMovement : MonoBehaviour, EntityMovement
// {
//     // Player top speed
//     [SerializeField]
//     private float m_PlayerSpeed = 10f;

//     // Names of all the player inputs
//     [SerializeField]
//     private string m_HorizontalMovement, m_VerticalMovement, m_AttackInput;

//     private float m_CurrentSpeed;

//     private Vector3 startPosition;

//     private AttackConditions[] m_AttackConditions = null;

//     // Start is called before the first frame update
//     void Start()
//     {
//         startPosition = transform.position;

//         Entity e = GetComponent<Entity>();

//         if (e != null)
//             m_AttackConditions = e.GetAttackConditions();
//     }

//     public float GetCurrentSpeed()
//     {
//         return m_CurrentSpeed;
//     }

//     public void UpdateRotation(Vector3 lookingPosition)
//     {
//         if (Mathf.Abs(m_CurrentSpeed) > 0.001)
//             transform.rotation = Quaternion.LookRotation(lookingPosition);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         RespawnFromVoidBorders();
//     }

//     // Checks if user has fallen the table, if so make them go back to the start position
//     private void RespawnFromVoidBorders()
//     {
//         if (transform.position.y < 0)
//             transform.position = startPosition;
//     }

//     void FixedUpdate()
//     {
//         EnactMovement();
//         DoAttack();
//     }

//     private void EnactMovement()
//     {
//         float inputX = Input.GetAxis(m_HorizontalMovement);
//         float inputZ = Input.GetAxis(m_VerticalMovement);

//         Vector3 movementVector = new Vector3(inputX, 0f, inputZ);

//         if (movementVector.magnitude > 1)
//             movementVector.Normalize();

//         movementVector *= m_PlayerSpeed;

//         float newSpeed = movementVector.magnitude;

//         movementVector *= Time.deltaTime;

//         transform.position += movementVector;

//         if (Mathf.Abs(m_CurrentSpeed - newSpeed) >= 0.001)
//             m_CurrentSpeed = newSpeed;

//         UpdateRotation(movementVector);
//     }

//     private void DoAttack()
//     {
//         if (m_AttackConditions == null)
//             return;

//         float attackInput = Input.GetAxis(m_AttackInput);
//         int attackNum = 1;
//         Entity e = GetComponent<Entity>();

//         if (e != null && attackInput >= 1 && m_AttackConditions.Length > 0 && m_AttackConditions[attackNum - 1].IsNotOnCooldown())
//             StartCoroutine(e.StartAttack(1));
//     }
// }
