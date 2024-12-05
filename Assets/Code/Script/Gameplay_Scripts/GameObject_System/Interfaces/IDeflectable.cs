using UnityEngine;
 
public interface IDeflectable
{
    public void Deflect(Vector3 dir, float strenght);
    public Rigidbody Rigidbody { get; }
}
