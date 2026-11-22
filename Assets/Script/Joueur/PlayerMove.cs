using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerStat playerStat;

    private CharacterController characterController;

    private Transform cameraTransform;
    [Header("Paramètre caméra")]
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float cameraVerticalRange = 85f;
    [SerializeField] private CinemachineHeadbob cinemachineHeadbob;
    [SerializeField] private CinemachineCamera vcam;

    [Header("Paramètre saut")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private  float verticalVelocity=0f;
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("Paramètre course")]
    [SerializeField] private float fovMarche;
    [SerializeField] private float fovCourse;
    [SerializeField] private bool isSprinting;







    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;       // bloquer le curseur au centre de l'écran
        Cursor.visible = false;                         // rendre le curseur invisible
    }

    
    void Update()
    {
        HandleMovement();
        HandleRotation();
        cinemachineHeadbob.MettreAJourMouvement(characterController.velocity.magnitude, characterController.isGrounded);
    }

    private void HandleMovement()
    {
        float forwardAxis = Input.GetAxisRaw("Vertical");  // Z et S
        float sideAxis = Input.GetAxisRaw("Horizontal");   // Q et D

        isSprinting = Input.GetKey(KeyCode.LeftShift) && forwardAxis > 0f; // on vérifie si le joueur appuie sur la touche de sprint et s'il avance

        float currentSpeed = isSprinting ? (playerStat.baseSpeed*playerStat.sprintMultiplier) + playerStat.speedModifier : playerStat.baseSpeed + playerStat.speedModifier; // on calcule la vitesse actuelle du joueur en fonction de s'il sprint ou non

        Vector3 horizontalMovement = (transform.forward * forwardAxis) + (transform.right * sideAxis);
        horizontalMovement= horizontalMovement.normalized*currentSpeed;

        FovChange();

        if (characterController.isGrounded)
        {
            verticalVelocity = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            }

        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }


        Vector3 finalMovement = horizontalMovement;
        finalMovement.y = verticalVelocity;

        characterController.Move(finalMovement*Time.deltaTime);                         // on dit au CharacterController de faire le mouvement
    }

    private void HandleRotation()
    {
        float xRotation = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * xRotation * mouseSensitivity * Time.deltaTime);


        float yRotation = Input.GetAxis("Mouse Y");
        yRotation = yRotation * mouseSensitivity * Time.deltaTime;
        verticalRotation -= yRotation;
        verticalRotation = Mathf.Clamp(verticalRotation, -cameraVerticalRange , cameraVerticalRange);
        //cameraTransform.localRotation = Quaternion.Euler(verticalRotation,0f,0f);     //j'ai modifié pour utiliser la caméra virtuelle de cinemachine pour les mouvements de tête
        vcam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void FovChange()
    {
        float targetFov = isSprinting ? fovCourse : fovMarche;

        //Camera.main.fieldOfView=Mathf.Lerp(Camera.main.fieldOfView, targetFov, Time.deltaTime * 7f);  //j'ai modifié pour utiliser la caméra virtuelle de cinemachine pour les mouvements de tête
        vcam.Lens.FieldOfView = Mathf.Lerp(vcam.Lens.FieldOfView, targetFov, Time.deltaTime * 7f);
    }
}
