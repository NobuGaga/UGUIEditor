using UnityEngine;

namespace DOTweenExtension.Runtime {

    public struct DOTweenAnimationJson {

        // ABSAnimationComponent Member
        public string on_rewind;
        public string on_step_complete;
        public string on_update;
        public string on_play;
        public string on_start;
        public string on_complete;
        public bool is_speed_based;

        // DOTweenAnimation Member
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
        public string end_vector3_value;
        public string end_vector2_value;
        public string end_color_value;
        public string end_string_value;
        public string end_rect_pos_value;
        public string end_rect_size_value;
        public string end_transform_name;
        public bool optional_bool;
        public float optional_float;
        public int optional_int;
        public string optional_string;
        public string rotate_mode;
        public string scramble_mode;
    }

    public struct DOTweenAnimationJsons {

        public DOTweenAnimationJson[] jsons;
    }
}