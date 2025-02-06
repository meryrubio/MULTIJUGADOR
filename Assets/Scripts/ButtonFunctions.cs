using Com.MyCompany.MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    //este un script de capa intermedia para que los gamemanager al cambiar de escena no se rayen, y los boyones puedan volver a funcionar de nuevoy asi no pierden la referencia, ya que no se van destruyendo.
    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }

    public void LoadCharacter(int CharacterIndex)
    {
        GameManager.instance.characterType = (Characters)CharacterIndex;
    }
    public void LoadScene(string sceneName)
    {
        GameManager.instance.LoadScene(sceneName);
    }
}