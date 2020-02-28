using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum tags {
    time_question,
    lying,
    truth,
    no_tag,
}
[System.Serializable]
public class DialogMaster : MonoBehaviour {

    public static List<question> question_list = new List<question>();
    private static List<question> result_question_list = new List<question>();
    private static List<question> time_question_list = new List<question>();
    private int correct_time_index = 16;
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
        question_cursor.question_index = index;
        result_question_list.Add(question_cursor);

        if (question_cursor.anwser[index].anwser_tag == tags.time_question) {
            time_question_list.Add(question_cursor);
            Debug.Log("Added time question");
            if (time_question_list.Count > 1) {
                if (time_question_list[1].question_index == time_question_list[0].question_index) {
                    question_cursor = question_list[correct_time_index];
                }
            }

        }

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

            //int truth = 0;
            //int lying = 0;



            //for(int i = 0; i < result_question_list.Count; i++) {
            //    if(result_question_list[i].anwsered_tag == tags.truth) {
            //        truth++;
            //    }
            //    if(result_question_list[i].anwsered_tag == tags.lying) {
            //        lying++;
            //    }
            //}

            //if(truth > lying) {
            //    TypeWriter.StartTypewriter("Game over, the cops is going to investigate and jail Karen");
            //}
            //else if(truth < lying) {
            //    TypeWriter.StartTypewriter("Game over, the cops is going to investigate and jail you");
            //}
            //else {
            //    TypeWriter.StartTypewriter("Game over, the cops is going to investigate both of you");
            //}
            question_cursor = question_list[0];
            time_question_list.Clear();
            result_question_list.Clear();
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