using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<CinemachineHeadbob>().MettreAJourMouvement(0f, true);
    }
}
