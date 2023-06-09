using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Instance
    private static SaveManager _instance;

    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("SaveManger is null");
            }

            return _instance;
        }
    }
    #endregion

    private List<NewBot> newBots = new List<NewBot>(); //  i should be saving these lists but since this is an excerise i create a file for each bot and item rather than appending 
    private List<NewItem> newItems = new List<NewItem>();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {

        Debug.Log("instance");
        StartCoroutine(LoadAll());

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveAll();
        }
    }


    #region Functions


    /// <summary>
    /// add all bots to list
    /// </summary>
    /// <param name="newBot"></param>
    public void RegisterBot(NewBot newBot)
    {
        if (!newBots.Contains(newBot))
        {
            newBots.Add(newBot);
        }
    }

    /// <summary>
    /// add all items to list
    /// </summary>
    /// <param name="newItem"></param>

    public void RegisterItem(NewItem newItem) //register to list
    {
        if (!newItems.Contains(newItem))
        {
            newItems.Add(newItem);
        }
    }


    /// <summary>
    /// Save all items and bots, typically this would be through an interface or an abstract class to make it more dymanamic and less hardcoded to specifics
    /// </summary>
    private void SaveAll()
    {
        foreach (NewItem item in newItems)
        {
            item.SaveItem();
        }

        foreach (NewBot bot in newBots)
        {
            bot.SaveBot();
        }
    }

    /// <summary>
    /// Load All items and bots, typically this would be through an interface or an abstract class to make it more dymanamic and less hardcoded to specifics
    /// </summary>
    IEnumerator LoadAll()
    {
        yield return new WaitForSeconds(1);

        foreach (NewItem item in newItems)
        {
            item.LoadItem();
        }

        foreach (NewBot bot in newBots)
        {
            bot.LoadBot();
        }
    }



    /// <summary>
    /// Handles saving 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="folderName"></param>
    /// <param name="fileName"></param>
    public void Save<T>(T data, string fileName)
    {

        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {

            // Serialize the data to XML
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, data);
            string xmlData = writer.ToString();

            // Write the XML data to a file and save locally
            File.WriteAllText(filePath, xmlData);

        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while saving file {fileName}: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles loading data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public T Load<T>(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Debug.Log($"File {fileName} does not exist.");
                return default(T);
            }

            // Read the XML data from the file
            string xmlData = File.ReadAllText(filePath);

            // Deserialize the XML data to the specified type
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xmlData);
            T loadedData = (T)serializer.Deserialize(reader);
            reader.Close();

            return loadedData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while loading file {fileName}: {ex.Message}");
            return default(T);
        }
    }



    #endregion
}
