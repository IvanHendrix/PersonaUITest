using Input;
using UnityEngine;

namespace UI
{
    public class UIMenuView : MonoBehaviour
    {
        protected void SendSelectedObject(GameObject selected)
        {
            UIControlModeSwitcher.Instance.SetDefaultSelectedObject(selected);
        }
    }
}