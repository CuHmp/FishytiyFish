public class anwser {
    public anwser(string text, question next_question, tags anwser_tag) {
        this.text = text;
        this.next_question = next_question;
        this.anwser_tag = anwser_tag;
    }
    public string text { get; set; }
    public question next_question { get; set; }
    public tags anwser_tag { get; }
}