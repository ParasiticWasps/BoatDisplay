using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandItem : MonoBehaviour
{
    [SerializeField] private Text rankOrder;

    [SerializeField] private Text account;

    [SerializeField] private Text score;

    public void Setup(string order, string _account, string _score)
    {
        rankOrder.text = order;
        account.text = _account;
        score.text = _score;
    }
}
