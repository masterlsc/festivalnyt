using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UIStyles
{
    [AddComponentMenu("UI Styles/Manager")]
    public class UIStylesManager : MonoBehaviour
    {
        /// <summary>
        /// Instance
        /// </summary>
        private static UIStylesManager instance_;
        public static UIStylesManager instance { get { return instance_; } }

        /// <summary>
        /// Delegate for data dropdowns
        /// </summary>
        /// <param name="optionData"></param>
        public delegate void OnGotData(List<Dropdown.OptionData> optionData);
        public static event OnGotData onGotData;

        /// <summary>
        /// Delegate for category dropdowns
        /// </summary>
        /// <param name="optionData"></param>
        public delegate void OnGotCategories(List<Dropdown.OptionData> optionData);
        public static event OnGotCategories onGotCategories;

        /// <summary>
        /// The applyMode to use, AllResources or ActiveInScene
        /// </summary>
        public ApplyMode applyMode = ApplyMode.ActiveInScene;

        /// <summary>
        /// List of all data files
        /// </summary>
        public List<StyleDataFile> dataList = new List<StyleDataFile>();

        /// <summary>
        /// Current data file index
        /// </summary>
        public int dataIndex = 0;

        /// <summary>
        /// Current category index
        /// </summary>
        public int categoryIndex = 0;

        /// <summary>
        /// The current loaded data file
        /// </summary>
        public StyleDataFile data
	    { get { return dataList[dataIndex]; } }
	    
        /// <summary>
        /// List of data OptionData for dropdowns
        /// </summary>
        public List<Dropdown.OptionData> dataOptionData;

        /// <summary>
        /// List of category OptionData for dropdowns
        /// </summary>
        public List<Dropdown.OptionData> CategoryOptionData;

        /// <summary>
        /// The cached objects
        /// </summary>
        public List<GameObject> cachedObjs = new List<GameObject>();

        /// <summary>
        /// cache the objects at start
        /// </summary>
        public bool cacheObjectsAtStart = false;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            // Set the instance and check theres only the one
            if (instance_ != null) Destroy(this);
            else instance_ = this;

            // Fill the data dropdowns
            FillDataDropdown();

            // Fill the categories dropdowns
            FillCategoryDropdown();

            // Cache the styles
            // CacheStyles();
        }

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            // Check if there is any data files found
            if (dataList.Count == 0)
                Debug.Log("No data files");

            if (cacheObjectsAtStart)
                CacheAllObjects();
        }

        /// <summary>
        /// Add object to cached objects list
        /// </summary>
        public void CacheObject(GameObject obj)
        {
            if (obj != null && !cachedObjs.Contains(obj))
                cachedObjs.Add(obj);
        }

        /// <summary>
        /// Find and cache all objectrs
        /// </summary>
        public void CacheAllObjects()
        {
            cachedObjs = StyleHelper.GetAllObjectsAssignedToStyles(dataList.ToArray(), applyMode);
        }
	    
	    
	    /// <summary>
	    /// Get the cached objects
	    /// </summary>
	    /// <returns></returns>
	    public GameObject[] GetCachedObjects ()
	    {
	    	return cachedObjs.ToArray();
	    }

        /// <summary>
        /// Apply the current category
        /// </summary>
        public void ApplyCurrentCategory()
        {
            StyleHelper.ApplyCurrentCategory(data, cachedObjs.ToArray());
        }

        /// <summary>
        /// Set the data file and 
        /// </summary>
	    public void SetDataFile(int index)
        {
            if (index < dataList.Count)
            {
	            dataIndex = index;
	            FillDataDropdown();
            }
            else Debug.LogError("index incorrect!");
        }

        /// <summary>
        /// Change the category
        /// </summary>
        /// <param name="newCategory"></param>
	    public void SetCategory(string newCategory)
        {
            if (data.categories.Contains(newCategory))
            {
                data.currentCategory = newCategory;
                StyleHelper.ApplyCurrentCategory(data, cachedObjs.ToArray());
            }
            else Debug.LogError(newCategory + "  invalid category");
        }

        /// <summary>
        /// Fill the data dropdown options
        /// </summary>
        public void FillDataDropdown()
        {
            dataOptionData = new List<Dropdown.OptionData>();
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i] != null)
                {
                    Dropdown.OptionData optionData = new Dropdown.OptionData();
                    optionData.text = dataList[i].name;
                    dataOptionData.Add(optionData);
                }
            }

            if (onGotData != null)
                onGotData(dataOptionData);
        }

        /// <summary>
        /// Fill the categories dropdown options
        /// </summary>
        public void FillCategoryDropdown()
        {
            if (dataList.Count > 0)
            {
                CategoryOptionData = new List<Dropdown.OptionData>();
                for (int i = 0; i < data.categories.Count; i++)
                {
                    if (!string.IsNullOrEmpty(data.categories[i]))
                    {
                        Dropdown.OptionData optionData = new Dropdown.OptionData();
                        optionData.text = data.categories[i];
                        CategoryOptionData.Add(optionData);
                    }
                }

                if (onGotCategories != null)
                    onGotCategories(CategoryOptionData);
            }
        }
    }
}




















