using System.Collections;
using System.IO;
using UnityEngine;

public class Bot : NewBot
{
    private string fileName = "bot";

    [SerializeField] private Color originalColor;
    [SerializeField] private Color newColor;

    private Vector3 currentPosition;

    private void Start()
    {
        SaveManager.Instance.RegisterBot(this);
        currentPosition = transform.position; //set current position
        GetComponent<MeshRenderer>().material.color = originalColor; //set start color
    }

    #region Color Functions
    public override IEnumerator UpdateColor()
    {
        yield return new WaitForSeconds(0f);
        gameObject.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public override IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0f);
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    #endregion

    #region Save Functions

    public override void SaveBot()
    {
        SaveManager.Instance.Save(currentPosition, fileName + transform.GetSiblingIndex() + ".xml");
    }

    public override void LoadBot()
    {
     
        string checkFile = fileName + transform.GetSiblingIndex() + ".xml";
        string filePath = Path.Combine(Application.persistentDataPath, checkFile);

        // Check if the file exists
        if (File.Exists(filePath))
        {
            transform.position = SaveManager.Instance.Load<Vector3>(fileName + transform.GetSiblingIndex() + ".xml");
        }
    }
    #endregion
}
