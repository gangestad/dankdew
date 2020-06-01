using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerStatus status;
    public Image foregroundImage;
    // Start is called before the first frame update
    void Start()
    {
        foregroundImage = GetComponent<Image>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        foregroundImage.fillAmount = (status.GetStamina()/status.GetMaxStamina());
    }
}
