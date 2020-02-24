using System.Collections.Generic;
using UnityEngine;

public class DialogMaster : MonoBehaviour {

    public static List<question> question_list = new List<question>();
    public question question_cursor;
    public GameObject buttonPrefab;
    public float text_delay = 0.125f;

    private bool hasCreatedButtons = false;

    void Start() {
        AddButton.buttonPrefab = buttonPrefab;

        question_list.Add(QuestionFactory.CreateQuestion("Aha I understand your situation now please be ready to die",
                                                        text_delay, new string[] { "test1", "asdf", "Leet Mega Haxor" },
                                                        new question[] { null,null,null }, 3));
        question_list.Add(QuestionFactory.CreateQuestion("Here you go you litle shit",
                                                         text_delay, new string[] { ";(", "What", "Stab officer" },
                                                         new question[] { question_list[0], question_list[0], question_list[0] }, 3));
        question_list.Add(QuestionFactory.CreateQuestion("The Cop leave you alone in the room to grab a coffy",
                                                 text_delay, new string[] { "Okey" },
                                                 new question[] { question_list[1] }, 1));
        question_list.Add(QuestionFactory.CreateQuestion("Hello Fellow human would you like a coffee?",
                                                 text_delay, new string[] { "Yes", "No", "Draw gun" },
                                                 new question[] { question_list[2], question_list[1], question_list[0] }, 3));
        question_list.Reverse();

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