using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    [SerializeField] private float m_verticalGravity;
    [SerializeField] private float m_horizontalAcceleration;
    [SerializeField] private float m_maxhorizontalVelocity;
    [SerializeField] private float m_maxVerticalVelocity;
    [SerializeField] private float m_startImpulse;
    [SerializeField] private float m_objectImpulse;

    public float VerticalGravity { get { return m_verticalGravity; } }
    public float HorizontalAcceleration { get { return m_horizontalAcceleration; } }
    public float MaxHorizontalVelocity { get { return m_maxhorizontalVelocity; } }
    public float MaxVerticalVelocity { get { return m_maxVerticalVelocity; } }
    public float StartImpulse { get { return m_startImpulse; } }
    public float ObjectImpulse { get { return m_objectImpulse; } }

}
