using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public GameObject imgCollection;
    [SerializeField] public GameObject imgStamina;
    [SerializeField] public string description;
    [SerializeField] public float price;
    [SerializeField] public List<float> dataCard;
    [SerializeField] public bool _maxLevel;
    [SerializeField] public bool _isBought;
}
