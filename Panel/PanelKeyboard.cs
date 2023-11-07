using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CloseThisKeyBoard();
    }

    public void CloseThisKeyBoard()
    {
        this.gameObject.SetActive(false);
    }
}
