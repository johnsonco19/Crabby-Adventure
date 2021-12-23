﻿using UnityEngine;
using System.Collections;

public class Player : Character
{
    // Used to get a reference to the prefab
    public Inventory inventoryPrefab;

    // A copy of the inventory prefab
    Inventory inventory;

    // Used to get a reference to the prefab
    public HealthBar healthBarPrefab;

    // A copy of the health bar prefab
    HealthBar healthBar;

    // Start is called when the script is activated
    private void OnEnable()
    {
        ResetCharacter();
    }


    // Called when player's collider touches an "Is Trigger" collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Retrieve the game object that the player collided with, and check the tag
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            // Grab a reference to the Item (scriptable object) inside the Consumable class and assign it to hitObject
            // Note: at this point it is a coin, but later may be other types of CanBePickedUp objects
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            // Check for null to make sure it was successfully retrieved, and avoid potential errors
            if (hitObject != null)
            {
                // debugging
                print("it: " + hitObject.objectName);

                // indicates if the collision object should disappear
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        // coins will disappear if they can be added to the inventory
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;

                    case Item.ItemType.HEALTH:
                        // hearts should disappear if they adjust the player's hit points
                        // when health meter is full, hearts aren't picked up and remain in the scene
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;
                }

                // Hide the game object in the scene to give the illusion of picking up
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        // Don't increase above the max amount
        if (hitPoints < maxHitPoints)
        {
            hitPoints = hitPoints + amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
            return true;
        }

        // Return false if hit points is at max and can't be adjusted
        return false;
    }

    public override void ResetCharacter()
    {
        // Get a copy of the inventory prefab and store a reference to it
        inventory = Instantiate(inventoryPrefab);

        // Start the player off with the starting hit point value
        hitPoints = startingHitPoints;

        // Get a copy of the health bar prefab and store a reference to it
        healthBar = Instantiate(healthBarPrefab);

        // Set the healthBar's character property to this character so it can retrieve the maxHitPoints
        healthBar.character = this;
    }
}
