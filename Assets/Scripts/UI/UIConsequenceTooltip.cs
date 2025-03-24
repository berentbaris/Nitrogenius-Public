using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsequenceTooltip : MonoBehaviour
{
    public Sprite imageSprite;
    public Sprite EmiSprite;
    public Sprite EcoSprite;
    public Sprite ProdSprite;
    public Sprite HealthSprite;
    public Sprite EnvSprite;

    public Sprite adjust1;
    public Sprite adjust2;
    public Sprite adjust3;

    public Image icon;
    public Image amountIcon;

    public Color greenColor;
    public Color redColor;

    public void DisplayConsequence(int name, string amount)
    {
        amountIcon.transform.rotation = Quaternion.Euler(1, 1, 1);
        icon.sprite = null;
        amountIcon.sprite = null;
        amountIcon.color = Color.white;

        switch (name)
        {
            case 0:
                icon.sprite = imageSprite;
                break;
            case 1:
                icon.sprite = EmiSprite;
                break;
            case 2:
                icon.sprite = EcoSprite;
                break;
            case 3:
                icon.sprite = ProdSprite;
                break;
            case 4:
                icon.sprite = HealthSprite;
                break;
            case 5:
                icon.sprite = EnvSprite;
                break;
            
            default:
                break;
        }

        if (name == 0 || name == 2 || name == 3 || name == 4 || name == 5)
        {
            switch (amount)
            {
                case "+":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = greenColor;
                    break;
                case "++":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = greenColor;
                    break;
                case "+++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    break;
                case "++++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    break;
                case "-":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "--":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "---":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "----":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (amount)
            {
                case "+":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = redColor;
                    break;
                case "++":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = redColor;
                    break;
                case "+++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    break;
                case "++++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    break;
                case "-":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "--":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "---":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "----":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                default:
                    break;
            }
        }
    }
}