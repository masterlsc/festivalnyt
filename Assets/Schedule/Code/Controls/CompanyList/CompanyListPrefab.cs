using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedModel;
using UnityEngine.UI;
using SharedModel.Company;

public class CompanyListPrefab : MonoBehaviour
{

    public GameObject _prefabUserListItem;
    public RectTransform Content;
    public Text EmptyText;
    public int Childs = 0;

    private void Start()
    {
        CompanyListItemPrefab.ActionItemRemoved = ActionItemRemoved;
    }

    public void AddCompanies(List<CompanyModel> companies)
    {
        foreach (var company in companies)
        {
            AddCompany(company);
        }
    }

    public void AddCompany(CompanyModel company)
    {
        var prefabinstance = Instantiate(_prefabUserListItem, Content);
        prefabinstance.GetComponent<RectTransform>().SetHeight(100);

        var prefabinstanceScript = prefabinstance.GetComponent<CompanyListItemPrefab>();
        prefabinstanceScript.LoadCompany(company);

        Childs++;
        UpdateEmptyText();
    }

    public void ActionItemRemoved()
    {
        Childs--;
        UpdateEmptyText();
    }

    private void UpdateEmptyText()
    {
  
        if (Childs == 0)
        {
            EmptyText.gameObject.SetActive(true);
        }
        else
        {
            EmptyText.gameObject.SetActive(false);
        }

    }


}
