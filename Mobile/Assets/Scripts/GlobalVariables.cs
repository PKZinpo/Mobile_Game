using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    [SerializeField] private float m_verticalGravity;
    [SerializeField] private float m_horizontalGravity;
    [SerializeField] private float m_maxVerticalVelocity;
    [SerializeField] private float m_startImpulse;

    public float VerticalGravity { get { return m_verticalGravity; } }
    public float HorizontalGravity { get { return m_horizontalGravity; } }
    public float MaxVerticalVelocity { get { return m_maxVerticalVelocity; } }
    public float StartImpulse { get { return m_startImpulse; } }

}
