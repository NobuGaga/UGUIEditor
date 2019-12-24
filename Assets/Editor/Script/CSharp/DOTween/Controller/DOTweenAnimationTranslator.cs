using UnityEngine;
using DG.Tweening;
using DOTweenExtension.Runtime;

namespace DOTweenExtension.Editor {

    public static class DOTweenAnimationTranslator {

        public static DOTweenAnimationJson ToJson(this DOTweenAnimation animation) {
            DOTweenAnimationJson json = default;
            SetABSAnimationComponentData(animation, ref json);
            SetDOTweenAnimationData(animation, ref json);
            return json;
        }

        private static void SetABSAnimationComponentData(DOTweenAnimation animation, ref DOTweenAnimationJson json) {
            if (animation.hasOnPlay)
                json.on_rewind = animation.onRewind.ToJson();
            if (animation.hasOnStepComplete)
                json.on_step_complete = animation.onStepComplete.ToJson();
            if (animation.hasOnUpdate)
                json.on_update = animation.onUpdate.ToJson();
            if (animation.hasOnPlay)
                json.on_play = animation.onPlay.ToJson();
            if (animation.hasOnStart)
                json.on_start = animation.onStart.ToJson();
            if (animation.hasOnComplete)
                json.on_complete = animation.onComplete.ToJson();
            json.is_speed_based = animation.isSpeedBased;
        }

        private static void SetDOTweenAnimationData(DOTweenAnimation animation, ref DOTweenAnimationJson json) {
            GameObject targetGO = animation.targetIsSelf ? animation.gameObject : animation.targetGO;
            json.target_name = targetGO.name;

            json.animation_type = animation.animationType.ToString();
            json.target_type = animation.targetType.ToString();
            json.delay = animation.delay;
            json.duration = animation.duration;

            json.ease_type = animation.easeType.ToString();
            AnimationCurve curve = animation.easeCurve;
            if (curve.keys != null && curve.keys.Length > 0) {
                int length = curve.keys.Length;
                json.curve_times = new float[length];
                json.curve_values = new float[length];
                for (ushort index = 0; index < length; index++) {
                    json.curve_times[index] = curve.keys[index].time;
                    json.curve_values[index] = curve.keys[index].value;
                }
            }

            json.loop_type = animation.loopType.ToString();
            json.loops = animation.loops;
            json.id = animation.id;
            json.is_relative = animation.isRelative;
            json.is_from = animation.isFrom;
            json.is_independent_update = animation.isIndependentUpdate;
            json.is_auto_kill = animation.autoKill;
            json.is_active = animation.isActive;
            json.is_valid = animation.isValid;
            json.is_auto_play = animation.autoPlay;
            json.is_use_target_vector3 = animation.useTargetAsV3;
            json.end_float_value = animation.endValueFloat;
            json.end_vector3_value = animation.endValueV3.ToJson();
            json.end_vector2_value = animation.endValueColor.ToJson();
            json.end_string_value = animation.endValueString;
            json.end_rect_pos_value = animation.endValueRect.position.ToJson();
            json.end_rect_size_value = animation.endValueRect.size.ToJson();
            if (animation.endValueTransform != null)
                json.end_transform_name = animation.endValueTransform.name;
            json.optional_bool = animation.optionalBool0;
            json.optional_float = animation.optionalFloat0;
            json.optional_int = animation.optionalInt0;
            json.optional_string = animation.optionalString;
            json.rotate_mode = animation.optionalRotationMode.ToString();
            json.scramble_mode = animation.optionalScrambleMode.ToString();
        }
    }
}