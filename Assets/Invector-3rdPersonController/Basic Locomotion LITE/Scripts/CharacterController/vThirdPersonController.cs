using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        public Interactable focus;  // Current focus of player (Weed, NPCs etc)
        public float range = .02f;
        public LayerMask interactionLayer;

        Camera cam;
        protected virtual void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {

            //   Debug.DrawRay(cam.transform.position, new Vector3(.05f, .05f, .05f), Color.red, 0.5f);

            // If LMB pressed
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("LMB down");

                Debug.DrawRay(cam.transform.position, cam.transform.forward * range, Color.red, 0.5f);
                // Create ray
               // Ray ray = cam.ViewportPointToRay(cam.transform.forward * range);
                RaycastHit hit;


                // If the ray hits
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, interactionLayer))
                {
                    Debug.Log("ray HIT: " + hit.collider.name);
                    // Getting the interactable object and setting focus on it
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.player = GameObject.FindGameObjectWithTag("Player").transform; // Temp?
                        SetFocus(interactable);
                    }
                }
            }
        }



        // Set the new focus
        private void SetFocus(Interactable theFocus)
        {
            if (theFocus != focus)
            {
                if (focus != null)
                {
                    focus.OnDefocused();
                }

                focus = theFocus; // the new focus
            }

            theFocus.OnFocused(transform);
            Debug.Log("in focus: " + theFocus.transform);
        }

        // Remove our current focus
        void RemoveFocus()
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = null;
        }

        public virtual void Sprint(bool value)
        {
            isSprinting = value;
        }

        public virtual void Strafe()
        {
            if (locomotionType == LocomotionType.OnlyFree) return;
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // conditions to do this action
            bool jumpConditions = isGrounded && !isJumping;
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;
            // trigger jump animations            
            if (_rigidbody.velocity.magnitude < 1)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", 0.2f);
        }

        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
            var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
            targetRotation = transform.rotation;
        }
    }
}