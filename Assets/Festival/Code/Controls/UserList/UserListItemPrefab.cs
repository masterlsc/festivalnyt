using SharedModel.User;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UserListItemPrefab : MonoBehaviour
{
    public Image Icon;
    public Text UserName;
    public Text UserEmail;

    [HideInInspector]
    public Usermodel User;
    [HideInInspector]
    public bool UseAsResource = false;

    public static Action ActionItemRemoved;

    void Start()
    {
        
    }

    public void LoadUser(Usermodel user, bool useAsResource)
    {
        User = user;
        UserName.text = user.FirstName + " " + user.LastName;
        UserEmail.text = user.Email;
    }

    public void ClickRemove()
    {
        Destroy(this.gameObject);
        ActionItemRemoved();
    }

  
}
