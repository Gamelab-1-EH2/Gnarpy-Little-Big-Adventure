using Player;
using Player.Behaviour.Machine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    public int i;
    public Animator _animator;
    public int _hp;
    public BossStateMachine _stateMachine;
    public List<GameObject> _gameObject;
    public GameObject objectThrown;
    public Vector3 playerTransform;
    public Transform Test;
    int attack;
    public List<Phase_So> phase_So = new List<Phase_So>();

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine= new BossStateMachine(new BossIdle_State());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8&& other.gameObject.GetComponent<MovableObject>().IsDeflected)
        {
            Damage();
        }
    }

    public void Damage()
    {
        _hp--;
        _animator.SetTrigger("Damage");
    }

    private void OnEnable()
    {
        StartCoroutine(Cooldown());
    }

    public GameObject SpawnObject()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform.position;
        return Instantiate(_gameObject[_gameObject.Count-1], Test.position,Quaternion.identity);
    }



    public IEnumerator Cooldown()
    {
        int i = 0;
        attack = Random.Range(0, 2);
        if (attack == 0)
        {
            _stateMachine = new BossStateMachine(new BossAttack1_State(this));
        }
        else
        {
            _stateMachine = new BossStateMachine(new BossAttack2_State(this));
        }
        yield return new WaitForSeconds(phase_So[i].DelayBetweenAttacks);
        _stateMachine.Process();
    }

}
