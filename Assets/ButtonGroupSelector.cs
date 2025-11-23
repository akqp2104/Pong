using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupSelector : MonoBehaviour
{
    public Button[] buttons;
    public Color normalColor = Color.gray;
    public Color highlightedColor = Color.white;
    private int selectedIndex = -1;

    void Start()
    {
        // Assign listeners
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture index for the lambda
            buttons[i].onClick.AddListener(() => SelectButton(index));
        }
        // Optionally, select a default button (e.g. first one)
        SelectButton(0);
    }

    public void SelectButton(int index)
    {
        selectedIndex = index;
        for (int i = 0; i < buttons.Length; i++)
        {
            var colors = buttons[i].colors;
            colors.normalColor = (i == selectedIndex) ? highlightedColor : normalColor;
            buttons[i].colors = colors;
        }
    }
}
