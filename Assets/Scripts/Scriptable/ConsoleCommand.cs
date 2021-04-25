using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "LudumDare2021/Console", order = 0)]
public class ConsoleCommand : ScriptableObject {
    public string Command;
    public string Response;
    public Color ResponseColor;

    public UnityEvent CallbackEvent;
}
