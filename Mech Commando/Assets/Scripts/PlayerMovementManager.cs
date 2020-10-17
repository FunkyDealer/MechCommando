using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    CharacterController controller;
    Camera cam;
    readonly float runSpeed = 50;
    readonly float walkSpeed = 25;
    readonly float sprintSpeed = 100;
    float currentSpeed;
    float jumpPower = 2;
    Player player;
    Quaternion originalRotation;
    Vector3 dir;
    bool jumping; //Jumping

    [SerializeField]
    float gravityScale = 0.8f; //Gravity's Scale

    //Dodge Things
    bool dodging;
    bool canDodge;
    float dodgeCoolDown; //CoolDown timer
    [SerializeField]
    float dodgeCoolDownTime = 2; //Time before being able to dodge Again
    Vector3 dodgeVector_;
    float dodgePotency_;
    float inpactSmoothFactor = 4;
    [SerializeField]
    float dodgeSpeed = 60;
    //Dodge Button
    enum DodgeButtonState
    {
        none = 0,
        Right1 = 1,
        Right2 = 2,
        Left1 = 3,
        Left2 = 4
    }
    DodgeButtonState dodgeButtonState;
    float dodgeButtonTimer;
    Vector3 dodgeDir;
    readonly float dodgeButtonTime = 0.2f;

    //sprint
    float EnergySpendingTimer;
    [SerializeField]
    readonly float EnergySpendingTime = 0.05f;

    // awake is the first thing to be called
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        jumping = false;
        dodging = false;
        canDodge = true;
        dodgeCoolDown = 0;
        dodgeButtonTimer = 0;
        dodgeDir = Vector3.zero;
        EnergySpendingTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        currentSpeed = runSpeed;
        dir = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAlive())
        {
            modifiers(); //Run, walk and sprint speed
            if (currentSpeed == sprintSpeed)
            {
                if (player.currentEnergy <= 0)
                {
                    currentSpeed = walkSpeed;
                    EnergySpendingTimer = 0;
                }
                else
                {
                    if (EnergySpendingTimer < EnergySpendingTime) EnergySpendingTimer += Time.deltaTime;
                    else
                    {
                        player.spendEnergy(player.currentEnergy - 1);
                        EnergySpendingTimer = 0;
                    }
                }
            }

            Movement(); //Main direction Calculations

            moveCharacter(dir); //Movement Calculations
            
            //Dodging Timer
            if (!canDodge && dodgeCoolDown < dodgeCoolDownTime) dodgeCoolDown += Time.deltaTime;
            else if (!canDodge && dodgeCoolDown > dodgeCoolDownTime)
            {
                dodgeCoolDown = 0;
                canDodge = true;
            }

        }
        else
        {
            dir.x = 0; dir.z = 0;
        }



    }

    void LateUpdate() //Runs after all other update functions
    {

        if (dodging) //Dodging
        {
            Dodge();
        }
    }

    void Movement()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
        float dirY = dir.y;
        dir = transform.right * input.x + transform.forward * input.y;
        dir.y = dirY;

        if (controller.isGrounded) //Check if playing is on the ground
        {
            jumping = false;
            dir.y = -1;

            if (Input.GetButtonDown("Jump")) //Jumping when space is pressed
            {
                Jump();
            }

        }
        else
        {
            dir.y += (Physics.gravity.y * gravityScale * Time.deltaTime); //Gravity is applied if not grounded
        }


       


        if (player.inControl && Input.GetButtonDown("Dodge") && canDodge) //Dodge Code 
            {
                if (player.currentEnergy - 50 >= 0) startDodge(dir);

        }
    }

    //Run, Walk and Sprint Modifiers
    void modifiers()
    {
            if (Input.GetButton("Sprint")) currentSpeed = sprintSpeed;
            else if (Input.GetButton("Walk") && controller.isGrounded) currentSpeed = walkSpeed;
            else currentSpeed = runSpeed;

    }

    //Movement Calculations
    void moveCharacter(Vector3 d)
    {

        controller.Move(new Vector3(
        d.x * currentSpeed * Time.deltaTime,
        0,
        d.z * currentSpeed * Time.deltaTime
        ));                             

        if (!dodging) { //Gravity
            controller.Move(new Vector3(
            0,
            d.y * runSpeed * Time.deltaTime,
            0));
        }

    }

    //Jumping
    void Jump()
    {
        dir.y = jumpPower;
        jumping = true;
    }


    #region Dodge
    //-----------------------------------------Dodging--------------------------------------------
    void startDodge(Vector3 dir)
    {
        Vector3 dodgeDir;
        if ((dir.x != 0 || dir.z != 0)) dodgeDir = new Vector3(dir.x, 0, dir.z);
        else dodgeDir = new Vector3(-transform.forward.x, 0, -transform.forward.z); //In case of no input, character goes backwards

        dodging = true;
        player.inControl = false;

        dodgeDir.Normalize();
        dodgeVector_ = dodgeDir * dodgeSpeed;
        dodgePotency_ = dodgeVector_.sqrMagnitude;
        //Debug.Log(dodgeVector_.sqrMagnitude);
        canDodge = false;
        player.spendEnergy(player.currentEnergy - 50);
    }

    void Dodge()
    {
        if (dodgeVector_.sqrMagnitude > dodgePotency_ / 3)
        {
            controller.Move(dodgeVector_ * Time.deltaTime);
            dodgeVector_ = Vector3.Lerp(dodgeVector_, Vector3.zero, inpactSmoothFactor * Time.deltaTime);

            canDodge = false;
            dodgeCoolDown = 0;

            // Debug.Log(dodgeVector_.sqrMagnitude);
        }
        else endDodge();
    }

    void endDodge()
    {
        dodging = false;
        canDodge = false;
        player.inControl = true;
        dodgeVector_ = Vector3.zero;

        dodgeCoolDown = 0;
    }
    //-----------------------------------------Dodge End----------------------------------------
    #endregion

    public void rotatePlayer(Quaternion xQuaternion)
    {
        transform.localRotation = originalRotation * xQuaternion;
    }


    void DodgeButtonMechanics(Vector3 input)
    {

        ///-------------------------DODGING BUTTON LOGIC-----------------------
        if (dodgeButtonState == DodgeButtonState.none)
        {
            if (input.x < 0) { dodgeButtonState = DodgeButtonState.Left1; dodgeButtonTimer = 0; }
            else if (input.x > 0) { dodgeButtonState = DodgeButtonState.Right1; dodgeButtonTimer = 0; }
        }

        if (dodgeButtonState == DodgeButtonState.Left1 || dodgeButtonState == DodgeButtonState.Right1)
        {
            if (dodgeButtonTime > dodgeButtonTimer) dodgeButtonTimer += Time.deltaTime;
            else { dodgeButtonState = DodgeButtonState.none; }

            if (dodgeButtonState == DodgeButtonState.Left1)
            {
                if (input.x == 0)
                {
                    dodgeButtonState = DodgeButtonState.Left2;
                    dodgeButtonTimer = 0;
                }
                else if (input.x > 0) dodgeButtonState = DodgeButtonState.none;
            }
            else if (dodgeButtonState == DodgeButtonState.Right1)
            {
                if (input.x == 0)
                {
                    dodgeButtonState = DodgeButtonState.Right2;
                    dodgeButtonTimer = 0;
                }
                else if (input.x < 0) dodgeButtonState = DodgeButtonState.none;
            }
        }

        else if (dodgeButtonState == DodgeButtonState.Left2 || dodgeButtonState == DodgeButtonState.Right2)
        {
            if (dodgeButtonTime > dodgeButtonTimer) dodgeButtonTimer += Time.deltaTime;
            else { dodgeButtonState = DodgeButtonState.none; }

            if (dodgeButtonState == DodgeButtonState.Left2)
            {
                if (input.x < 0 && player.inControl && canDodge)
                {
                    startDodge(-transform.right);
                }
            }
            else if (dodgeButtonState == DodgeButtonState.Right2)
            {
                if (input.x > 0 && player.inControl && canDodge)
                {
                    startDodge(transform.right);
                }

            }
        }


    }



    //End of Class
}
