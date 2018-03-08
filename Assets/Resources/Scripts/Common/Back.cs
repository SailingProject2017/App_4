using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back : BaseObject {

    [SerializeField]
    private Button button;

    private void Start()
    {
        Singleton<GameInstance>.instance.IsBack = false;
        button = GetComponent<Button>();
    }

    public override void OnUpdate()
    {
        if (!Singleton<GameInstance>.instance.IsBack)
            button.interactable = false;
        else
            button.interactable = true;
    }


}
