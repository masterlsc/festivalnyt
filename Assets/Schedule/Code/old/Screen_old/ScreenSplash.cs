//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Core
//{

//    public class ScreenSplash : MonoBehaviour
//    {
//        // Start is called before the first frame update
//        void Start()
//        {
//            StartCoroutine(StartApp());
//        }

//        private IEnumerator StartApp()
//        {
//            yield return new WaitForSeconds(2f);
//            //Try to autologin to backend if enabled. if login ok show home else show login screen.
//            this.GetComponentInParent<RootMaster>().EnableScreen(IdsData.Ids.screen_home_root);


//        }

//    }

//}



