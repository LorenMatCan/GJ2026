using UnityEngine;

public class PaloBalancin : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;

    void LateUpdate()
    {

        Vector3 medio = (puntoA.position + puntoB.position) / 2f;
        transform.position = medio;

        Vector3 dir = puntoB.position - puntoA.position;

        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        
    }
}
