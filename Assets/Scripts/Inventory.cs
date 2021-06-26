using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<PowerUps> items = new List<PowerUps>();

    // Start is called before the first frame update
    public void Add(PowerUps powerUp)
    {
        items.Add(powerUp);
    }

    public void Remove(PowerUps powerUp)
    {
        items.Remove(powerUp);

        if(onItemChangedCallback != null){
            onItemChangedCallback.Invoke();
        }
    }
}
