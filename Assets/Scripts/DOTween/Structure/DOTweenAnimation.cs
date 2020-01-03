#region Copy Class ABSAnimationComponent And Class DOTweenAnimation Using
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
#endregion

#region Custom namespace and class head

namespace DOTweenExtension.Runtime {

    internal class DOTweenAnimation {

#endregion

        #region Copy ABSAnimationComponent Member (Delete updateType, hasOnTweenCreated, onTweenCreated) and disable structure default value warning

        #pragma warning disable 0649

        public Tween tween;
        public UnityEvent onRewind;
        public UnityEvent onStepComplete;
        public UnityEvent onUpdate;
        public UnityEvent onPlay;
        public UnityEvent onStart;
        public UnityEvent onComplete;
        public bool hasOnComplete;
        public bool hasOnStepComplete;
        public bool hasOnUpdate;
        public bool hasOnRewind;
        public bool hasOnPlay;
        public bool hasOnStart;
        public bool isSpeedBased;
        #endregion

        #region Copy DOTweenAnimation Enum

        public enum AnimationType
        {
            None,
            Move, LocalMove,
            Rotate, LocalRotate,
            Scale,
            Color, Fade,
            Fill,
            Text,
            PunchPosition, PunchRotation, PunchScale,
            ShakePosition, ShakeRotation, ShakeScale,
            CameraAspect, CameraBackgroundColor, CameraFieldOfView, CameraOrthoSize, CameraPixelRect, CameraRect,
            UIWidthHeight
        }

        public enum TargetType
        {
            Unset,

            Camera,
            CanvasGroup,
            Image,
            Light,
            RectTransform,
            Renderer, SpriteRenderer,
            Rigidbody, Rigidbody2D,
            Text,
            Transform,

            tk2dBaseSprite,
            tk2dTextMesh,

            TextMeshPro,
            TextMeshProUGUI
        }
        #endregion

        #region Copy DOTweenAnimation Member (Delete private member) and restore structure default value warning

        public bool targetIsSelf = true; // If FALSE allows to set the target manually
        public GameObject targetGO = null; // Used in case targetIsSelf is FALSE
        // If TRUE always uses the GO containing this DOTweenAnimation (and not the one containing the target) as DOTween's SetTarget target
        public bool tweenTargetIsTargetGO = true;

        public float delay;
        public float duration = 1;
        public Ease easeType = Ease.OutQuad;
        public AnimationCurve easeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public LoopType loopType = LoopType.Restart;
        public int loops = 1;
        public string id = "";
        public bool isRelative;
        public bool isFrom;
        public bool isIndependentUpdate = false;
        public bool autoKill = true;

        public bool isActive = true;
        public bool isValid;
        public Component target;
        public AnimationType animationType;
        public TargetType targetType;
        public TargetType forcedTargetType; // Used when choosing between multiple targets
        public bool autoPlay = true;
        public bool useTargetAsV3;

        public float endValueFloat;
        public Vector3 endValueV3;
        public Vector2 endValueV2;
        public Color endValueColor = new Color(1, 1, 1, 1);
        public string endValueString = "";
        public Rect endValueRect = new Rect(0, 0, 0, 0);
        public Transform endValueTransform;

        public bool optionalBool0;
        public float optionalFloat0;
        public int optionalInt0;
        public RotateMode optionalRotationMode = RotateMode.Fast;
        public ScrambleMode optionalScrambleMode = ScrambleMode.None;
        public string optionalString;

        #pragma warning restore 0649

        #endregion

        #region Copy DOTweenAnimation CreateTween() Method (Delete hasOnTweenCreated and onTweenCreated code, this.gameObject use targetGO insteaded)

        // Used also by DOTweenAnimationInspector when applying runtime changes and restarting
        public void CreateTween()
        {
//            if (target == null) {
//                Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target is NULL, because the animation was created with a DOTween Pro version older than 0.9.255. To fix this, exit Play mode then simply select this object, and it will update automatically", targetGO.name), targetGO);
//                return;
//            }

            GameObject tweenGO = GetTweenGO();
            if (target == null || tweenGO == null) {
                if (targetIsSelf && target == null) {
                    // Old error caused during upgrade from DOTween Pro 0.9.255
                    Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target is NULL, because the animation was created with a DOTween Pro version older than 0.9.255. To fix this, exit Play mode then simply select this object, and it will update automatically", targetGO.name), targetGO);
                } else {
                    // Missing non-self target
                    Debug.LogWarning(string.Format("{0} :: This DOTweenAnimation's target/GameObject is unset: the tween will not be created.", targetGO.name), targetGO);
                }
                return;
            }

            if (forcedTargetType != TargetType.Unset) targetType = forcedTargetType;
            if (targetType == TargetType.Unset) {
                // Legacy DOTweenAnimation (made with a version older than 0.9.450) without stored targetType > assign it now
                targetType = TypeToDOTargetType(target.GetType());
            }

            switch (animationType) {
            case AnimationType.None:
                break;
            case AnimationType.Move:
                if (useTargetAsV3) {
                    isRelative = false;
                    if (endValueTransform == null) {
                        Debug.LogWarning(string.Format("{0} :: This tween's TO target is NULL, a Vector3 of (0,0,0) will be used instead", targetGO.name), targetGO);
                        endValueV3 = Vector3.zero;
                    } else {
#if true // UI_MARKER
                        if (targetType == TargetType.RectTransform) {
                            RectTransform endValueT = endValueTransform as RectTransform;
                            if (endValueT == null) {
                                Debug.LogWarning(string.Format("{0} :: This tween's TO target should be a RectTransform, a Vector3 of (0,0,0) will be used instead", targetGO.name), targetGO);
                                endValueV3 = Vector3.zero;
                            } else {
                                RectTransform rTarget = target as RectTransform;
                                if (rTarget == null) {
                                    Debug.LogWarning(string.Format("{0} :: This tween's target and TO target are not of the same type. Please reassign the values", targetGO.name), targetGO);
                                } else {
                                    // Problem: doesn't work inside Awake (ararargh!)
                                    endValueV3 = DOTweenModuleUI.Utils.SwitchToRectTransform(endValueT, rTarget);
                                }
                            }
                        } else
#endif
                            endValueV3 = endValueTransform.position;
                    }
                }
                switch (targetType) {
                case TargetType.Transform:
                    tween = ((Transform)target).DOMove(endValueV3, duration, optionalBool0);
                    break;
                case TargetType.RectTransform:
#if true // UI_MARKER
                    tween = ((RectTransform)target).DOAnchorPos3D(endValueV3, duration, optionalBool0);
#else
                    tween = ((Transform)target).DOMove(endValueV3, duration, optionalBool0);
#endif
                    break;
                case TargetType.Rigidbody:
#if false // PHYSICS_MARKER
                    tween = ((Rigidbody)target).DOMove(endValueV3, duration, optionalBool0);
#else
                    tween = ((Transform)target).DOMove(endValueV3, duration, optionalBool0);
#endif
                    break;
                case TargetType.Rigidbody2D:
#if false // PHYSICS2D_MARKER
                    tween = ((Rigidbody2D)target).DOMove(endValueV3, duration, optionalBool0);
#else
                    tween = ((Transform)target).DOMove(endValueV3, duration, optionalBool0);
#endif
                    break;
                }
                break;
            case AnimationType.LocalMove:
                tween = tweenGO.transform.DOLocalMove(endValueV3, duration, optionalBool0);
                break;
            case AnimationType.Rotate:
                switch (targetType) {
                case TargetType.Transform:
                    tween = ((Transform)target).DORotate(endValueV3, duration, optionalRotationMode);
                    break;
                case TargetType.Rigidbody:
#if false // PHYSICS_MARKER
                    tween = ((Rigidbody)target).DORotate(endValueV3, duration, optionalRotationMode);
#else
                    tween = ((Transform)target).DORotate(endValueV3, duration, optionalRotationMode);
#endif
                    break;
                case TargetType.Rigidbody2D:
#if false // PHYSICS2D_MARKER
                    tween = ((Rigidbody2D)target).DORotate(endValueFloat, duration);
#else
                    tween = ((Transform)target).DORotate(endValueV3, duration, optionalRotationMode);
#endif
                    break;
                }
                break;
            case AnimationType.LocalRotate:
                tween = tweenGO.transform.DOLocalRotate(endValueV3, duration, optionalRotationMode);
                break;
            case AnimationType.Scale:
                switch (targetType) {
#if false // TK2D_MARKER
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                    break;
#endif
                default:
                    tween = tweenGO.transform.DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                    break;
                }
                break;
#if true // UI_MARKER
            case AnimationType.UIWidthHeight:
                tween = ((RectTransform)target).DOSizeDelta(optionalBool0 ? new Vector2(endValueFloat, endValueFloat) : endValueV2, duration);
                break;
#endif
            case AnimationType.Color:
                isRelative = false;
                switch (targetType) {
                case TargetType.Renderer:
                    tween = ((Renderer)target).material.DOColor(endValueColor, duration);
                    break;
                case TargetType.Light:
                    tween = ((Light)target).DOColor(endValueColor, duration);
                    break;
#if false // SPRITE_MARKER
                case TargetType.SpriteRenderer:
                    tween = ((SpriteRenderer)target).DOColor(endValueColor, duration);
                    break;
#endif
#if true // UI_MARKER
                case TargetType.Image:
                    tween = ((Graphic)target).DOColor(endValueColor, duration);
                    break;
                case TargetType.Text:
                    tween = ((Text)target).DOColor(endValueColor, duration);
                    break;
#endif
#if false // TK2D_MARKER
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOColor(endValueColor, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOColor(endValueColor, duration);
                    break;
#endif
#if false // TEXTMESHPRO_MARKER
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOColor(endValueColor, duration);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOColor(endValueColor, duration);
                    break;
#endif
                }
                break;
            case AnimationType.Fade:
                isRelative = false;
                switch (targetType) {
                case TargetType.Renderer:
                    tween = ((Renderer)target).material.DOFade(endValueFloat, duration);
                    break;
                case TargetType.Light:
                    tween = ((Light)target).DOIntensity(endValueFloat, duration);
                    break;
#if false // SPRITE_MARKER
                case TargetType.SpriteRenderer:
                    tween = ((SpriteRenderer)target).DOFade(endValueFloat, duration);
                    break;
#endif
#if true // UI_MARKER
                case TargetType.Image:
                    tween = ((Graphic)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.Text:
                    tween = ((Text)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.CanvasGroup:
                    tween = ((CanvasGroup)target).DOFade(endValueFloat, duration);
                    break;
#endif
#if false // TK2D_MARKER
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.tk2dBaseSprite:
                    tween = ((tk2dBaseSprite)target).DOFade(endValueFloat, duration);
                    break;
#endif
#if false // TEXTMESHPRO_MARKER
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOFade(endValueFloat, duration);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOFade(endValueFloat, duration);
                    break;
#endif
                }
                break;
            case AnimationType.Fill:
                isRelative = false;
                switch (targetType) {
#if true // UI_MARKER
                case TargetType.Image:
                    tween = (target as Image).DOFillAmount(endValueFloat, duration);
                    break;
#endif
                }
                break;
            case AnimationType.Text:
#if true // UI_MARKER
                switch (targetType) {
                case TargetType.Text:
                    tween = ((Text)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
                }
#endif
#if false // TK2D_MARKER
                switch (targetType) {
                case TargetType.tk2dTextMesh:
                    tween = ((tk2dTextMesh)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
                }
#endif
#if false // TEXTMESHPRO_MARKER
                switch (targetType) {
                case TargetType.TextMeshProUGUI:
                    tween = ((TextMeshProUGUI)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
                case TargetType.TextMeshPro:
                    tween = ((TextMeshPro)target).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    break;
                }
#endif
                break;
            case AnimationType.PunchPosition:
                switch (targetType) {
                case TargetType.Transform:
                    tween = ((Transform)target).DOPunchPosition(endValueV3, duration, optionalInt0, optionalFloat0, optionalBool0);
                    break;
#if true // UI_MARKER
                case TargetType.RectTransform:
                    tween = ((RectTransform)target).DOPunchAnchorPos(endValueV3, duration, optionalInt0, optionalFloat0, optionalBool0);
                    break;
#endif
                }
                break;
            case AnimationType.PunchScale:
                tween = tweenGO.transform.DOPunchScale(endValueV3, duration, optionalInt0, optionalFloat0);
                break;
            case AnimationType.PunchRotation:
                tween = tweenGO.transform.DOPunchRotation(endValueV3, duration, optionalInt0, optionalFloat0);
                break;
            case AnimationType.ShakePosition:
                switch (targetType) {
                case TargetType.Transform:
                    tween = ((Transform)target).DOShakePosition(duration, endValueV3, optionalInt0, optionalFloat0, optionalBool0);
                    break;
#if true // UI_MARKER
                case TargetType.RectTransform:
                    tween = ((RectTransform)target).DOShakeAnchorPos(duration, endValueV3, optionalInt0, optionalFloat0, optionalBool0);
                    break;
#endif
                }
                break;
            case AnimationType.ShakeScale:
                tween = tweenGO.transform.DOShakeScale(duration, endValueV3, optionalInt0, optionalFloat0);
                break;
            case AnimationType.ShakeRotation:
                tween = tweenGO.transform.DOShakeRotation(duration, endValueV3, optionalInt0, optionalFloat0);
                break;
            case AnimationType.CameraAspect:
                tween = ((Camera)target).DOAspect(endValueFloat, duration);
                break;
            case AnimationType.CameraBackgroundColor:
                tween = ((Camera)target).DOColor(endValueColor, duration);
                break;
            case AnimationType.CameraFieldOfView:
                tween = ((Camera)target).DOFieldOfView(endValueFloat, duration);
                break;
            case AnimationType.CameraOrthoSize:
                tween = ((Camera)target).DOOrthoSize(endValueFloat, duration);
                break;
            case AnimationType.CameraPixelRect:
                tween = ((Camera)target).DOPixelRect(endValueRect, duration);
                break;
            case AnimationType.CameraRect:
                tween = ((Camera)target).DORect(endValueRect, duration);
                break;
            }

            if (tween == null) return;

            if (isFrom) {
                ((Tweener)tween).From(isRelative);
            } else {
                tween.SetRelative(isRelative);
            }
            GameObject setTarget = targetIsSelf || !tweenTargetIsTargetGO ? targetGO : targetGO;
            tween.SetTarget(setTarget).SetDelay(delay).SetLoops(loops, loopType).SetAutoKill(autoKill)
                .OnKill(()=> tween = null);
            if (isSpeedBased) tween.SetSpeedBased();
            if (easeType == Ease.INTERNAL_Custom) tween.SetEase(easeCurve);
            else tween.SetEase(easeType);
            if (!string.IsNullOrEmpty(id)) tween.SetId(id);
            tween.SetUpdate(isIndependentUpdate);

            if (hasOnStart) {
                if (onStart != null) tween.OnStart(onStart.Invoke);
            } else onStart = null;
            if (hasOnPlay) {
                if (onPlay != null) tween.OnPlay(onPlay.Invoke);
            } else onPlay = null;
            if (hasOnUpdate) {
                if (onUpdate != null) tween.OnUpdate(onUpdate.Invoke);
            } else onUpdate = null;
            if (hasOnStepComplete) {
                if (onStepComplete != null) tween.OnStepComplete(onStepComplete.Invoke);
            } else onStepComplete = null;
            if (hasOnComplete) {
                if (onComplete != null) tween.OnComplete(onComplete.Invoke);
            } else onComplete = null;
            if (hasOnRewind) {
                if (onRewind != null) tween.OnRewind(onRewind.Invoke);
            } else onRewind = null;

            if (autoPlay) tween.Play();
            else tween.Pause();
        }

        // Returns the gameObject whose target component should be animated
        GameObject GetTweenGO()
        {
            return targetIsSelf ? targetGO : targetGO;
        }

        public TargetType TypeToDOTargetType(Type t)
        {
            string str = t.ToString();
            int dotIndex = str.LastIndexOf(".");
            if (dotIndex != -1) str = str.Substring(dotIndex + 1);
            if (str.IndexOf("Renderer") != -1 && (str != "SpriteRenderer")) str = "Renderer";
//#if true // PHYSICS_MARKER
//            if (str == "Rigidbody") str = "Transform";
//#endif
//#if true // PHYSICS2D_MARKER
//            if (str == "Rigidbody2D") str = "Transform";
//#endif
#if true // UI_MARKER
//            if (str == "RectTransform") str = "Transform";
            if (str == "RawImage") str = "Image"; // RawImages are managed like Images for DOTweenAnimation (color and fade use Graphic target anyway)
#endif
            return (TargetType)Enum.Parse(typeof(TargetType), str);
        }
        #endregion
    
        #region Custom Method

        public Tween GetTween() {
            CreateTween();
            return tween;
        }
        #endregion
    }
}