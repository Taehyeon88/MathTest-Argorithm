using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task6_1 : MonoBehaviour
{
    [Header("PlayerState")]
    public int level = 1;
    public float attackValue = 20;
    public Weapon currentWeapon;
    [SerializeField] Button levelUpButton;
    [SerializeField] Button attackButton;

    [Header("WeaponVariable")]
    [SerializeField] Button daggerButton;
    [SerializeField] Button longSwordButton;
    [SerializeField] Button axButton;

    [Header("AttackUI")]
    private int attackCount = 0;
    private bool isCritical = false;
    private int successCount = 0;
    private float percent;

    //내부 변수
    private List<Weapon> weapons = new List<Weapon>();
    public delegate void AttackDelegate(float attackValue);
    public event AttackDelegate attackDelegate;
    
    void Start()
    {
        Weapon dagger = new Dagger();
        Weapon longSword = new LongSword();
        Weapon ax = new Ax();

        weapons.Add(dagger);
        weapons.Add(longSword);
        weapons.Add(ax);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapon();
        //levelUpButton.onClick.AddListener(LevelUp);
        //attackButton.onClick.AddListener(Attack);
    }

    public void LevelUp()
    {
        level++;
        attackValue = 20 * level;
    }

    void ChangeWeapon()
    {
        daggerButton.onClick.AddListener(() =>currentWeapon = weapons.Find(((x) => x is Dagger)));
        longSwordButton.onClick.AddListener(()=> currentWeapon = weapons.Find((x)=> x is LongSword));
        axButton.onClick.AddListener(() => currentWeapon = weapons.Find((x) => x is Ax));
    }


    public void Attack()
    {
        float attack = attackValue;
        if (currentWeapon != null)
        {
            attack = GenerateGaussian(attackValue, attackCount * currentWeapon.normalRate);
            if (attack < 0) attack = 0;
            if (CheckCritical(currentWeapon.p_critical, currentWeapon.p_critical))
            {
                attack *= currentWeapon.criticalRate;
            }
        }
        attackDelegate.Invoke(attack);
    }

    private bool CheckCritical(int standard, int c_Percent)
    {
        attackCount++;
        int result = Random.Range(0, 101);

        result = CorrectCritical(c_Percent, standard);
        if (result <= standard)
        {
            successCount++;
            isCritical = true;
        }
        else isCritical = false;

        return isCritical;
    }

    private int CorrectCritical(int c_Percent, int standard)
    {
        float preCarculate = (float)(successCount + 1) / (attackCount + 1) * 100f; //치명타보정
        float preCarculate2 = (float)successCount / (attackCount + 1) * 100f;

        if (percent < c_Percent && preCarculate < c_Percent) return standard;
        else if (percent > c_Percent && preCarculate2 > c_Percent) return standard + 1;
        return standard;
    }

    private float GenerateGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value; //0보다 큰 난수
        float u2 = 1.0f - Random.value; //0보다 큰 난수

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                            Mathf.Sin(2.0f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal; //원하는 평균과 표준편차로 변환
    }
}
