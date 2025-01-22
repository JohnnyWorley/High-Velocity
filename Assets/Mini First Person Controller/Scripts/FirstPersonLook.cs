using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonLook : MonoBehaviour
{
    public static FirstPersonLook instance;
    [SerializeField]
    Transform character;
    public Image interactPrompt;
    public TextMeshProUGUI interactPromptText;
    public TextMeshProUGUI actionText;
    public float sensitivity;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    public float sensX;
    public float sensY;
    GroundCheck gc;
    public LayerMask layerMask;
    


    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    private void Awake()
    {
        instance = this;
        transform.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("fov");
        sensitivity = PlayerPrefs.GetFloat("sens");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gc = FindObjectOfType<GroundCheck>();
    }
   

    Quaternion rot;
    Outline outline;

 
    private Vector2 currentRotation;
    float maxMinAngle = 2f;
    public float tiltAngle;
    float tiltTime = 6f;
    public bool aHeld;
    public bool dHeld;
    void Update()
    {
        KeyCode pickUpKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pickUpKeybind"));
        if (PauseScript.isPaused == false && GameManager.instance.isAlive)
        {

            if (gc.isGrounded)
            {
                if (Input.GetKey(KeyCode.A) && !dHeld)
                {
                    aHeld = true;
                    tiltAngle = Mathf.Lerp(tiltAngle, maxMinAngle, tiltTime * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D) && !aHeld)
                {
                    dHeld = true;
                    tiltAngle = Mathf.Lerp(tiltAngle, -maxMinAngle, tiltTime * Time.deltaTime);
                }

                if (Input.GetKeyUp(KeyCode.A))
                {
                    aHeld = false;
                }

                if (Input.GetKeyUp(KeyCode.D))
                {
                    dHeld = false;
                }

                if (!dHeld && !aHeld)
                {
                    tiltAngle = Mathf.Lerp(tiltAngle, 0, 8 * Time.deltaTime);
                    if (tiltAngle < 0.5f && tiltAngle > -0.5f)
                    {
                        tiltAngle = 0;
                    }
                }
            }
            else
            {
                tiltAngle = Mathf.Lerp(tiltAngle, 0, tiltTime * Time.deltaTime);
            }

            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);
            Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, tiltAngle);

            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);


            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.CompareTag("Collectable") && hit.distance < 3.5f)
                {
                    interactPromptText.text = PlayerPrefs.GetString("pickUpKeybind");
                    interactPrompt.gameObject.SetActive(true);
                    actionText.text = "Pick Up";
                    outline = hit.transform.GetComponent<Outline>();
                    outline.enabled = true;
                    if (Input.GetKeyDown(pickUpKeycode))
                    {
                        string weaponTag = hit.transform.gameObject.GetComponent<CollectableTag>().ItemTag;
                        WeaponsManager.instance.WeaponsPickUp(weaponTag);
                        Destroy(hit.transform.gameObject);
                        interactPrompt.gameObject.SetActive(false);
                    }

                }
                else if (hit.transform.CompareTag("Button") && hit.distance < 3.5f)
                {
                    actionText.text = "Press Button";
                    interactPromptText.text = PlayerPrefs.GetString("pickUpKeybind");
                    interactPrompt.gameObject.SetActive(true);
                    if (Input.GetKeyDown(pickUpKeycode))
                    {
                        hit.transform.GetComponent<ButtonScript>().ButtonPress();
                    }
                }
                else
                {
                    interactPrompt.gameObject.SetActive(false);
                    if (outline != null)
                    {
                        outline.enabled = false;
                    }
                }
            }
           
        }
    }
}
