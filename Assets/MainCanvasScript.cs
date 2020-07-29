using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCanvasScript : MonoBehaviour
{
    public ToggleGroup TG;
    public List<MonoBehaviour> DrawerScripts = new List<MonoBehaviour>();
    // Update is called once per frame
    void Update()
    {
        //Gets the current toggle
        IEnumerator<Toggle> toggleEnum = TG.ActiveToggles().GetEnumerator();
        toggleEnum.MoveNext();
        string toggle = toggleEnum.Current.name;

        if (toggle == "Circle")
        {
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "CircleDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        else if (toggle == "Square")
        {
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "SquareDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
    }
}
