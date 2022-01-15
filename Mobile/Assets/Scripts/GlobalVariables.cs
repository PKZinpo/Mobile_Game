using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    [SerializeField] private float m_verticalGravity;
    [SerializeField] private float m_horizontalGravity;

    public float VerticalGravity { get { return m_verticalGravity; } }
    public float HorizontalGravity { get { return m_horizontalGravity; } }

}
