using UnityEngine;
using UnityEngine.UI;

public class UIImage:Image {

    private float m_difWidth;
    private float m_difHeight;

    protected override void Awake() {
        m_difWidth = Size.x - sprite.textureRect.width;
        m_difHeight = Size.y - sprite.textureRect.height;
    }

    private Vector2 Size {
        set => GetComponent<RectTransform>().sizeDelta = value;
        get => GetComponent<RectTransform>().sizeDelta;
    }

    private void SetSliceFill(bool isHorizontal, bool isVertical, float amount) {
        Vector2 size = Size;
        if (isHorizontal)
            size.x = sprite.textureRect.width + m_difWidth * amount;
        if (isVertical)
            size.y = sprite.textureRect.height + m_difHeight * amount;
        Size = size;
    }

    public new float fillAmount {
        set {
            if (type == Type.Sliced)
                SetSliceFill(true, true, value);
            else
                base.fillAmount = value;
        }
        get => base.fillAmount;
    }

    public float fillAmountX {
        set {
            if (type == Type.Sliced)
                SetSliceFill(true, false, value);
            else
                fillAmount = value;
        }
    }

    public float fillAmountY {
        set {
            if (type == Type.Sliced)
                SetSliceFill(false, true, value);
            else
                fillAmount = value;
        }
    }
}