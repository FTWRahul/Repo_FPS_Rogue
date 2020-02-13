using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLootDropItem<T>
{
    public T item;
    
    // How many units the item takes - more units, higher chance of being picked
    public float probabilityWeight;
    
    // Displayed only as an information for the designer/programmer. Should not be set manually via inspector!    
    public float probabilityPercent;
    
    // These values are assigned via LootDropTable script. They represent from which number to which number if selected, the item will be picked.
    [HideInInspector] 
    public float probabilityRangeFrom;
    [HideInInspector] 
    public float probabilityRangeTo;  
}

public abstract class GenericLootDropTable<T, U> where T : GenericLootDropItem<U>
{
    // List where we'll assign the items.
    [SerializeField]	
    public List<T> lootDropItems;
 
    // Sum of all weights of items.
    float probabilityTotalWeight;

    public void ValidateTable()
    {
        // Prevent editor from "crying" when the item list is empty :)
        if (lootDropItems != null && lootDropItems.Count > 0)
        {
            float currentProbabilityWeightMaximum = 0f;

            // Sets the weight ranges of the selected items.
            foreach (T lootDropItem in lootDropItems)
            {

                if (lootDropItem.probabilityWeight < 0f)
                {
                    // Prevent usage of negative weight.
                    Debug.Log("You can't have negative weight on an item. Reseting item's weight to 0.");
                    lootDropItem.probabilityWeight = 0f;
                }
                else
                {
                    lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += lootDropItem.probabilityWeight;
                    lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
                }

            }

            probabilityTotalWeight = currentProbabilityWeightMaximum;

            // Calculate percentage of item drop select rate.
            foreach (T lootDropItem in lootDropItems)
            {
                lootDropItem.probabilityPercent = ((lootDropItem.probabilityWeight) / probabilityTotalWeight) * 100;
            }

        }
    }
    
    public T PickLootDropItem()
    {
        float pickedNumber = Random.Range(0, probabilityTotalWeight);
 
        // Find an item whose range contains pickedNumber
        foreach (T lootDropItem in lootDropItems)
        {
            // If the picked number matches the item's range, return item
            if(pickedNumber > lootDropItem.probabilityRangeFrom && pickedNumber < lootDropItem.probabilityRangeTo){
                return lootDropItem;
            }
        }	
 
        // If item wasn't picked... Notify programmer via console and return the first item from the list
        Debug.LogError("Item couldn't be picked... Be sure that all of your active loot drop tables have assigned at least one item!");
        return lootDropItems[0];
    }
}
