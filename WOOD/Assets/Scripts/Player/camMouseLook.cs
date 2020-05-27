using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothView;

    [Header("Variables")]
    // Variables de Sensiilité et de Smoothing
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    // Le Character
    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Mouvements de la camera
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothView.x = Mathf.Lerp(smoothView.x, md.x, 1f / smoothing) ;
        smoothView.y = Mathf.Lerp(smoothView.y, md.y, 1f / smoothing);
        mouseLook += smoothView;

        // Clamp la view
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
