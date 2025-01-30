using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ChangeScene : MonoBehaviour
{
    public void ChangeTheFuckingScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
