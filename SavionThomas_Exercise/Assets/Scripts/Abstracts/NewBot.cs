using System.Collections;
using UnityEngine;

public abstract class NewBot : MonoBehaviour
{

    public abstract IEnumerator UpdateColor(); //ensures a function is available to change the color
    public abstract IEnumerator ResetColor(); //ensures a function is available to reset the color

    public abstract void SaveBot();
    public abstract void LoadBot();

}
