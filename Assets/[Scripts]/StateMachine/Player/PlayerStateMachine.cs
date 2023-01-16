using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field : SerializeField]public InputReader InputReader { get; private set; }
    
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float FreeLookDashMovementSpeed { get; private set; }

    [field: SerializeField] public float TragetingMovementSpeed { get; private set; }    

    [field: SerializeField] public float RotationDamping { get; private set; }

    [field: SerializeField] public float DodgeDuration { get; private set; }

    [field: SerializeField] public float DodgeLength { get; private set; }

    [field: SerializeField] public float DodgeCooldown { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }

    [field: SerializeField] public bool isSword { get; private set; }

    [field: SerializeField] public bool isShield { get; private set; }

    [field: SerializeField] public int swordDurability { get; set; }

    [field: SerializeField] public int shieldDurability { get; set; }

    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public Targeter Targeter { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public GameObject Sword { get; private set; }

    [field: SerializeField] public GameObject Shield { get; private set; }


    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    public Transform MainCameraTransform  { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        var manager = GameObject.FindObjectOfType<ChangeGameManager>();
        isSword = manager.isWeapon;
        isShield = manager.isShield;
        if (isSword)
        {
            Sword.SetActive(true);
            swordDurability = manager.swordDurability;
        }
        if (isShield)
        {
            Shield.SetActive(true);
            shieldDurability = manager.shieldDurability;
        }


        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpackState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void SetDodgeTime(float dodgeTime)
    {
        PreviousDodgeTime = dodgeTime;
    }
}