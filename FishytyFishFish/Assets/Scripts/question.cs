using System.Collections.Generic;

[System.Serializable]
public class question {
    public question(string text, float delay) {
        this.text = text;
        this.text_delay = delay;
    }
    public float text_delay { get; set; }
    public string text { get; set; }
    public List<anwser> anwser { get; set; }
    public tags anwsered_tag { get; set; }
}

