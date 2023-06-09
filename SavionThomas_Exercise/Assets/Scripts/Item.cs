using System.Collections;
using System.IO;
using UnityEngine;

public class Item : NewItem
{
    private string fileName = "Item";

    [SerializeField] private Color originalColor;
    [SerializeField] private Color newColor;
    private Vector3 currentPosition;

    private void Start()
    {
        SaveManager.Instance.RegisterItem(this); // register to list to avoid over inheritance all over the place
        currentPosition = transform.position; //set current position
        GetComponent<MeshRenderer>().material.color = originalColor; //set start color
    }


    #region Color Functions
    public override IEnumerator UpdateColor()
    {
        yield return new WaitForSeconds(0.1f); //gave delays to ensure only one color is chaning at a time per item and bot
        gameObject.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public override IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }


    #endregion

    #region Save Functions

    public override void SaveItem()
    {
        SaveManager.Instance.Save(currentPosition, fileName + transform.GetSiblingIndex() + ".xml"); //should be saving entire list, things like get child is bad practice
    }

    public override void LoadItem()
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
