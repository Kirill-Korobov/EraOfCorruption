using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    public void Consume(Food food);
    public void Drink(Potion potion);
}
