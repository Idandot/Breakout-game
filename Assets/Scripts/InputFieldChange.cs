using UnityEngine;
using UnityEngine.UI;

public class InputFieldChange : MonoBehaviour
{
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = gameObject.GetComponent<InputField>();
    }

    private void Update()
    {
        inputField.onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(string inputText)
    {
        BetweenScenesData.instance.GetNewName(inputText);
    }
}
