﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceDisplay : MonoBehaviour
{
    public GameObject IconPrefab;
    [Space(10)]
    public List<Clothing> Bodies;
    public GameObject BodiesDisplay;
    [Space(10)]
    public List<Clothing> Faces;
    public GameObject FacesDisplay;
    [Space(10)]
    public List<Clothing> Hairs;
    public GameObject HairsDisplay;
    [Space(10)]
    public List<Clothing> Tops;
    public GameObject TopsDisplay;
    [Space(10)]
    public List<Clothing> Bots;
    public GameObject BotsDisplay;
    [Space(10)]
    public List<Clothing> Shoes;
    public GameObject ShoesDisplay;

    public List<GameObject> Buttons;
    public List<GameObject> Displays;

    private GameObject _bodyBodyPart;
    private GameObject _hairBodyPart;
    private GameObject _faceBodyPart;
    private GameObject _topBodyPart;
    private GameObject _botBodyPart;
    private GameObject _shoesBodyPart;
    private Sprite[] _spritesFromStorage;

    public Sprite UnSelectedButton;
    public Sprite SelectedButton;

    public Animator PanelAnimator;

    private void Start()
    {
        _spritesFromStorage = Resources.LoadAll<Sprite>("Clothing");

        _bodyBodyPart = GameObject.Find("Body Body Part");
        _hairBodyPart = GameObject.Find("Hair Body Part");
        _faceBodyPart = GameObject.Find("Face Body Part");
        _topBodyPart = GameObject.Find("Top Body Part");
        _botBodyPart = GameObject.Find("Bot Body Part");
        _shoesBodyPart = GameObject.Find("Shoes Body Part");

        SetupDisplays();
        LoadCharacterAppearance();
    }

    // Whenever the player clicks on a button to view a body type's elements
    // this will make it so that its list of buttons will be hidden and viewed
    // whenever he clicks on the body type button.
    public void ToggleDisplay(Object display)
    {
        PanelAnimator.SetBool("IsOpen", true);
        GameObject displayObj = (GameObject)display;
        foreach(GameObject _display in Displays)
        {
            if(_display != displayObj)
            {
                _display.SetActive(false);
            }
        }
        if(displayObj.activeSelf)
        {
            displayObj.SetActive(false);
            PanelAnimator.SetBool("IsOpen", false);
        } else
        {
            displayObj.SetActive(true);
        }
    }

    public void ToggleQuestDisplay(Object display)
    {
        GameObject displayObj = (GameObject)display;
        displayObj.SetActive(!displayObj.activeSelf);
    }

    public void ToggleButton(Object button)
    {
        GameObject buttonObj = (GameObject)button;
        foreach(GameObject _button in Buttons)
        {
            if(_button != buttonObj)
            {
                _button.GetComponent<Image>().sprite = UnSelectedButton;
            }
        }
        if(buttonObj.GetComponent<Image>().sprite == SelectedButton)
        {
            buttonObj.GetComponent<Image>().sprite = UnSelectedButton;
        } else
        {
            buttonObj.GetComponent<Image>().sprite = SelectedButton;
        }
        
    }

    // This function creates the icons in the body tab's lists (displays) using the
    // predefined wearables from the wearables json processed by the player.
    private void SetupDisplays()
    {
        foreach (Clothing clothing in Character.Instance.Wearables)
        {
            switch (clothing.BodyPart)
            {
                case "Body":
                    {
                        LoadIcon(clothing, BodiesDisplay, Bodies);
                        break;
                    }
                case "Face":
                    {
                        LoadIcon(clothing, FacesDisplay, Faces);
                        break;
                    }
                case "Hair":
                    {
                        LoadIcon(clothing, HairsDisplay, Hairs);
                        break;
                    }
                case "Top":
                    {
                        LoadIcon(clothing, TopsDisplay, Tops);
                        break;
                    }
                case "Bot":
                    {
                        LoadIcon(clothing, BotsDisplay, Bots);
                        break;
                    }
                case "Shoes":
                    {
                        LoadIcon(clothing, ShoesDisplay, Shoes);
                        break;
                    }
            }
        }
    }

    // This retrieves the icons from the storage and assigns them to buttons in the
    // appearance selection tabs on the right.
    private void LoadIcon(Clothing item, GameObject display, List<Clothing> listOfItems)
    {
        if (display != null)
        {
            listOfItems.Add(item);
            GameObject newIcon = Instantiate(IconPrefab, display.transform.GetChild(0).transform);
            newIcon.GetComponent<AppearanceSelector>().PortraitImage = item.PortraitImage;
            newIcon.GetComponent<AppearanceSelector>().BodyPart = item.BodyPart;

            Sprite sprite = Resources.Load<Sprite>("Icons/" + item.Icon);
            newIcon.GetComponent<Image>().sprite = sprite;
        }
    }

    // This imports the appearance elements according to the body parts and selected
    // appearance elements from the player and it will load them into the player's
    // character view.
    public void LoadCharacterAppearance()
    {
        foreach (Clothing clothing in Character.Instance.Wearables)
        {
            if (clothing.Selected)
            {
                foreach (Sprite sprite in _spritesFromStorage)
                {
                    if (sprite.name == clothing.PortraitImage)
                    {
                        // More to add...
                        switch (clothing.BodyPart)
                        {
                            case "Body":
                                 _bodyBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                            case "Hair":
                                  _hairBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                            case "Face":
                                _faceBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                            case "Top":
                                _topBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                            case "Bot":
                                _botBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                            case "Shoes":
                                _shoesBodyPart.GetComponent<Image>().sprite = sprite;
                                break;
                        }
                    }
                }
            }
        }
    }
}
