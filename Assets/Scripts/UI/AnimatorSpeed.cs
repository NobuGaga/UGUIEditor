using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorSpeed : MonoBehaviour {
     
     public float m_speed = 1;
     public Animator m_animator;
     public string m_stateName;

     private void Awake() {
         if (m_animator == null)
            m_animator = GetComponent<Animator>();
     }

     private void OnEnable() {
         float startTime = m_speed > 0 ? 0 : 1;
         m_animator.SetFloat("Speed", m_speed);
         m_animator.Play(m_stateName, -1, startTime);
     }
}