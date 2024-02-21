//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//namespace Core
//{
//    public class ScreenMono : MonoBehaviour
//    {
//        public GameObject Parent;

//        private void OnEnable()
//        {
//            if (this.GetComponentInParent<RootMaster>().GetComponentInChildren<MyHeader>(true) != null)
//            {
//                ScreenData data = ScreenDataContainer.GetInstance().Screens[this.GetComponent<IdsMono>().mIdsData.mIds];
//                this.GetComponentInParent<RootMaster>().GetComponentInChildren<MyHeader>(true).ShowBackButton(data.ShowBackButton);
//                this.GetComponentInParent<RootMaster>().GetComponentInChildren<MyHeader>(true).ShowDrawerButton(!data.ShowBackButton);
//            }
//        }
//    }
//}

