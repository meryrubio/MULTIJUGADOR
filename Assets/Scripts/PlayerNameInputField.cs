using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInputField : MonoBehaviour
{
    const string playerNameKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string playerName = "DEFAULT NAME";
        TMP_InputField _inputField =GetComponent<TMP_InputField>();

        if (_inputField)
        {
            if(PlayerPrefs.HasKey(playerNameKey)) //si los settings del usuario tiene la key(player name)
            {
                playerName = PlayerPrefs.GetString(playerNameKey);//si existe eso que ya nos ha dado el nombre
                _inputField.text = playerName;//lo guarda y recuerda el nombre para cuando vuelva a iniciar sesion
            }
        }

        PhotonNetwork.NickName = playerName; 
    }

    public void SetPlayerName()//ser capaces de introducir el nombre
    {
        TMP_InputField _inputField = GetComponent<TMP_InputField>();
        string playerName = _inputField.text;// lo que ha introducido el usuario

        if(!string.IsNullOrEmpty(playerName))//si no esta vacio
        {
            PlayerPrefs.SetString(playerNameKey, playerName);//se establece como nombre
            PhotonNetwork.NickName = playerName;
        }

    }
}
