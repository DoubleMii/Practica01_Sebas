using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [SerializeField] private bool shotLeft = false;
     [SerializeField] public float fuerza = 15f;
  //  [SerializeField] Bala Script;
    private Rigidbody rb;
    private InputSystem_Actions inputActions;
    Vector2 moveInput;




   void Awake()
    {  
        rb = GetComponent<Rigidbody>(); 
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
  
   //     inputActions.Player.Shoot.performed += ctx => OnBperformed();




    }



    void FixedUpdate () 
    {
        Vector3 movement = new Vector3(moveInput.x,0f, moveInput.y);
        rb.AddForce(movement * fuerza, ForceMode.Force);
        
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
  
  /*  void OnBperformed()
    {
        Script.GetBala();
        Debug.Log("shoot");
      
    }
  */

}


