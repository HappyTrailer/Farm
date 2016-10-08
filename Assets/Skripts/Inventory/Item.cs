using UnityEngine;
using System.Collections;

public interface Item
{
    int Id { get; set; }
    int ItemPrice { get; set; }
    string ItemName { get; set; }
    string ItemType { get; set; }
    string SpritePath { get; set; }
    int ItemCount { get; set; }

    void Sale();
    void Drop();
}
