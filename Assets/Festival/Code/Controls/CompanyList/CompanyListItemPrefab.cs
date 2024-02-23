using SharedModel.Company;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyListItemPrefab : MonoBehaviour
{
    public Image Icon;
    public Text CompanyName;

    public static Action ActionItemRemoved;

    void Start()
    {
        
    }



    public void LoadCompany(CompanyModel company)
    {
        CompanyName.text = company.Name;
    }

    public void ClickRemove()
    {
        ActionItemRemoved();
        Destroy(this.gameObject);
      
    }

  
}
