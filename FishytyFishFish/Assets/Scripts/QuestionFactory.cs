using System.Collections.Generic;

public class QuestionFactory {
    // create question and returns finished product
    public static question CreateQuestion(string question_text, float delay, string[] anwser_texts, question[] links, int amount_of_anwsers) {
        question q = new question(question_text, delay);

        List<anwser> anwsers = new List<anwser>();
        for(int i = 0; i < amount_of_anwsers; i++) {
            anwsers.Add(CreateAnwser(anwser_texts[i], links[i]));
        }
        q.anwser = anwsers;
        return q;
    }

    private static anwser CreateAnwser(string anwser_text, question link) {
        anwser a = new anwser(anwser_text, link);
        return a;
    }

}
