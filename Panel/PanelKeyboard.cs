using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelKeyboard : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
=======
>>>>>>> 90c4bc3 (1208)
    void Start()
    {
        CloseThisKeyBoard();
    }

    public void CloseThisKeyBoard()
    {
        this.gameObject.SetActive(false);
    }
<<<<<<< HEAD
=======

    public void ShowThisKeyBoard()
    {
        this.gameObject.SetActive(true);
    }
>>>>>>> 90c4bc3 (1208)
}
