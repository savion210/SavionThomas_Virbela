using UnityEngine;

[ExecuteAlways]
public class Player : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject botPrefab;

    [Header("Distance Handler")]
    [SerializeField] private DistanceHandler distanceHandler;

    [Header("Items Parent")]
    [SerializeField] private Transform itemParentObject; //Hold all the items and bots 

    [Header("Bot Object")]
    [SerializeField] private Transform botParentObject; //Hold all the items and bots 



    private Vector3 lastPosition; //player last position when not moving

    private void Start()
    {
        distanceHandler = GetComponent<DistanceHandler>();
        distanceHandler.CheckNearest();
        lastPosition = transform.position;
    }

    private void Update()
    {
        //due to being an exercise i am using the old input system rahter than going with events in the new. The new input system is better to use becasue it enforces well written code which is easier to maintain
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnRandomItem(itemPrefab);
            SpawnRandomBot(botPrefab);
        }

        // Check if the player's position has changed (should have used event on triggers rather than update which could be costly due to running distance formula)
        if (lastPosition != transform.position)
        {
            distanceHandler.CheckNearest();

            // Update the last position for the next frame
            lastPosition = transform.position;
        }

    }

    /// <summary>
    /// Spawn a new item at a random position
    /// </summary>
    /// <param name="objectPrefab"></param>
    private void SpawnRandomItem(GameObject objectPrefab) //bot spawn object could have been one generic object creation function
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-5, 30),
            Random.Range(-5, 30),
            Random.Range(-5, 30)
        );

        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        newObject.transform.SetParent(itemParentObject);//set the parent 
        newObject.name = "item";//set the name
        NewItem newItem = newObject.GetComponent<NewItem>(); //get the component
        distanceHandler.AddNewItem(newItem);

    }

    /// <summary>
    /// Spawn a new bot at a random position
    /// </summary>
    /// <param name="objectPrefab"></param>
    private void SpawnRandomBot(GameObject objectPrefab)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-5, 30),
            Random.Range(-5, 30),
            Random.Range(-5, 30)
        );

        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        newObject.transform.SetParent(botParentObject); //set the parent
        newObject.name = "bot"; //set the name
        NewBot newBot = newObject.GetComponent<NewBot>(); //get the component

        distanceHandler.AddNewBot(newBot);
    }
}




