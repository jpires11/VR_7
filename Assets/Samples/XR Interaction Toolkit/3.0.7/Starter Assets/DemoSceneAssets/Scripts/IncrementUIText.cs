using UnityEngine.UI;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Add this component to a GameObject and call the <see cref="IncrementText"/> method
    /// in response to a Unity Event to update a text display to count up with each event.
    /// </summary>
    public class IncrementUIText : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Text component this behavior uses to display the incremented value.")]
        Text m_Text;

        [SerializeField]
        [Tooltip("计数为1时要显示的目标对象")]
        GameObject m_TargetObject;

        /// <summary>
        /// The Text component this behavior uses to display the incremented value.
        /// </summary>
        public Text text
        {
            get => m_Text;
            set => m_Text = value;
        }

        int m_Count;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Awake()
        {
            if (m_Text == null)
                Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
        }

        /// <summary>
        /// Increment the string message of the Text component.
        /// </summary>
        public void IncrementText()
        {
            m_Count += 1;
            Debug.Log($"计数等于{m_Count}");
            if (m_Count == 1)
            {
                if (m_TargetObject != null)
                    m_TargetObject.SetActive(true);
                else
                    Debug.Log("没有设置目标对象");
            }
            if (m_Text != null)
                m_Text.text = m_Count.ToString();
        }
    }
}
