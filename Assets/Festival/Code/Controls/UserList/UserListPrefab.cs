using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedModel;
using UnityEngine.UI;
using SharedModel.User;

public class UserListPrefab : MonoBehaviour
{

    public GameObject _prefabUserListItem;
    public RectTransform Content;
    public Text EmptyText;
    public int Childs = 0;

    private void Start()
    {
        UserListItemPrefab.ActionItemRemoved = ActionItemRemoved;
    }

    //public void AddUsers(List<Usermodel> users)
    //{
    //    foreach (var user in users)
    //    {
    //        AddUser(user);
    //    }
    //}

    public void AddUser(Usermodel user ,bool useAsResource)
    {
        var prefabinstance = Instantiate(_prefabUserListItem, Content);
        prefabinstance.GetComponent<RectTransform>().SetHeight(100);

        var prefabinstanceScript = prefabinstance.GetComponent<UserListItemPrefab>();
        prefabinstanceScript.LoadUser(user, useAsResource);

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
