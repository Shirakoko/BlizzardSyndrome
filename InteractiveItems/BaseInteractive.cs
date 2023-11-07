using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractive : MonoBehaviour
{
    protected bool isInteracted; // 是否已经交互过
    protected abstract void ShowPrompt();
    protected abstract void HidePrompt();
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}
