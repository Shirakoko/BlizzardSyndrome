using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrompt // 具有提示的物体的接口
{
    void ShowPrompt();
    void HidePrompt();
    void OnTriggerEnter(Collider other);
    void OnTriggerExit(Collider other);
}