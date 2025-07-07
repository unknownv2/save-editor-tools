using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Metro_2033.Controls
{
    public partial class MetroUserConfigControl : UserControl
    {
        public string Value
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        public MetroUserConfigControl()
        {
            InitializeComponent();
            comboBoxEx1.Items.AddRange(Template_Values);
        }
        private static string[] Template_Values =
        {
           "g_god 1",
"g_unlimitedammo 1",
"g_global_god on",
"_show_subtitles 0",
"ai::feel_vision off",
"ai::feel_vision_ex off",
"ai::graph off",
"ai::graph::links off",
"ai::graph::normals off",
"ai::graph::radius 10",
"ai::look_body off",
"ai::look_head off",
"ai::path::detail off",
"ai::path::patrol off",
"ai::path::vertex off",
"ai::patrol off",
"ai::space_restrictions off",
"unbindall",
"bind changemenumap kESCAPE",
"bind wpn_1 k1",
"bind wpn_2 k2",
"bind wpn_3 k3",
"bind wpn_4 k4",
"bind wpn_5 k5",
"bind medkit kQ",
"bind forward kW",
"bind use kE",
"bind wpn_reload kR",
"bind wpn_next kLBRACKET",
"bind wpn_prev kRBRACKET",
"bind time kT",
"bind menu_enter kRETURN",
"bind crouch kLCONTROL",
"bind lstrafe kA",
"bind back kS",
"bind rstrafe kD",
"bind wpn_light kF",
"bind gasmask kG",
"bind console kGRAVE",
"bind sprint kLSHIFT",
"bind crouch_toggle kZ",
"bind accel kX",
"bind nightvision kN",
"bind map kM",
"bind jump kSPACE",
"bind cam_1 kF1",
"bind cam_2 kF2",
"bind cam_3 kF3",
"bind quick_save kF5",
"bind quick_load kF7",
"bind quick_load kF8",
"bind cam_zoom_out kSUBTRACT",
"bind cam_zoom_in kADD",
"bind pause kPAUSE",
"bind up kUP",
"bind left kLEFT",
"bind right kRIGHT",
"bind down kDOWN",
"bind wpn_fire mouse0",
"bind wpn_aim mouse1",
"bind nightvision x_dpad_up",
"bind gasmask x_dpad_down",
"bind wpn_next x_dpad_left",
"bind wpn_prev x_dpad_right",
"bind changemenumap x_start",
"bind map x_back",
"bind sprint x_left_thumb",
"bind medkit x_right_thumb",
"bind crouch_toggle x_left_shoulder",
"bind jump x_right_shoulder",
"bind use x_a",
"bind wpn_light x_b",
"bind wpn_reload x_x",
"bind time x_y",
"bind wpn_aim x_left_trigger",
"bind wpn_fire x_right_trigger",
"cvr_cover_hit_danger_distance 3",
"cvr_cover_hit_danger_interval 1200",
"cvr_cover_hit_danger_penalty 1000",
"cvr_death_danger_distance 5",
"cvr_death_danger_interval 2400",
"cvr_death_danger_penalty 10000",
"cvr_grenade_danger_distance 10",
"cvr_grenade_danger_interval 1",
"cvr_grenade_danger_penalty 1000",
"cvr_hit_danger_distance 3",
"cvr_hit_danger_interval 1200",
"cvr_hit_danger_penalty 1000",
"g_autopickup on",
"g_global_god off",
"g_god off",
"g_unlimitedammo off",
"mouse_sens 0.4",
"msaa 0",
"ph_ce_sound_distance 70",
"ph_ce_sound_maxvelocity 50",
"ph_ce_sound_minvelocity 1",
"ph_dbg_render 0",
"ph_dump_stats 0",
"ph_enable_int_coll 1",
"phv_actor_axes 0",
"phv_anim_mixing 0",
"phv_awake_only 0",
"phv_body_ang_velocity 0",
"phv_body_axes 0",
"phv_body_lin_velocity 0",
"phv_body_mass_axes 0",
"phv_buoyancy 0",
"phv_ccd_sceletons 0",
"phv_cloth_mesh 0",
"phv_collision_aabbs 0",
"phv_collision_axes 0",
"phv_collision_compounds 0",
"phv_collision_dynamic 1",
"phv_collision_edges 0",
"phv_collision_fnormals 0",
"phv_collision_free 0",
"phv_collision_sap 0",
"phv_collision_shapes 0",
"phv_collision_spheres 0",
"phv_collision_static 0",
"phv_collision_vnormals 0",
"phv_contact_error 0",
"phv_contact_force 0",
"phv_contact_normal 0",
"phv_contact_point 0",
"phv_dynamic 0",
"phv_fluids 0",
"phv_joints 0",
"phv_kinematic 1",
"phv_static 0",
"phv_trigger_shapes_only 0",
"phv_use_zbuffer 0",
"phv_world_axes 0",
"physx_connect_to_debugger 0",
"r_af_level 0",
"r_bloom_threshold 0.01",
"r_can_miniformat 0",
"r_dao 0",
"r_deblur_dist 10",
"r_api 0",
"r_exp_temporal 0",
"r_fullscreen oN",
"r_hud on",
"r_hud_weapon on",
"r_ignore_portals off",
"r_light_frames2sleep 10",
"r_local_mblur_coef 0.015",
"r_mipcolor 0",
"r_msaa_level 0",
"r_quality_level 3",
"r_res_hor 1680",
"r_res_vert 1050",
"r_show 0",
"r_sun_depth_far_bias 0",
"r_sun_depth_far_scale 1",
"r_sun_depth_near_bias -0",
"r_sun_depth_near_scale 1",
"r_sun_near 12",
"r_sun_near_border 0.666",
"r_sun_tsm_bias -0",
"r_sun_tsm_proj 0.2",
"r_supersample 1.",
"r_texnostreaming off",
"r_tone_adaptation 5.",
"r_tone_amount 0.",
"r_tone_low_lum 0.01",
"r_tone_middlegray 0.33",
"r_view_distance 125.",
"r_vsync off",
"role_border0 100.",
"role_border1 1000.",
"role_time0 2.",
"role_time1 0.5",
"s_cone_inner_volume 1.",
"s_cone_outer_volume 0.75",
"s_dbg_draw 0",
"s_dbg_draw_dist 0",
"s_dbg_draw_name 0",
"s_dbg_draw_stopped 0",
"s_dbg_stat_active 0",
"s_master_volume 0.5",
"s_music_volume 0.5",
"s_render_targets 24",
"sick_camera 0.",
"sick_fov 45.",
"sick_hud 0.",
"sick_mblur 0.",
"sick_mouse 0.",
"sick_slowmo 0.",
"stats off",
"stats_graph 1.000000,5,5.000000,1,0.000000",
"stats_graph_rect 300,200,1000,200",
"show_points_ex 0",
"invert_y_axis 0",
"gamepad_preset 0",
"g_game_difficulty 1",
"joy_sens_x 1.5",
"joy_sens_aiming_x 0.4",
"vibration 2",
        };

        private void cmdAddValue_Click(object sender, EventArgs e)
        {
            //Set our text
            Clipboard.SetText(comboBoxEx1.Text);
        }
        private void RemoveItemWith(string with)
        {
            //Create our resultant text
            string result = "";
            //Loop for each line
            foreach (string line in richTextBox1.Lines)
                if (line.Length > with.Length && line.Substring(0, with.Length) == with)
                {
                }
                else
                {
                    result += line + "\n";
                }
            try
            {
                while (result[result.Length - 1] == '\n')
                    result = result.Substring(0, result.Length - 1);
            }
            catch { }
            richTextBox1.Text = result + "\n";
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            RemoveItemWith("g_god");
            richTextBox1.Text += "g_god 1\n";
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            RemoveItemWith("g_unlimitedammo");
            richTextBox1.Text += "g_unlimitedammo 1\n";
        }
    }
}
