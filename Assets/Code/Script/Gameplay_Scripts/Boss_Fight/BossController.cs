using Audio_System.SFX;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour, IDamageable
{
    private int i = 0;
    private float _attack;
    private int _phaseHp;
    [SerializeField]private int _hp = 0;
    public int HP=>_hp;

    public List<ObjectPooler> Pooler=new List<ObjectPooler>();
    public BossView BossView;
    public BossStateMachine StateMachine;
    public List<GameObject> GameObject;
    public GameObject ObjectThrown;
    public List<Phase_So> PhaseSo = new List<Phase_So>();
    public Action OnBossDefeat;
    public Action OnBossFightStart;
    public Action<int> OnBossHealthChange;

    [SerializeField] private SFX_SO _bossDamageSfx;
    [SerializeField] private SFX_SO _bossAttackSfx;
    [SerializeField] private SFX_SO _bossDeathSfx;
    public SFX BossDamage => GetSFX(_bossDamageSfx);
    public SFX BossAttack => GetSFX(_bossAttackSfx);
    public SFX BossDeath => GetSFX(_bossDeathSfx);

    private SFX GetSFX(SFX_SO sfxSO)
    {
        if (sfxSO != null)
            return sfxSO.GetSFX();
        return null;
    }
    public void Damage()
    {
        BossView.Animator.SetTrigger("Damage");
        SFXManager.PlaySFX?.Invoke(BossDamage, this.transform.position);
        OnBossHealthChange?.Invoke(_hp);
        _hp--;
        _phaseHp--;
        if (_phaseHp == 0 && _hp != 0)
        {
            i++;
            _phaseHp = PhaseSo[i].Trigger;
        }
        if (_hp==0)
        {
            SFXManager.PlaySFX?.Invoke(BossDeath, this.transform.position);
            BossView.Animator.SetTrigger("Death");
            StateMachine = new BossStateMachine(new BossDeath_State());
            OnBossDefeat?.Invoke();
            Debug.Log("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BossProjectile>().Deflected)
        {            
            Damage();
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<BossProjectile>().Deflected = false;
        }
    }

    private void OnEnable()
    {
        OnBossFightStart?.Invoke();
        for (int i = 0; i < GameObject.Count; i++)
        {
            Pooler.Add(new ObjectPooler(GameObject[i], 2));
        }
        _hp=0;
        for (int i = 0; i < PhaseSo.Count; i++)
        {
            _hp = _hp + PhaseSo[i].Trigger;
        }
        _phaseHp = PhaseSo[i].Trigger;
        StartCoroutine(Attack());
    }
    public PlayerController Player()
    {
        return FindObjectOfType<PlayerController>();
    }
    public Vector3 PlayerPos()
    {
        return FindObjectOfType<PlayerController>().transform.position;
    }
    public GameObject SpawnObject()
    {
        int i;
        i=UnityEngine.Random.Range(0, Pooler.Count);
        Pooler[i].PoolObject().transform.position= new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        return Pooler[i].PoolObject();
    }

    public IEnumerator DisplayWarning()
    {
        BossView.WarningSprite.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1 / 2f);
        BossView.Tentacle.transform.position = PlayerPos() + Vector3.up * 10;
        yield return new WaitForSeconds(PhaseSo[i].WarningDelay);
        BossView.WarningSprite.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
        BossView.Tentacle.transform.GetComponent<Rigidbody>().velocity = Vector3.down*PhaseSo[i].TentacleSpeed;
        BossView.Tentacle.SetActive(true);
    }

    public IEnumerator Attack()
    {        
        _attack = UnityEngine.Random.Range(0f, 100f);
        Debug.Log(_attack);
        if (_attack <= PhaseSo[i].Attack1Percentage)
        {
            StateMachine = new BossStateMachine(new BossAttack1_State(this, i,BossView));
        }
        else
        {
            StateMachine = new BossStateMachine(new BossAttack2_State(this,i,BossView));
        }
        yield return new WaitForSeconds(PhaseSo[i].DelayBetweenAttacks);
        StateMachine.Process();      
    }

}    

