using Com.MyCompany.MyGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{

    public TMP_Text victoryText;
    private string nextSceneName;

    private void Start()
    {
        victoryText.text = GameManager.instance.playerNames[0] + " WIN";
    }

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

    public void SetNextSceneName(string sceneName)
    {
        nextSceneName = sceneName;
    }

    public void AudioClip(AudioClip clip)
    {
        AudioSource audiosource = AudioManager.instance.PlayAudio(clip, "button");
        StartCoroutine(LoadSceneDelay(audiosource, clip, nextSceneName));
    }

    IEnumerator LoadSceneDelay(AudioSource src, AudioClip clip, string sceneName)
    {
        yield return new WaitForSeconds(clip.length);
        LoadScene(sceneName);
    }
}