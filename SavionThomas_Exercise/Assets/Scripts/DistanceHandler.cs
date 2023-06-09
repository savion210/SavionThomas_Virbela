using System.Collections.Generic;
using UnityEngine;

public class DistanceHandler : MonoBehaviour
{


    /// <summary>
    /// This could be more performant by using triggers and using distance checks from within the given radius instead of adding every single object to the list
    /// </summary>


    [Header("Items")]
    [SerializeField] private List<NewItem> items = new List<NewItem>();  // items within the radius (this could have been a dictionary for faster lookups)
    [Header("Bots")]
    [SerializeField] private List<NewBot> bots = new List<NewBot>(); // bots within the radius

    [Header("Items Parent")]
    [SerializeField] private Transform itemParentObject; //Hold all the items and bots 

    [Header("Bot Object")]
    [SerializeField] private Transform botParentObject; //Hold all the items and bots 

    private void Start()
    {
        AddAllBots();
        AddAllItems();
        CheckNearest();
    }


    #region Functions

    /// <summary>
    /// getting nearest whenever the player is moving, can be very expensive should have use events and triggers
    /// </summary> events would have shortened this function
    public void CheckNearest()
    {

        NewBot nearestBot = GetNearestBot();
        NewItem nearestItem = GetNearestItem();

        // Use the nearest bot and item as needed
        if (nearestBot != null)
        {
            StartCoroutine(nearestBot.UpdateColor()); //should be usuing events to change colors
        }

        if (nearestItem != null)
        {
            StartCoroutine(nearestItem.UpdateColor());
        }

        foreach (NewItem item in items) // reset item color that are not near
        {
            if (item != GetNearestItem())
            {
                StartCoroutine(item.ResetColor());
            }
        }

        foreach (NewBot bot in bots) // reset item color that are not near
        {
            if (bot != GetNearestBot())
            {
                StartCoroutine(bot.ResetColor());
            }
        }

    }


    /// <summary>
    /// add new bots to list
    /// </summary>
    /// <param name="newBot"></param>
    public void AddNewBot(NewBot newBot)
    {
        bots.Add(newBot);
    }


    /// <summary>
    /// add new items to list
    /// </summary>
    /// <param name="item"></param>
    public void AddNewItem(NewItem item)
    {
        items.Add(item);
    }

    /// <summary>
    /// add all curent items in parent to list
    /// </summary>
    private void AddAllItems()
    {
        foreach (NewItem item in itemParentObject.GetComponentsInChildren<NewItem>())
        {
            items.Add(item);
        }
    }

    /// <summary>
    /// Add all bots to the list
    /// </summary>
    private void AddAllBots()
    {
        foreach (NewBot bot in botParentObject.GetComponentsInChildren<NewBot>())
        {
            bots.Add(bot);
        }
    }


    #endregion

    #region Getters

    private NewBot GetNearestBot()
    {
        NewBot nearestBot = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Iterate over each bot in the list
        foreach (NewBot bot in bots)
        {
            // Calculate the distance between the current position and the bot's position
            float distance = Vector3.Distance(currentPosition, bot.transform.position);

            // Check if the current bot is closer than the previously found closest bot
            if (distance < closestDistance)
            {
                // Update the closest distance and nearest bot
                closestDistance = distance;
                nearestBot = bot;
            }

        }

        return nearestBot;
    }

    private NewItem GetNearestItem()
    {
        NewItem nearestItem = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Iterate over each item in the list
        foreach (NewItem item in items)
        {
            // Calculate the distance between the current position and the item's position
            float distance = Vector3.Distance(currentPosition, item.transform.position);

            // Check if the current item is closer than the previously found closest item
            if (distance < closestDistance)
            {
                // Update the closest distance and nearest item
                closestDistance = distance;
                nearestItem = item;
            }
        }

        return nearestItem;
    }
    #endregion

    #region Debuging
    private void OnDrawGizmos()
    {
        NewItem newItem = GetNearestItem();

        // If a nearest bot is found, draw the gizmo
        if (newItem != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, newItem.transform.position);
        }

        NewBot nearestBot = GetNearestBot();

        // If a nearest bot is found, draw the gizmo
        if (nearestBot != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nearestBot.transform.position);
        }
    }
    #endregion
}
