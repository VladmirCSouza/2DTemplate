using UnityEngine;
using UnityEngine.Events;
public class MudaBlocoFase : MonoBehaviour {

    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3> { }
    public Vector3Event OnChangeBlockLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlocoFase"))
        {
            OnChangeBlockLevel.Invoke(collision.transform.position);
        }
    }
}
