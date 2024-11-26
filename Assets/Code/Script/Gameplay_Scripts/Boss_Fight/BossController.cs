using Player.Behaviour.Machine;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    Animator animator;
    
    [SerializeField] private Boss_So _bossSo;
    int _currentHp;
    private BossStateMachine _stateMachine;
    LayerMask _layerMask;

    public void Damage()
    {
        _currentHp--;
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHp = _bossSo.Hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerMask)
        {
            Damage();
        }
    }

}
