using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Items {
    public class ItemUI : MonoBehaviour {
        [SerializeField] private Image _IFrame;
        [SerializeField] private Image _IIcon;
        [SerializeField] private TextMeshProUGUI _tStacks;
        [SerializeField] private GameObject _notif;

        public void Set(Item item) {
            this._IIcon.sprite = item.Data.Icon;
            this._tStacks.text = item.Stacks.ToString();
        }

        public void SetNotif(bool value) {
            this._notif.SetActive(value);
        }
    }
}