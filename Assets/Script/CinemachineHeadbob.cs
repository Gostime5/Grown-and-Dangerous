using UnityEngine;
using Unity.Cinemachine; 

public class CinemachineHeadbob : MonoBehaviour
{
    //Script fait par IA
    //Ce script gère le headbob (petit mouvement de tête) de la caméra en fonction de la vitesse du joueur et de son état (marche ou course).
    [Header("Références")]
    [SerializeField] private CinemachineCamera vcam;

    [Header("Ajustements de la marche")]
    [SerializeField] private float amplitudeMarche = 1.8f;
    [SerializeField] private float frequenceMarche = 0.03f;

    [Header("Ajustements de la course")]
    [SerializeField] private float amplitudeCourse = 1.8f;
    [SerializeField] private float frequenceCourse = 0.05f;

    [Header("Ajustements de la respiration")]
    [SerializeField] private float amplitudeRespiration = 1.3f;
    [SerializeField] private float frequenceRespiration = 0.006f;

    [Header("Vitesse de transition")]
    [SerializeField] private float vitesseTransition = 5f;

    private CinemachineBasicMultiChannelPerlin vcamNoise;

    private void Start()
    {
        if (vcam != null)
        {
            // On récupère le composant de bruit de la caméra virtuelle
            vcamNoise = vcam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    //explication : cette fonction met à jour le mouvement de la caméra en fonction de la vitesse actuelle du joueur et de son état (au sol ou non).
    public void MettreAJourMouvement(float vitesseActuelle, bool estAuSol)
    {
        if (vcamNoise == null) return;

        float amplitudeCible = 0f;
        float frequenceCible = 0f;

        if (estAuSol)
        {
            
            // Si le joueur court (vitesse arbitraire supérieure à 5.5 m/s)
            if (vitesseActuelle > 5.5f)
            {
                amplitudeCible = amplitudeCourse;
                frequenceCible = frequenceCourse;
            }
            else if (vitesseActuelle > 2f) // Si le joueur marche (vitesse arbitraire supérieure à 2 m/s)
            {
                amplitudeCible = amplitudeMarche;
                frequenceCible = frequenceMarche;
            }
            else // mouvement de respiration quand le joueur est immobile (vitesse inférieure à 2 m/s)
            {
                amplitudeCible = amplitudeRespiration;
                frequenceCible = frequenceRespiration;
            }
        }

        // On applique une transition fluide (Lerp) pour éviter les secousses brusques
        //explication : on utilise Mathf.Lerp pour interpoler entre la valeur actuelle de l'amplitude et la valeur cible, en fonction du temps écoulé et de la vitesse de transition.
        //Cela permet d'obtenir un effet de transition fluide plutôt qu'un changement brusque.
        vcamNoise.AmplitudeGain = Mathf.Lerp(vcamNoise.AmplitudeGain, amplitudeCible, Time.deltaTime * vitesseTransition); 
        vcamNoise.FrequencyGain = Mathf.Lerp(vcamNoise.FrequencyGain, frequenceCible, Time.deltaTime * vitesseTransition);
    }
}