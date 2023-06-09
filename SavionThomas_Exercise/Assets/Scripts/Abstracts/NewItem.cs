using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewItem : MonoBehaviour
{

    public abstract IEnumerator UpdateColor();  //ensures a function is available to change the color
    public abstract IEnumerator ResetColor(); //ensures a function is available to reset the color

    public abstract void SaveItem();
    public abstract void LoadItem();

}
