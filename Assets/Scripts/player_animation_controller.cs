using UnityEngine;

public class player_animation_controller : MonoBehaviour
{
    private Animator animator;
    private float velocity_x;
    private float velocity_z;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float max_walk_speed = 0.5f;
    public float max_run_speed = 2.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W); //key for forward movement has been pressed
        bool backwardPressed = Input.GetKey(KeyCode.S); //key for backward movement has been pressed
        bool leftPressed = Input.GetKey(KeyCode.A); //key for left strafe movement has been pressed
        bool rightPressed = Input.GetKey(KeyCode.D); //key for right strafe movement has been pressed
        bool runPressed = Input.GetKey(KeyCode.LeftShift); //key for running has been pressed

        float currentMaxVelocity = runPressed ? max_run_speed : max_walk_speed; //determines the current max speed based on whether the run key is pressed

        if (forwardPressed && velocity_z < currentMaxVelocity) //forward walking acceleration
        {
            velocity_z += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocity_x > -currentMaxVelocity) //left strafe walking acceleration
        {
            velocity_x -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocity_x < currentMaxVelocity) //right strafe walking acceleration
        {
            velocity_x += Time.deltaTime * acceleration;
        }


        if (!forwardPressed && velocity_z > 0.0f) //deceleration for forward movement
        {
            velocity_z -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && velocity_x < 0.0f) //deceleration for left strafe movement
        {
            velocity_x += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocity_x > 0.0f) //deceleration for right strafe movement
        {
            velocity_x -= Time.deltaTime * deceleration;
        }


        if (!forwardPressed && velocity_z < 0.0f) //hard stop for forward movement deceleration
        {
            velocity_z = 0.0f;
        }
        if (!leftPressed && !rightPressed && velocity_x > -0.05f && velocity_x < 0.05f) //hard stop for strafe movement deceleration
        {
            velocity_x = 0.0f;
        }


        if (forwardPressed && runPressed && velocity_z > currentMaxVelocity) //hard stop for forward acceleration when running
        {
            velocity_z = currentMaxVelocity;
        } else if (forwardPressed && velocity_z > currentMaxVelocity) //hard stop for forward acceleration when walking
        {
            velocity_z -= Time.deltaTime * deceleration;
            if (velocity_z > currentMaxVelocity && velocity_z < (currentMaxVelocity + 0.05f)) //round to max velocity when within offset to prevent jittering
            {
                velocity_z = currentMaxVelocity;
            }
        } else if (forwardPressed && velocity_z < currentMaxVelocity && velocity_z > (currentMaxVelocity - 0.05)) //round to max velocity when within offset to prevent jittering
        {
            velocity_z = currentMaxVelocity;
        }


        if (leftPressed && runPressed && velocity_x > -currentMaxVelocity) //hard stop for forward acceleration when running
        {
            velocity_x = -currentMaxVelocity;
        } else if (leftPressed && velocity_x > -currentMaxVelocity) //hard stop for forward acceleration when walking
        {
            velocity_x -= Time.deltaTime * deceleration;
            if (velocity_x > -currentMaxVelocity && velocity_x > (-currentMaxVelocity - 0.05f)) //round to max velocity when within offset to prevent jittering
            {
                velocity_x = -currentMaxVelocity;
            }
        } else if (leftPressed && velocity_x < -currentMaxVelocity && velocity_x < (-currentMaxVelocity + 0.05f)) //round to max velocity when within offset to prevent jittering
        {
            velocity_x = -currentMaxVelocity;
        }
        
        //jitter prevention for right strafe animation
        if (rightPressed && runPressed && velocity_x > currentMaxVelocity) //hard stop for forward acceleration when running
        {
            velocity_x = currentMaxVelocity;
        } else if (rightPressed && velocity_x > currentMaxVelocity) //hard stop for forward acceleration when walking
        {
            velocity_x -= Time.deltaTime * deceleration;
            if (velocity_x > currentMaxVelocity && velocity_x < (currentMaxVelocity + 0.05f)) //round to max velocity when within offset to prevent jittering
            {
                velocity_x = currentMaxVelocity;
            }
        } else if (rightPressed && velocity_x < currentMaxVelocity && velocity_x > (currentMaxVelocity - 0.05f)) //round to max velocity when within offset to prevent jittering
        {
            velocity_x = currentMaxVelocity;
        }

        //set the velocity parameters in the animator to control the animation blending
        animator.SetFloat("velocity_x", velocity_x);
        animator.SetFloat("velocity_z", velocity_z);
    }
}
