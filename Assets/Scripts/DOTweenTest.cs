using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour {

    [SerializeField]
    private RectTransform m_rectTrans;
    private List<Tweener> m_listTween = new List<Tweener>();
    private ushort count = 0;

    void Awake() {
        if (m_rectTrans == null)
            m_rectTrans = GetComponent<RectTransform>();
    }

    void OnEnable() {
        if (count % 2 == 0) {
            m_listTween.ForEach((Tweener tweener) => tweener.PlayForward());
            count++;
        }
        else {
            m_listTween.ForEach((Tweener tweener) => tweener.PlayBackwards());
            count--;
        }
    }

    void Start() {
        m_listTween.Add(m_rectTrans.DOAnchorPosX(100, 1));
        m_listTween.Add(m_rectTrans.DOScaleX(0.5f, 1));
        m_listTween.Add(m_rectTrans.DOLocalRotate(new Vector3(180, 0), 1));
        m_listTween.ForEach((Tweener tweener) => tweener.SetAutoKill(false));
    }

    private void KillLog() => Debug.Log("KillLog");
}