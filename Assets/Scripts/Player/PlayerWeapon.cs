using System;
using System.Collections;
using NUnit.Framework.Internal.Builders;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : CharacterWeapon
{
    public static event Action<Weapon> OnWeaponUIUpdateEvent;
    
    [Header("Player")]
    [SerializeField] private PlayerConfig config;
    
    private int weaponIndex; // 0 - 1
    private Weapon[] equippedWeapons = new Weapon[2];
    
    private PlayerActions actions;
    private PlayerEnergy playerEnergy;
    private PlayerDetection detection;
    private PlayerMovement playerMovement;
    
    private Coroutine weaponCoroutine;
    private ItemText weaponNameText;
    
    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
        detection = GetComponentInChildren<PlayerDetection>();
        playerEnergy = GetComponent<PlayerEnergy>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        actions.Weapon.Shoot.performed += ctx => ShootWeapon();
        actions.Interactions.ChangeWeapons.performed += ctx => ChangeWeapon();
    }

    private void Update()
    {
        if (currentWeapon == null) return;
        RotatePlayerWeapon();
    }

    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position,
            Quaternion.identity, weaponPos);
        equippedWeapons[weaponIndex] = currentWeapon;
        equippedWeapons[weaponIndex].CharacterParent = this;
        ShowCurrentWeaponName();
        OnWeaponUIUpdateEvent?.Invoke(currentWeapon);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapons[0] == null)
        {
            CreateWeapon(weapon);
            return;
        }

        if (equippedWeapons[1] == null)
        {
            weaponIndex++;
            equippedWeapons[0].gameObject.SetActive(false);
            CreateWeapon(weapon);
            return;
        }
        
        // Destroy current weapon
        currentWeapon.DestroyWeapon();
        equippedWeapons[weaponIndex] = null;
        
        // Create new weapon
        CreateWeapon(weapon);
    }

    private void ChangeWeapon()
    {
        if (equippedWeapons[1] == null) return;
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            equippedWeapons[i].gameObject.SetActive(false);
        }

        weaponIndex = 1 - weaponIndex;
        currentWeapon = equippedWeapons[weaponIndex];
        currentWeapon.gameObject.SetActive(true);
        ResetWeaponForChange();
        ShowCurrentWeaponName();
        OnWeaponUIUpdateEvent?.Invoke(currentWeapon);
    }

    private void RotatePlayerWeapon()
    {
        if (playerMovement.MoveDirection != Vector2.zero)
        {
            RotateWeapon(playerMovement.MoveDirection);
        }

        if (detection.EnemyTarget != null)
        {
            Vector3 dirToEnemy = detection.EnemyTarget.transform.position -
                                 transform.position;
            RotateWeapon(dirToEnemy);
        }
    }
    
    private void ShootWeapon()
    {
        if (currentWeapon == null)
        {
            return;
        }

        if (CanUseWeapon() == false)
        {
            return;
        }
        
        currentWeapon.UseWeapon();
        playerEnergy.UseEnergy(currentWeapon.ItemWeapon.RequiredEnergy);
    }

    public float GetDamageUsingCricitalChance()
    {
        float damage = currentWeapon.ItemWeapon.Damage;
        float porc = Random.Range(0f, 100f);
        if (porc < config.CriticalChance)
        {
            damage = damage * (config.CriticalDamage / 100f) + damage;
            return damage;
        }

        return damage;
    }
    
    private void ShowCurrentWeaponName()
    {
        if (weaponCoroutine != null)
        {
            StopCoroutine(weaponCoroutine);
        }

        if (weaponNameText != null && weaponNameText.gameObject.activeInHierarchy)
        {
            Destroy(weaponNameText.gameObject);
        }

        weaponCoroutine = StartCoroutine(IEShowName());
    }
    
    private IEnumerator IEShowName()
    {
        Vector3 textPos = transform.position + Vector3.up;
        Color weaponNameColor = GameManager.Instance.
            GetWeaponNameColor(currentWeapon.ItemWeapon.Rarity);
        weaponNameText = ItemTextManager.Instance
            .ShowMessage(currentWeapon.ItemWeapon.ID, weaponNameColor, 
                textPos);
        weaponNameText.transform.SetParent(transform);
        yield return new WaitForSeconds(2f);
        Destroy(weaponNameText.gameObject);
    }
    
    private bool CanUseWeapon()
    {
        if (currentWeapon.ItemWeapon.WeaponType == WeaponType.Gun && 
            playerEnergy.CanUseEnergy)
        {
            return true;
        }

        if (currentWeapon.ItemWeapon.WeaponType == WeaponType.Melee)
        {
            return true;
        }

        return false;
    }

    private void ResetWeaponForChange()
    {
        Transform weaponTransform = currentWeapon.transform;
        weaponTransform.rotation = Quaternion.identity;
        weaponTransform.localScale = Vector3.one;
        weaponPos.rotation = Quaternion.identity;
        weaponPos.localScale = Vector3.one;
        playerMovement.FaceRightDirection();
    }
    
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
