using System;
using UnityEngine;

namespace Loot
{
    public class DropController : MonoBehaviour , IDropItem
    {
        // Loot drop table that contains items that can spawn
        public GenericLootDropTableGameObject lootDropTable;
	
        // How many items treasure will spawn
        public int numItemsToDrop;
        
        // Shouldn't be here
        private HealthState _healthState;

        private void Start()
        {
            _healthState = GetComponent<HealthState>();
            _healthState.onDeath.AddListener(Drop);
        }

        void OnValidate()
        {
            // Validate table and notify the programmer / designer if something went wrong.
            lootDropTable.ValidateTable();
        }

        public void Drop()
        {
            for (int i = 0; i < numItemsToDrop; i++)
            {
                GenericLootDropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
                
                if(selectedItem == null) return;
                
                GameObject selectedItemGameObject = Instantiate(selectedItem.item);
                /*selectedItemGameObject.transform.position = new Vector2(i/2f, 0f);*/
            }
        }
    }
}
