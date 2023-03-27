using UnityEngine;
using System.Collections;

public class EnemyTankShooting : MonoBehaviour
{

    public Rigidbody m_Shell;               // Prefab of the Shell
    public Transform m_FireTransform;       // A child of the tank where the shells are spawned

    public float m_LaunchForce = 30f;       // The force given to the shell when firing
    public float m_ShootDelay = 1f;

    private bool m_CanShoot;
    private float m_ShootTimer;


    private void Awake()
    {
        m_CanShoot = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = Time.deltaTime;
                Fire();
            }
        }
    }

    private void Fire()
    {
        // Create an instance of the shell and store a reference to it's rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CanShoot = true;
        m_ShootTimer = m_ShootDelay;
    }

    private void OnTriggerExit(Collider other)
    {
        m_CanShoot = false;
    }
}
