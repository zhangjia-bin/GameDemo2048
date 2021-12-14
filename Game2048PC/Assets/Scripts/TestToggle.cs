using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    public List<Toggle> toggles = new List<Toggle>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener((bool isOn) => { SelectBu(toggle, isOn); });
        }



        //for (int i = 0; i < toggles.Count; i++)
        //{
        //    toggles[i].onValueChanged.AddListener((bool isOn) => { SelectBu(toggles[i], isOn);});
        //}
    }

    private void SelectBu(Toggle isToggle, bool isOn)
    {
        print(isToggle.name);
        if (isOn)
        {
            print(isToggle.transform.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
