using UnityEngine;
using UnityEngine.UI;
public class AddButton : MonoBehaviour {

    public static GameObject buttonPrefab;
    public static void MakeButton(string text, DialogMaster dialog_master, int index) {
        // Instantiate (clone) the prefab    
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.GetComponentInChildren<Text>().text = text;
        var listner = button.GetComponent<Button>();

        listner.onClick.AddListener((() => { dialog_master.OnClick(index); }));

        var panel = GameObject.Find("CommandPanel");
        button.transform.position = panel.transform.position;
        button.GetComponent<RectTransform>().SetParent(panel.transform);
        button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 10);
        button.layer = 5;

    }
}
