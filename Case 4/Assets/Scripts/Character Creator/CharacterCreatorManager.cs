﻿using LitJson;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterCreatorManager : MonoBehaviour
{
    #region Character creation references
    public GameObject CharacterCompletionPopup;
    public GameObject CharacterCreationMenu;
    public GameObject CharacterNameMenu;
    public GameObject CharacterNamePopupWindow;
    public GameObject CharacterNameErrorPopupWindow;
    public GameObject CharacterClothesSelectionErrorPopup;
    public GameObject CharacterClothesChangedErrorPopop;
    public GameObject SaveCharacterPopup;

    public TextMeshProUGUI CharacterSelectionErrorMessage;
    private string errorString;


    private string _currentBody;
    private string _currentFace;
    private string _currentHair;
    private string _currentTop;
    private string _currentBot;
    private string _currentShoes;

    private string _jsonWordsFilter;
    private List<string> _wordsFilter = new List<string>();
    #endregion
    
    void Start()
    {
        // When the game starts we extract all the bad words that we want to
        // filter out whenever the player is deciding on a character name.
        TextAsset filterWordsToJson = Resources.Load<TextAsset>("Default World Data/BadWords");
        JsonData filterWordsData = JsonMapper.ToObject(filterWordsToJson.text);

        for (int i = 0; i < filterWordsData["BadWords"].Count; i++)
        {
            _wordsFilter.Add(filterWordsData["BadWords"][i].ToString());
            //Debug.Log(_wordsFilter[i]);
        }

        DefineCurrentWearables();

        // If the player has already created a character then 
        // we just start the main menu instead.
        if (Character.Instance.CharacterCreation && SceneManager.GetActiveScene().name != "Character Customization")
        {
            SceneManager.LoadScene("Main Map");
            if (Character.Instance.TutorialCompleted)
            {
                //SceneManager.LoadScene("Main Map");
            } else
            {
                SceneManager.LoadScene("Tutorial Map Area");
            }
        }
    }

    //defining current player's appearances in order to later check if any has changed
    public void DefineCurrentWearables()
    {
        if (SceneManager.GetActiveScene().name == "Character Customization")
        {
            foreach (Clothing clothing in Character.Instance.Wearables)
            {
                if (clothing.Selected == true)
                {
                    if (clothing.BodyPart == "Body")
                    {
                        _currentBody = clothing.Name;
                    }
                    if (clothing.BodyPart == "Face")
                    {
                        _currentFace = clothing.Name;
                    }
                    else if (clothing.BodyPart == "Hair")
                    {
                        _currentHair = clothing.Name;
                    }
                    else if (clothing.BodyPart == "Top")
                    {
                        _currentTop = clothing.Name;
                    }
                    else if (clothing.BodyPart == "Bot")
                    {
                        _currentBot = clothing.Name;
                    }
                    else if (clothing.BodyPart == "Shoes")
                    {
                        _currentShoes = clothing.Name;
                    }
                }
            }
        }
    }

    #region Character creation functions
    public void ConfirmCharacter()
    {
        // Close/Open popup is meant to be used mainly for in-game popups but since it
        // does the same thing as closing one, we can reuse it for other UI as well.
        OpenWindow(CharacterNameMenu);
        CloseWindow(CharacterCompletionPopup);
        CloseWindow(CharacterCreationMenu);

        Character.Instance.RefreshWearables();
        //Debug.Log("Character has been created!");
    }

    //function for the Character Customization scene only
    public void ConfirmCharacterChanges()
    {
        Character.Instance.RefreshWearables();
        DefineCurrentWearables();
    }

    public void CheckForSelectedClothing()
    {
        bool bodySelected = false;
        bool hairSelected = false;
        bool faceSelected = false;
        bool topSelected = false;
        bool botSelected = false;
        bool shoesSelected = false;

        foreach(Clothing clothing in Character.Instance.Wearables)
        {
            if (clothing.BodyPart == "Body" &&
                clothing.Selected)
            {
                bodySelected = true;
            }
            if (clothing.BodyPart == "Hair" &&
                clothing.Selected)
            {
                hairSelected = true;
            }
            if (clothing.BodyPart == "Face" &&
                clothing.Selected)
            {
                faceSelected = true;
            }
            if (clothing.BodyPart == "Top" &&
                clothing.Selected)
            {
                topSelected = true;
            }
            if (clothing.BodyPart == "Bot" &&
                clothing.Selected)
            {
                botSelected = true;
            }
            if (clothing.BodyPart == "Shoes" &&
                clothing.Selected)
            {
                shoesSelected = true;
            }
        }
        
        if (bodySelected == false ||
            hairSelected == false ||
            faceSelected == false ||
            topSelected == false ||
            botSelected == false ||
            shoesSelected == false)
        {
            if(bodySelected == false)
            {
                errorString = errorString + "body, ";
            }
            if (hairSelected == false)
            {
                errorString = errorString + "hair, ";
            }
            if (faceSelected == false)
            {
                errorString = errorString + "face, ";
            }
            if (topSelected == false)
            {
                errorString = errorString + "top, ";
            }
            if (botSelected == false)
            {
                errorString = errorString + "bot, ";
            }
            if (shoesSelected == false)
            {
                errorString = errorString + "shoes, ";
            }

            CharacterSelectionErrorMessage.text = "You have not selected " + errorString.Substring(0, errorString.Length - 2) + ".";
            OpenWindow(CharacterClothesSelectionErrorPopup);
            errorString = string.Empty;
        } else
        {
            OpenWindow(CharacterNamePopupWindow);
        }
    }
    public void ReturnToMainMapAndCheck()
    {
        bool bodySelected = false;
        bool hairSelected = false;
        bool faceSelected = false;
        bool topSelected = false;
        bool botSelected = false;
        bool shoesSelected = false;

        foreach (Clothing clothing in Character.Instance.Wearables)
        {
            if (clothing.BodyPart == "Body" &&
                clothing.Selected)
            {
                bodySelected = true;
            }
            if (clothing.BodyPart == "Hair" &&
                clothing.Selected)
            {
                hairSelected = true;
            }
            if (clothing.BodyPart == "Face" &&
                clothing.Selected)
            {
                faceSelected = true;
            }
            if (clothing.BodyPart == "Top" &&
                clothing.Selected)
            {
                topSelected = true;
            }
            if (clothing.BodyPart == "Bot" &&
                clothing.Selected)
            {
                botSelected = true;
            }
            if (clothing.BodyPart == "Shoes" &&
                clothing.Selected)
            {
                shoesSelected = true;
            }
        }

        if (bodySelected == false ||
            hairSelected == false ||
            faceSelected == false ||
            topSelected == false ||
            botSelected == false ||
            shoesSelected == false)
        {
            if (bodySelected == false)
            {
                errorString = errorString + "body, ";
            }
            if (hairSelected == false)
            {
                errorString = errorString + "hair, ";
            }
            if (faceSelected == false)
            {
                errorString = errorString + "face, ";
            }
            if (topSelected == false)
            {
                errorString = errorString + "top, ";
            }
            if (botSelected == false)
            {
                errorString = errorString + "bot, ";
            }
            if (shoesSelected == false)
            {
                errorString = errorString + "shoes, ";
            }

            CharacterSelectionErrorMessage.text = "You have not selected " + errorString.Substring(0, errorString.Length - 2) + ".";
            OpenWindow(CharacterClothesSelectionErrorPopup);
            errorString = string.Empty;
        }
        else
        {
            CheckIfClothingChanged();
        }
    }

    //function that checks if any of the body parts were changed
    public void CheckIfClothingChanged()
    {
        bool _bodyChanged = false;
        bool _faceChanged = false;
        bool _hairChanged = false;
        bool _topChanged = false;
        bool _botChanged = false;
        bool _shoesChanged = false;
        foreach (Clothing clothing in Character.Instance.Wearables)
        {
            if (clothing.Selected == true)
            {
                if (clothing.BodyPart == "Body")
                {
                    if (clothing.Name != _currentBody)
                    {
                        _bodyChanged = true;
                    }
                }
                if (clothing.BodyPart == "Face")
                {
                    if(clothing.Name != _currentFace)
                    {
                        _faceChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Hair")
                {
                    if (clothing.Name != _currentHair)
                    {
                        _hairChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Top")
                {
                    if (clothing.Name != _currentTop)
                    {
                        _topChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Bot")
                {
                    if (clothing.Name != _currentBot)
                    {
                        _botChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Shoes")
                {
                    if (clothing.Name != _currentShoes)
                    {
                        _shoesChanged = true;
                    }
                }
            }
        }
        if(_bodyChanged == true ||
            _faceChanged == true ||
            _hairChanged == true ||
            _topChanged == true ||
            _botChanged == true ||
            _shoesChanged == true)
        {
            OpenWindow(CharacterClothesChangedErrorPopop);
        } else
        {
            ReturnToMainMap();
        }
    }

    //simply return to main map without any additional checks
    public void ReturnToMainMap()
    {
        Character.Instance.CharacterCreation = true;
        Character.Instance.RefreshJsonData();
        SceneManager.LoadScene("Main Map");
    }

    public void ConfirmCharacterName(Object obj)
    {
        GameObject inputField = obj as GameObject;
        string nameInput = inputField.GetComponent<InputField>().text;

        bool foundMatch = false;
        for (int i = 0; i < _wordsFilter.Count; i++)
        {
            // We look for a match in the current name the player has picked for his character
            // and if there is one, then an error will later be visualized and wont let him continue
            // until he corrects his name.
            Match match = Regex.Match(nameInput, @"(\b" + _wordsFilter[i] + @"|\B" + _wordsFilter[i] + @")",
                RegexOptions.IgnoreCase);

            if (match.Success && match.Length > 1)
            {
                foundMatch = true;
                //Debug.Log(nameInput);
                //Debug.Log(_wordsFilter[i]);
            }
        }

        if (foundMatch == false && nameInput.Length > 2 && nameInput.Length < 25)
        {
            Character.Instance.Name = nameInput;
            Character.Instance.CharacterCreation = true;

            // We update the player's name now that its confirmed as valid and then we call SetupWorldData
            // because once we load to a new scene with new gameo objects such as the
            // inventory and Quests diary, we want to re-initialize the data since the
            // scene before that did not contain those elements to put the data in.
            Character.Instance.RefreshJsonData();
            Character.Instance.SetupWorldData();
            if(SceneManager.GetActiveScene().name == "Begining Character Creation")
            {
                SceneManager.LoadScene("Tutorial Map Area");
            } else
            {
                GameObject.Find("Character Name Menu").SetActive(false);
            }
        }
        else
        {
            OpenWindow(CharacterNameErrorPopupWindow);
        }
    }

    public void ShowKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
    }
    
    //function for the save button of the character customization
    public void SaveButton()
    {
        bool _bodyChanged = false;
        bool _faceChanged = false;
        bool _hairChanged = false;
        bool _topChanged = false;
        bool _botChanged = false;
        bool _shoesChanged = false;
        foreach (Clothing clothing in Character.Instance.Wearables)
        {
            if (clothing.Selected == true)
            {
                if (clothing.BodyPart == "Body")
                {
                    if (clothing.Name != _currentBody)
                    {
                        _bodyChanged = true;
                    }
                }
                if (clothing.BodyPart == "Face")
                {
                    if (clothing.Name != _currentFace)
                    {
                        _faceChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Hair")
                {
                    if (clothing.Name != _currentHair)
                    {
                        _faceChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Top")
                {
                    if (clothing.Name != _currentTop)
                    {
                        _topChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Bot")
                {
                    if (clothing.Name != _currentBot)
                    {
                        _botChanged = true;
                    }
                }
                else if (clothing.BodyPart == "Shoes")
                {
                    if (clothing.Name != _currentShoes)
                    {
                        _shoesChanged = true;
                    }
                }
            }
        }
        if (_bodyChanged == true ||
            _faceChanged == true ||
            _hairChanged == true ||
            _topChanged == true ||
            _botChanged == true ||
            _shoesChanged == true)
        {
            OpenWindow(SaveCharacterPopup);
        }
        else
        {
            return;
        }
    }

    public void OpenWindow(Object obj)
    {
        GameObject windowObj = (GameObject)obj;
        windowObj.SetActive(true);
    }

    public void CloseWindow(Object obj)
    {
        GameObject windowObj = (GameObject)obj;
        windowObj.SetActive(false);
    }
    #endregion
}
