using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ConsoleController : MonoBehaviour
{

    public TMPro.TMP_InputField InputField;
    public ConsoleHistory ConsoleHistory;
    public GameObject ConsolePanel;
    public string ErrorMessage;
    public Color ErrorColor;
    public List<ConsoleCommand> commands = new List<ConsoleCommand>();


    //Inputs
    public void OnConsole(){
        ConsolePanel.SetActive(!ConsolePanel.activeSelf);
        InputField.Select();

    }

    public void OnConsoleSubmit(){
        string text = InputField.text;

        ConsoleCommand command = commands.Where(command => command.Command == text).FirstOrDefault();

        if(command){
            ConsoleHistory.AddMessage(text);
            ConsoleHistory.AddMessage(WrapWithColorTag(command.ResponseColor, command.Response));
            command.CallbackEvent.Invoke();
        }else{
            ConsoleHistory.AddMessage(text);
            ConsoleHistory.AddMessage(WrapWithColorTag(this.ErrorColor, this.ErrorMessage));
        }

        InputField.text = "";
        InputField.Select();
    }

    private string WrapWithColorTag(Color _color, string _text){
        string s = "";

        s = "<color=#" + ColorUtility.ToHtmlStringRGB(_color) + ">" + _text + "</color>";

        return s;
    }

}
