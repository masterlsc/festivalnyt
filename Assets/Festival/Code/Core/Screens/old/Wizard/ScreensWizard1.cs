//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using TMPro;
//using UnityEngine.UI;
//using Michsky.UI.ModernUIPack;
//using static SharedModel.CustomerModel;
//using System;
//using SharedModel.User;
//using SharedModel.Company;

//public class ScreensWizard1 : ScreensMain
//{

//    public List<Pages> _pages;

//    public Button ButtonBackExit;

//    public TMP_InputField Page1InputBusinessName;
//    public TMP_InputField Page1InputExternalName;
//    public TMP_InputField Page1InputUserFirstname;
//    public TMP_InputField Page1InputUserlastname;
//    public TMP_InputField Page1InputuserEmail;
//    public TMP_InputField Page1InputuserPassword;
//    public Text Page1TextError;
//    public Dropdown Page1DropdownBusinessType;

//    public TMP_InputField Page2InputBusinessName;
//    public TMP_InputField Page2InputCompanyName;
//    public TMP_InputField Page2InputCompanyExternalId;
//    public GameObject page2CompanyListPrefab;

//    public Dropdown Page3DropdownCompany;
//    public TMP_InputField Page3InputUserFirstName;
//    public TMP_InputField Page3InputUserLastName;
//    public TMP_InputField Page3InputUserEmail;
//    public SwitchManager Page3SwitchAddAsResource;
//    public GameObject page3UserListPrefab;
//    public GameObject page3ButtonAddUser;

//    private WizardValues WizardValue;

//    private void Awake()
//    {
//        this.EnumId = Id.ScreensWizardCompanyPage1;
//    }

//    private void Start()
//    {
//        WizardValue = new WizardValues();
//        foreach (var page in _pages)
//        {
//            page.IsActive = page.pageNumber == 1;
//        }

//        Page1Load();
//    }

//    public void NextComplete()
//    {
//        var currentPage = GetCurrentPage();

//        if (currentPage.pageNumber == 1 && Page1Validate())
//        {
//            LoadNextPage();
//            Page2Load();
//        }
//        else if (currentPage.pageNumber == 2 && Page2Validate())
//        {
//            LoadNextPage();
//            Page3Load();
//        }
//        else if (currentPage.pageNumber == 3 && Page3Validate())
//        {
//            LoadNextPage();
//            Page4Load();
//        }

//    }

//    private void LoadNextPage()
//    {
//        var currentPage = GetCurrentPage();

//        if (currentPage.pageNumber == _pages.Count)
//        {
//            //Wizard complete
//            Destroy(this);
//        }
//        else
//        {
//            currentPage.IsActive = false;
//            currentPage.Page.SetActive(false);

//            var page = GetPage(currentPage.pageNumber + 1);
//            page.IsActive = true;
//            page.Page.SetActive(true);

//            SetBackExitButton();
//        }
//    }

//    public void PrevClose()
//    {
//        var currentPage = GetCurrentPage();

//        if (currentPage.pageNumber == 1)
//        {
//            //close wizard
//        }
//        else
//        {
//            currentPage.IsActive = false;
//            currentPage.Page.SetActive(false);

//            var page = GetPage(currentPage.pageNumber - 1);
//            page.IsActive = true;
//            page.Page.SetActive(true);

//            SetBackExitButton();
//        }
//    }

//    private void SetBackExitButton()
//    {
//        var currentPage = GetCurrentPage();

//        if (currentPage.pageNumber == 1)
//        {
//            ButtonBackExit.GetComponentInChildren<Text>().text = "Exit";
//        }
//        else
//        {
//            ButtonBackExit.GetComponentInChildren<Text>().text = "Back";

//        }
//    }

//    private Pages GetCurrentPage()
//    {
//        return _pages.Single(x => x.IsActive);
//    }

//    private Pages GetPage(int pageNumber)
//    {
//        return _pages.Single(x => x.pageNumber == pageNumber);
//    }

//    #region Page 1

//    private bool Page1Validate()
//    {
//        if (Page1DropdownBusinessType.options[Page1DropdownBusinessType.value].text.Equals("Not set"))
//        {
//            Page1TextError.text = "Select a company type!";
//            return false;
//        }

//        if (string.IsNullOrEmpty(Page1InputBusinessName.text))
//        {
//            Page1TextError.text = "Enter a name for your business!";
//            return false;
//        }

//        if (string.IsNullOrEmpty(Page1InputUserFirstname.text))
//        {
//            Page1TextError.text = "Enter first name!";
//            return false;
//        }

//        if (string.IsNullOrEmpty(Page1InputUserlastname.text))
//        {
//            Page1TextError.text = "Enter last name!";
//            return false;
//        }

//        if (string.IsNullOrEmpty(Page1InputuserEmail.text))
//        {
//            Page1TextError.text = "Enter a email!";
//            return false;
//        }

//        if (string.IsNullOrEmpty(Page1InputuserPassword.text))
//        {
//            Page1TextError.text = "Enter a password!";
//            return false;
//        }

 
//        var customerTypes = (CustomerTypes)Enum.Parse(typeof(CustomerTypes), Page1DropdownBusinessType.options[Page1DropdownBusinessType.value].text);
//        //WizardValue.Customer = new SharedModel.CustomerModel(Page1InputBusinessName.text, Page1InputExternalName.text, customerTypes);
//        //WizardValue.UserAdministrator = new SharedModel.Usermodel(0, 0, "", Page1InputUserFirstname.text, Page1InputUserlastname.text, Page1InputUserFirstname.text + " " + Page1InputUserlastname.text, SharedModel.Usermodel.UserTypes.Admin.ToString(), Page1InputuserEmail.text, "Username");


//        return true;


//    }

//    public void Page1Load()
//    {
//        var options = new List<Dropdown.OptionData>();
//        options.Add(new Dropdown.OptionData("Select type of business"));

//        foreach (var item in Enum.GetNames(typeof(CustomerTypes)))
//        {
//            options.Add(new Dropdown.OptionData(item));
//        }


//        Page1DropdownBusinessType.AddOptions(options);

//    }

//    #endregion

//    #region Page 2

//    public void Page2Load()
//    {
//        Page2InputBusinessName.text = Page1InputBusinessName.text;
//    }

//    public void Page2ClickAddCompany()
//    {
//        if (!string.IsNullOrEmpty(Page2InputCompanyName.text))
//        {
//            //add company to prefab list
//            var companyListPrefab = page2CompanyListPrefab.GetComponentInChildren<CompanyListPrefab>();
//            companyListPrefab.AddCompany(new CompanyModel(Page2InputCompanyExternalId.text, Page2InputCompanyName.text, 1));

//            Page2InputCompanyExternalId.text = "";
//            Page2InputCompanyName.text = "";
//        }
//        else
//        {
//            Debug.Log("Page2InputCompanyName not set!");
//            //please fill out company name
//        }
//    }

//    private bool Page2Validate()
//    {
//        //page2 prefab list must contain atleast 1 company
//        var companyListPrefab = page2CompanyListPrefab.GetComponentInChildren<CompanyListPrefab>();
//        if (companyListPrefab.Content.childCount > 0)
//        {
//            return true;
//        }
//        else
//        {
//            Debug.Log("No companies added!");
//            return false;
//        }

//    }
//    #endregion

//    #region Page 3

//    public void Page3Load()
//    {
//        var companyListPrefab = page2CompanyListPrefab.GetComponentInChildren<CompanyListPrefab>();
//        var options = new List<Dropdown.OptionData>();
//        foreach (Transform transformChild in companyListPrefab.Content.transform)
//        {
//            //add to dropdown
//            var companyListItemPrefab = transformChild.GetComponent<CompanyListItemPrefab>();
//            string companyName = companyListItemPrefab.CompanyName.text;
//            options.Add(new Dropdown.OptionData(companyName));
//            Debug.Log(companyName);
//        }
//        Page3DropdownCompany.AddOptions(options);
//    }

//    public void Page3ClickAddUser()
//    {
//        if (!string.IsNullOrEmpty(Page3InputUserFirstName.text) && !string.IsNullOrEmpty(Page3InputUserLastName.text) && !string.IsNullOrEmpty(Page3InputUserEmail.text))
//        {
//            //add company to prefab list
//            var companyListPrefab = page3UserListPrefab.GetComponent<UserListPrefab>();
//            //TODO: Fix userModel, weird tenantId why????
//            //companyListPrefab.AddUser(new Usermodel(
//            //        0, 
//            //        0, 
//            //        "externalId", 
//            //        Page3InputUserFirstName.text + " " + Page3InputUserLastName.text, 
//            //        Page3InputUserLastName.text, Page3InputUserFirstName.text, 
//            //        Usermodel.UserTypes.customer_user.ToString(), 
//            //        Page3InputUserEmail.text, "random username"), 
//            //        Page3SwitchAddAsResource.isOn
//            //        );

//            Page3InputUserLastName.text = "";
//            Page3InputUserFirstName.text = "";
//            Page3InputUserEmail.text = "";
//        }
//        else
//        {
//            Debug.Log("Please enter all info!");
//        }
//    }

//    private bool Page3Validate()
//    {

//        var userListPrefab = page3UserListPrefab.GetComponentInChildren<UserListPrefab>();
//        if (userListPrefab.Content.childCount > 0)
//        {
//            return true;
//        }
//        else
//        {
//            Debug.Log("No companies added!");
//            return false;
//        }
//    }

//    #endregion

//    #region Page 4

//    public void Page4Load()
//    {

//    }

//    private bool Page4Validate()
//    {

//        return false;
//    }

//    #endregion


//    [System.Serializable]
//    public class Pages
//    {
//        public int pageNumber;
//        public GameObject Page;
//        [HideInInspector]
//        public bool IsActive;
//    }

//    public class WizardValues
//    {
//        public SharedModel.CustomerModel Customer;
//        public SharedModel.User.Usermodel UserAdministrator;

//        public List<SharedModel.Company.CompanyModel> companies;

//        public Dictionary<SharedModel.Company.CompanyModel, SharedModel.User.Usermodel> CompanyUsers;


//        public WizardValues()
//        {

//        }


//    }





//}
