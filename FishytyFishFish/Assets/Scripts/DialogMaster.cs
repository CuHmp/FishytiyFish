using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum tags {
    night,
    morning,
    lunch,
    lying,
    no_tag,
}
[System.Serializable]
public class DialogMaster : MonoBehaviour {

    public static List<question> question_list = new List<question>();
    public question question_cursor;
    public GameObject buttonPrefab;
    public float text_delay = 0.125f;

    private bool hasCreatedButtons = false;

    public List<UIQuestionHolder> questions;

    void Start() {
        AddButton.buttonPrefab = buttonPrefab;

//        questions.Reverse();

        for (int i = 0; i < questions.Count; i++) {
            question q = new question(questions[i].text, questions[i].text_delay);
            List<anwser> anwser_list = new List<anwser>();
            for(int j = 0; j < questions[i].anwser_list.Count; j++) {
                anwser a = new anwser(questions[i].anwser_list[j].text, null, questions[i].anwser_list[j].tag);
                anwser_list.Add(a);
            }
            q.anwser = anwser_list;
            question_list.Add(q);
        }

        for (int i = 0; i < questions.Count; i++) {
            for (int j = 0; j < questions[i].anwser_list.Count; j++) {
                if (questions[i].anwser_list[j].next_question == -1) {
                    question_list[i].anwser[j].next_question = null;
                    continue;
                }
                else {
                    question_list[i].anwser[j].next_question = question_list[questions[i].anwser_list[j].next_question];
                }
            }
        }


        question_cursor = question_list[0];
    }

    void Update() {
        // start the printing of the text
        if (!TypeWriter.is_done && !TypeWriter.is_writing) {
            TypeWriter.typewriter_delay = question_cursor.text_delay;
            TypeWriter.StartTypewriter(question_cursor.text);
        }
        // add the buttons when the text has finished printing
        if (TypeWriter.is_done && !hasCreatedButtons) {
            for (int i = 0; i < question_cursor.anwser.Count; i++) {
                AddButton.MakeButton(question_cursor.anwser[i].text, this, i);
            }
            hasCreatedButtons = true;
        }
    }

    public void OnClick(int index) {
        question_cursor.anwsered_tag = question_cursor.anwser[index].anwser_tag;

        // Remove all the previous buttons
        Transform parent = GameObject.Find("CommandPanel").transform;
        foreach (Transform child in parent) {
            Destroy(child.gameObject);
        }
        hasCreatedButtons = false;

        // Go to the corresponding button press question
        if (question_cursor.anwser[index].next_question != null) {
            question_cursor = question_cursor.anwser[index].next_question;
        }
        else {
            hasCreatedButtons = true;
            TypeWriter.StartTypewriter("Game over, thank you for playing");
        }
        // reset the typewriter and button creator
        TypeWriter.is_done = false;

    }
}

[System.Serializable]
public class UIAnwsersHolder {
    public string text;
    public tags tag;
    public int next_question;
}

[System.Serializable]
public class UIQuestionHolder {
    public string text = "";
    public float text_delay;
    public List<UIAnwsersHolder> anwser_list;
}