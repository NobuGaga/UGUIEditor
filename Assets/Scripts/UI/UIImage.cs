using UnityEngine;
using UnityEngine.UI;

public class UIImage:Image {

    private float m_width;
    private float m_height;

    protected override void Awake() {
        m_width = Size.x;
        m_height = Size.y;
    }

    private Vector2 Size {
        set => GetComponent<RectTransform>().sizeDelta = value;
        get => GetComponent<RectTransform>().sizeDelta;
    }

    public float fillAmountX {
        set {
            switch (type) {
                case Type.Sliced:
                    if (value < 0)
                        return;
                    Vector2 size = Size;
                    size.x = m_width * value;
                    Size = size;
                    break;
                default:
                    fillAmount = value;
                    break;
            }
        }
    }

    public float fillAmountY {
        set {
            switch (type) {
                case Type.Sliced:
                    if (value < 0)
                        return;
                    Vector2 size = Size;
                    size.y = m_height * value;
                    Size = size;
                    break;
                default:
                    fillAmount = value;
                    break;
            }
        }
    }
}