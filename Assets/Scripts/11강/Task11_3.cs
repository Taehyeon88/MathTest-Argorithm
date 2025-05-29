using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Task11_3 : MonoBehaviour  //플레이어 시스템
{
    public GameObject bulletPrefab;
    public GameObject target;

    public float skillCoolTime = 3f;
    public static float currentSkillCoolTime = 3f;
    public int attackCount = 10;

    private int currentAttackCount = 0;
    private Task11_2 camera;
    private Task11_4 UIManager;
    private bool isAttacking = false;
    private bool isCooltime = false;


    void Start()
    {
        camera = FindObjectOfType<Task11_2>();
        UIManager = FindObjectOfType<Task11_4>();

        currentSkillCoolTime = skillCoolTime;
        currentAttackCount = attackCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isCooltime)
            {
                UIManager.ShowTimer();
                StartCoroutine(UseSkill());
                isCooltime = true;
                isAttacking = true;
            }
        }

        if (isCooltime)  //현재 스킬사용 중
        {
            if(isAttacking) camera.RotateCamera(target);

            currentSkillCoolTime = Mathf.Clamp(currentSkillCoolTime - Time.deltaTime, 0f, skillCoolTime);
            UIManager.ShowTimer();
            
            if (currentSkillCoolTime <= 0)
            {
                UIManager.ResetTimer();
                currentSkillCoolTime = skillCoolTime;
                isCooltime = false;
            }
        }

        if (currentAttackCount < 1 && isAttacking)
        {
            StartCoroutine(ResetCamera());
            currentAttackCount = attackCount;
            isAttacking = false;
        }
    }

    private void Attack()
    {
        Vector3 genPos = transform.position + transform.forward * 2.5f;
        GameObject bullet = Instantiate(bulletPrefab, genPos, Quaternion.identity);
        Task11_1 bulletController = bullet.GetComponent<Task11_1>();
        bulletController.p0 = genPos;
        bulletController.p3 = target.transform;
        bulletController.target = target;

        currentAttackCount--;
    }

    private IEnumerator UseSkill()
    {
        int count = attackCount - 1;
        float[] randoms = new float[count];
        for (int i = 0; i < count; i++)
        {
            randoms[i] = UnityEngine.Random.Range(0, 0.2f);
            randoms[i] = Mathf.Min(randoms[i], 0.1f);
        }

        Attack();
        foreach (float value in randoms)
        {
            yield return new WaitForSeconds(value);
            Attack();
        }
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(0.8f);   //bullet 발사후, 적에게 도착하는 시간
        camera.ResetCamera();
    }
}
