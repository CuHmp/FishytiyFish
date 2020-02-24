using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour {

    private static TypeWriter instance = null;
    private static Text text_box_;
    private static string story_;
    private static float typewriter_delay_ = 0.125f;
    private static bool is_writing_ = false;
    private static bool is_done_ = false;
    void Start() {
        instance = this;

        text_box_ = GetComponent<Text>();
        text_box_.text = "";
    }

    public static void StartTypewriter(string story) {
        if (is_writing_) {
            return;
        }
        text_box_.text = "";
        story_ = story;
        TypeWriter.instance.StopCoroutine("PlayText");
        TypeWriter.instance.StartCoroutine("PlayText");
    }

    IEnumerator PlayText() {
        is_writing_ = true;
        foreach (char c in story_) {
            text_box_.text += c;
            yield return new WaitForSeconds(typewriter_delay_);
        }
        is_writing_ = false;
        is_done_ = true;
    }

    public static bool is_writing { get { return is_writing_; } }
    public static bool is_done { get { return is_done_; } set { is_done_ = value; } }
    public static float typewriter_delay { get { return typewriter_delay_; } set { typewriter_delay_ = value; } }
}
