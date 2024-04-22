using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : ScriptableObject
{
    [SerializeField] public string id;
    [SerializeField] public string name;
    [SerializeField] public GameObject imgCollection;
    [SerializeField] public GameObject imgStamina;
    [SerializeField] public string description;
    [SerializeField] public float price;
    [SerializeField] public List<float> dataCard;
    [SerializeField] public bool _maxLevel;
    [SerializeField] public bool _isBought;

    public string ID
    {
        get { return id; }
        set { id = value; }
    }



}
