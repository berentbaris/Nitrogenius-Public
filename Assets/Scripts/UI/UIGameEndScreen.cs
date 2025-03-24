using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameEndScreen : MonoBehaviour
{
    //national
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI GDPText;
    public TextMeshProUGUI happinessText;
    public NationalData natData;
    public SectorList sectorList;

    //sector
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI imageText;
    public TextMeshProUGUI NUEText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public Transform parentTransform;
    public GameObject RoleIconPrefab;

    public void DisplayEndGameScreen()
    {
        List<Sector> playerSectors = new List<Sector>();

        foreach (Sector sector in sectorList.list)
        {
            /*if (sector.controllerAgent == Controller.Player)
            {
                playerSectors.Add(sector);
            }
            */
            if (sector.ID != 4)
            {
                playerSectors.Add(sector);
            }
        }

        for (int i = 0; i < playerSectors.Count; i++)
        {
            GameObject obj = Instantiate(RoleIconPrefab);
            obj.transform.SetParent(parentTransform, false);
            UIGameEndRoleButton choice = obj.GetComponent<UIGameEndRoleButton>();
            choice.OnCreateButton(playerSectors[i], budgetText, imageText, NUEText, scoreText, healthText, sectorList);
            if (i == 0)
            {
                choice.OnSelect();
            }
        }


        if (natData._NationalItem._N2000_Below_Critical >= 74)
        {
            mainText.text = "Congratulations, you win! You brought " + Mathf.RoundToInt(natData._NationalItem._N2000_Below_Critical) + "% of nitrogen-sensitive Natura 2000 areas back below the critical deposition loads. The national target for 2035 was 74%.";
        }
        else
        {
            mainText.text = "Unfortunately, you have failed to meet the 2035 national nitrogen target. You brought " + Mathf.Floor(natData._NationalItem._N2000_Below_Critical) + "% of nitrogen-sensitive Natura 2000 areas back below the critical deposition loads. The national target for 2035 was 74%.";
        }

        //GDP per capita text
        if (natData._NationalItem._GDP_Per_Capita == natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            GDPText.text = "- GDP Per Capita remained the same";
        }
        else if (natData._NationalItem._GDP_Per_Capita > natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            float percentage = ((natData._NationalItem._GDP_Per_Capita - natData._NationalItem.GDP_Per_Capita_Record[0]) / natData._NationalItem.GDP_Per_Capita_Record[0]) * 100;
            GDPText.text = "- GDP Per Capita increased by " + Mathf.Floor(percentage) + "%";
        }
        else if(natData._NationalItem._GDP_Per_Capita < natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            float percentage = ((natData._NationalItem.GDP_Per_Capita_Record[0] -natData._NationalItem._GDP_Per_Capita) / natData._NationalItem.GDP_Per_Capita_Record[0]) * 100;
            GDPText.text = "- GDP Per Capita decreased by " + Mathf.Floor(percentage) + "%";
        }

        //happiness text
        if (natData._NationalItem._Happiness == natData._NationalItem.Happiness_Record[0])
        {
            happinessText.text = "- Citizen happiness remained the same";
        }
        else if (natData._NationalItem._Happiness > natData._NationalItem.Happiness_Record[0])
        {
            float percentage = ((natData._NationalItem._Happiness - natData._NationalItem.Happiness_Record[0]) / natData._NationalItem.Happiness_Record[0]) * 100;
            happinessText.text = "- Citizen happiness increased by " + Mathf.RoundToInt(percentage) + "%";
        }
        else if (natData._NationalItem._Happiness < natData._NationalItem.Happiness_Record[0])
        {
            float percentage = ((natData._NationalItem.Happiness_Record[0] - natData._NationalItem._Happiness) / natData._NationalItem.Happiness_Record[0]) * 100;
            happinessText.text = "- Citizen happiness decreased by " + Mathf.RoundToInt(percentage) + "%";
        }
    }


    public void OnPlayAgainButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}