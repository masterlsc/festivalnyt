using SharedModel.Company;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyListMono : MonoBehaviour
{
    public GameObject CompanyListPrefab;

    public void Load(List<CompanyModel> companies)
    {
        var clone = Instantiate(CompanyListPrefab, this.gameObject.GetComponent<RectTransform>());
        var script = clone.GetComponent<CompanyListPrefab>();
        script.AddCompanies(companies);
    }

}
