using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform m_target;
    public CharacterController2D player;
    public float m_smoothTime = 0.3F;
    public float m_distanciaY = 2f;
    public float m_distanciaX = 2.5f;
    public bool snapRight = true;
    private float m_snapPosition = 0f;

    private Vector3 m_velocity = Vector3.zero;

    private float m_targetPostionY = 0f;
    private float m_targetPositionX = 0f;
    private Vector3 m_targetPosition;

    private bool m_targetFalling = false;
    
    // Use this for initialization
    void Start () {
        m_targetPositionX = m_target.position.x;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_targetFalling)
            m_targetPostionY = m_target.position.y;

        if(m_target.position.y < transform.position.y - 1.5f)
            m_targetPostionY = m_target.position.y;

        if (snapRight)
        {
            if (player.FacingRight())
            {
                m_snapPosition = m_distanciaX;
                m_targetPositionX = m_target.position.x;
            }
            else
            {
                if(m_target.position.x < ((transform.position.x - m_snapPosition) - 2))
                {
                    snapRight = false;
                }
            }
        }
        else
        {
            if (!player.FacingRight())
            {
                m_snapPosition = m_distanciaX * -1;
                m_targetPositionX = m_target.position.x;
            }
            else
            {
                if (m_target.position.x > ((transform.position.x - m_snapPosition) + 2))
                {
                    snapRight = true;
                }
            }
        }

        // Define a target position above and behind the target transform
        //m_targetPosition = m_target.TransformPoint(new Vector3(0, 2, -10));
        m_targetPosition = new Vector3(m_targetPositionX + m_snapPosition, m_targetPostionY + m_distanciaY, -10f);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, m_targetPosition, ref m_velocity, m_smoothTime);
    }

    /// <summary>
    /// Evento chamado quando jogador inicia queda. Evento NÃO está vinculado ao pulo
    /// </summary>
    public void OnPlayerFall()
    {
        m_targetFalling = true;
    }

    /// <summary>
    /// Evento chamado quando o jogador toca o chão
    /// </summary>
    public void OnPlayerLand()
    {
        m_targetPostionY = m_target.position.y;
        m_targetFalling = false;
    }
}
