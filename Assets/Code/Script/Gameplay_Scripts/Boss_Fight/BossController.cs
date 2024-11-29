using Player;
using Player.Behaviour.Machine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.Rendering.DebugUI;

public class BossController : MonoBehaviour, IDamageable
{
    private int i=0;
    private int _attack;
    private int _phaseHp;
    [SerializeField] private int _hp=0;

    public Animator _animator;
    public BossStateMachine _stateMachine;
    public List<GameObject> _gameObject;
    public GameObject objectThrown;
    public Vector3 playerTransform;
    public Transform Test;
    public List<Phase_So> phase_So = new List<Phase_So>();

    void Awake()
    {
        _animator = GetComponent<Animator>();
        for (int i=0;i<phase_So.Count;i++)
        {
            _hp = _hp + phase_So[i].Trigger;
        }
    }

    public void Damage()
    {
        _hp--;
        _phaseHp--;
        if (_phaseHp == 0 && _hp!=0)
        {
            i++;
            _phaseHp = phase_So[i].Trigger;
        }
        else
        {
            _stateMachine = new BossStateMachine(new BossDeath_State());
        }
        _animator.SetTrigger("Damage");
    }

    private void OnEnable()
    {
        _phaseHp = phase_So[i].Trigger;
        StartCoroutine(Cooldown());
    }

    public GameObject SpawnObject()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform.position;
        return Instantiate(_gameObject[_gameObject.Count - 1], Test.position, Quaternion.identity);
    }



    public IEnumerator Cooldown()
    {
        _attack = Random.Range(0, 2);
        if (_attack == 0)
        {
            _stateMachine = new BossStateMachine(new BossAttack1_State(this,i));
        }
        else
        {
            _stateMachine = new BossStateMachine(new BossAttack2_State(this,i));
        }
        yield return new WaitForSeconds(phase_So[i].DelayBetweenAttacks);
        _stateMachine.Process();      
    }

}    

