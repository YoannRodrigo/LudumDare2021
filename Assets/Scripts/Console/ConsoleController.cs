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

    [Header("Console apparition")]
    public float Duration;
    public float MovementValue;
    
    [SerializeField]
    private bool consoleLock = false;


    //Inputs
    public void OnConsole(){
        if(!consoleLock){
            SoundManager.PlaySFX("Open_Console");
            bool phase = !ConsolePanel.activeSelf;
            consoleLock = true;

            if(phase){
                ConsolePanel.SetActive(phase);
                MovementUtils.MoveX(this.gameObject.GetComponent<RectTransform>(), this.MovementValue, this.Duration, ActivateConsole);
            }else{
                MovementUtils.MoveX(this.gameObject.GetComponent<RectTransform>(), -this.MovementValue, this.Duration, DeactivateConsole);
            }
        }


    }

    public void ToggleLock(){
        this.consoleLock = !this.consoleLock;
    }

    public void DeactivateConsole(){
        ConsolePanel.SetActive(false);
        ToggleLock();
    }

    public void ActivateConsole(){
        this.ToggleLock();
        InputField.ActivateInputField();
    }

    public void OnConsoleSubmit(){
        string text = InputField.text;

        if(text != ""){
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
            InputField.ActivateInputField();
        }
    }
    private string WrapWithColorTag(Color _color, string _text){
        string s = "";

        s = "<color=#" + ColorUtility.ToHtmlStringRGB(_color) + ">" + _text + "</color>";

        return s;
    }

}
