using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class YK_Selected : MonoBehaviour
{
    [SerializeField] private Image Title;
    [SerializeField] private Sprite NotSelect;
    [SerializeField] private Sprite Select;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
            Title.sprite = Select;
        else
            Title.sprite = NotSelect;
    }
}
