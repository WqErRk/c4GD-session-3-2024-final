using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; //!
using UnityEngine.Rendering.Universal; //!

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance; //Singleton

    //[Header("name")] is just syntax for formatting a section in the unity inspector

    [Header("Settings")]
    public bool VFXOn = true; //so we can disable and enable the vfx

    [Header("Camera Shake")] //Shaking without cinemachine
    public Transform cameraTransform; //Make sure to set this to the camera object in the inspector
    //NOTES: When we are shaking the camera, we are changing the localPosition, meaning that there needs to be an empty parent object for the camera
    //IF you are moving the camera to a target position, make sure you are moving the PARENT of the camera to the target position, for this to work. IE: The camerafollow script would be on the Parent object
    
    // How long the object should shake for. Set by outside function
    public float shakeDuration = 0f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0f;

    [Header("Particles")] //Just storing the prefabs here for easy access
    public GameObject particleExplosion;

    [Header("Screen Effects")] //Post processing
    public Volume volume; //A volume handles post processing effects in Unity
    ColorAdjustments CA; //stores color adjustment from profile
    //Damage Flash stuff
    public float damageFlashDurationMax = 0.3f;
    private float damageFlashDurationTimer = 0;
    public Color damageFlashColor = Color.red;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        volume.gameObject.SetActive(VFXOn);
        
        instance = this; //Setting the singleton

        volume.profile.TryGet<ColorAdjustments>(out CA); //Function to get the color adjustment from the volume profile
    }

    //Function to shake the camera by duration and amount
    public void ShakeCam(float duration = .3f, float amt = .7f)
    {
        if (VFXOn)
        {
            shakeDuration = duration;
            shakeAmount = amt;
        }
    }

    //Function to flash damage on screen
    public void FlashDamage(float duration = .3f)
    {
        if (VFXOn)
        {
            damageFlashDurationMax = duration;
            damageFlashDurationTimer = duration;
            CA.colorFilter.value = damageFlashColor;
        }
    }

    //Function to spawn an explosion (realistically this would be a function/line in another script, but I'm putting it here to consolidate everything
    public void SpawnExplosion(Vector3 position)
    {
        if (VFXOn)
        {
            Instantiate(particleExplosion, position, particleExplosion.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Camera Shake
        if (shakeDuration > 0)
        {
            //lerp the position to a random position within a "sphere" to shake the camera
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, Random.insideUnitSphere * shakeAmount, .2f);
            //decrement the duration timer
            shakeDuration -= Time.deltaTime;
        } else
        {
            //reset the position
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, Vector3.zero, .2f);
        }

        //Damage Flash
        if (damageFlashDurationTimer > 0)
        {
            //When damageFlashDurationTimer/damageFlashDurationMax == 1, screen is red tinted, and when damageFlashDurationTimer/damageFlashDurationMax == 0, screen is normal tint
            CA.colorFilter.value = Color.Lerp(Color.white, damageFlashColor, damageFlashDurationTimer / damageFlashDurationMax);
            //decrement the duration timer
            damageFlashDurationTimer -= Time.deltaTime;
        }
        else
        {
            //reset the tint
            //CA.colorFilter.value = Color.white;
        }

    }
}