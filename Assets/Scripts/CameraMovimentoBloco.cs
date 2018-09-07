using UnityEngine;

public class CameraMovimentoBloco : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 m_newPosition = Vector3.zero;
    private Vector3 currPosition;

    private Vector3 viewportPoint = Vector3.zero;
    
    private Camera cam;

    private float camHeight;
    private float camWidth;

    private void Start()
    {
        cam = GetComponent<Camera>();
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        m_newPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, m_newPosition) > 0.01f)
        {
            currPosition = m_newPosition + (Vector3.forward * -10);
            transform.position = Vector3.SmoothDamp(transform.position, currPosition, ref velocity, smoothTime);
        }
        else
        {
            viewportPoint = cam.WorldToViewportPoint(target.position);

            if (viewportPoint.x < 0) //Esquerda
                m_newPosition += Vector3.left * camWidth;

            if (viewportPoint.x > 1) //Direita
                m_newPosition += Vector3.right * camWidth;

            if (viewportPoint.y < 0) //Baixo
                m_newPosition += Vector3.down * camHeight;

            if (viewportPoint.y > 1) //Cima
                m_newPosition += Vector3.up * camHeight;
        }
    }
}
