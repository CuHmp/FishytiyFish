using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogMaster : MonoBehaviour {

    public List<question> question_list = new List<question>();
    public question question_cursor;
    public GameObject buttonPrefab;
    public int buttonPressed = -1;
    public float text_delay = 0.125f;
    // Start is called before the first frame update
    void Start() {
        AddButton.buttonPrefab = buttonPrefab;

        question q1 = new question("Hello Fellow human would you like a coffee?", text_delay);
        question q2 = new question("Aha I understand your situation now please be ready to die", 0.05f);


        List<anwser> list = new List<anwser>();
        list.Add(new anwser("test", q2));
        list.Add(new anwser("qwerts", q2));
        list.Add(new anwser("Yeet The Feet", q2));
        q1.anwser = list;
        question_list.Add(q1);
        List<anwser> lists = new List<anwser>();
        lists.Add(new anwser("test1", null));
        lists.Add(new anwser("asdf", null));
        lists.Add(new anwser("Leet Mega Haxor", null));
        q2.anwser = lists;
        question_list.Add(q2);

        question_cursor = question_list[0];
    }

    // Update is called once per frame
    void Update() {
        if (!TypeWriter.is_done && !TypeWriter.is_writing) {
            TypeWriter.typewriter_delay = question_cursor.text_delay;
            TypeWriter.StartTypewriter(question_cursor.text);
            for(int i = 0; i < question_cursor.anwser.Count; i++) {
                AddButton.MakeButton(question_cursor.anwser[i].text, this, i);
            }
        }
        if (buttonPressed >= 0) {
            if (question_cursor.anwser[buttonPressed].next_question != null) {
                question_cursor = question_cursor.anwser[buttonPressed].next_question;
                buttonPressed = -1;
            }

        }
    }
    public void OnClick(int index) {
        Transform parent = GameObject.Find("CommandPanel").transform;
        foreach (Transform child in parent) {
            Destroy(child.gameObject);
        }


        if (question_cursor.anwser[index].next_question != null) {
            question_cursor = question_cursor.anwser[index].next_question;
        }
        TypeWriter.is_done = false;
    }
}

public class question {
    public question(string text, float delay) {
        this.text = text;
        this.text_delay = delay;
    }
    public float text_delay { get; set; }
    public string text { get; set; }
    public List<anwser> anwser { get; set; }
}

public class anwser {
    public anwser(string text, question next_question) {
        this.text = text;
        this.next_question = next_question;
    }
    public string text { get; set; }
    public question next_question { get; set; }
}