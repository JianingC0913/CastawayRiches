using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button defaultButton;
    public Button swingButton;
    public TMP_Text text1;
    public TMP_Text text2;

    // Start is called before the first frame update
    void Start()
    {
        defaultButton.onClick.AddListener(DefaultClicked);
        swingButton.onClick.AddListener(SwingingClicked);
        if (PlayerPrefs.GetString("Mode") == "Swinging")
        {
            text2.fontStyle = FontStyles.Bold;
            text1.fontStyle = FontStyles.Normal;
        }
        else
        {
            text1.fontStyle = FontStyles.Bold;
            text2.fontStyle = FontStyles.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DefaultClicked()
    {
        PlayerPrefs.SetString("Mode", "Default");
        text1.fontStyle = FontStyles.Bold;
        text2.fontStyle = FontStyles.Normal;
    }

    void SwingingClicked()
    {
        PlayerPrefs.SetString("Mode", "Swinging");
        text2.fontStyle = FontStyles.Bold;
        text1.fontStyle = FontStyles.Normal;
    }
}
