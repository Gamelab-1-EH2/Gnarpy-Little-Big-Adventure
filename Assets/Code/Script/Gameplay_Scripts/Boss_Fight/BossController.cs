using Player.Behaviour.Machine;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{

    Animator _animator;
    [SerializeField] private Phase_So _phaseSo;
    [SerializeField]int _currentHp;
    private BossStateMachine _stateMachine;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        //_currentHp = _phaseSo;
        _stateMachine= new BossStateMachine(new BossIdle_State());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Damage();
        }
    }

    public void Damage()
    {
        _currentHp--;
        _animator.SetTrigger("Damage");
    }

    private void OnEnable()
    {
        ///lista di scriptable per gestire le varie fasi del boss
        int attack = Random.Range(0, 1);
        if (attack == 1)
        {
            _stateMachine = new BossStateMachine(new BossAttack1_State());
        }
        else
        {

        }
    }

}
