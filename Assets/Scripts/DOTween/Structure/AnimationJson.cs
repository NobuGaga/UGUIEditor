using UnityEngine;

namespace DOTweenExtension.Runtime {

    public struct DOTweenAnimationJson {

        public string target_name;
        public string animation_type;
        public string target_type;
        public float delay;
        public float duration;
        public string ease_type;
        public float[] curve_times;
        public float[] curve_values;
        public string curve_pre_wrap;
        public string curve_post_wrap;
        public string loop_type;
        public int loops;
        public string id;
        public bool is_relative;
        public bool is_from;
        public bool is_independent_update;
        public bool is_auto_kill;
        public bool is_active;
        public bool is_valid;
        public bool is_auto_play;
        public bool is_use_target_vector3;
        public float end_float_value;
        public Vector3 end_vector3_value;
        public Vector2 end_vector2_value;
        public Color end_color_value;
        public string end_string_value;
        public Rect end_rect_value;
        public string end_transform_name;
        public bool optional_bool;
        public float optional_float;
        public int optional_int;
        public string optional_string;
        public string rotate_mode;
        public string scramble_mode;

        public DOTweenAnimationJson(string target_name) {
            this.target_name = target_name;
            animation_type = target_type = ease_type = curve_pre_wrap = curve_post_wrap = loop_type = id = 
            end_string_value = end_transform_name = optional_string = rotate_mode = scramble_mode = default;
            delay = duration = end_float_value = optional_float = default;
            curve_times = curve_values = default;
            loops = optional_int = default;
            is_relative = is_from = is_independent_update = is_auto_kill = is_active = is_valid = is_auto_play = is_use_target_vector3 = optional_bool = default;
            end_vector3_value = end_vector2_value = default;
            end_color_value = default;
            end_rect_value = default;
        }
    }

    public struct DOTweenAnimationJsons {

        public DOTweenAnimationJson[] jsons;

        public DOTweenAnimationJsons(DOTweenAnimationJson[] jsons) => this.jsons = jsons;
    }
}