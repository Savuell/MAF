using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    //XRControll
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.LocomotionSystem locomotion;
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] private InputActionProperty leftHandLocomotion;
    //Wheels
    [SerializeField] private WheelCollider[] wheels = new WheelCollider[4];    
    [SerializeField] private SteeringWheel steeringWheel;
    [SerializeField] private WheelsAngle frontWheels;
    [SerializeField] private float maxMotorForce;
    [SerializeField] private float pedalMultiplier;
    private float pedal;
    //Audio
    [SerializeField] private AudioSource motorSound1, motorStartSound, motortStopSound, motorSound2;
    [SerializeField] private float motorSoundKD;
    //Stats
    [SerializeField] private BatteryCharge charge;
    [SerializeField] private TabController tab;
    [SerializeField] private float battery;
    [SerializeField] private float batteryLoseSpeed;
    
    [HideInInspector] public static bool playerInCar = false;
    private float motorVolume1, motorVolume2, motorSoundTimer;
    private bool preMotorSound, motorSound;
    private GameObject rigidBodyObject;
    private bool prePlayerInCar;

    private void Start()
    {
        rigidBodyObject = transform.GetChild(0).gameObject;
    }
    void FixedUpdate()
    {
        SetStats();
        GetInCar();
        frontWheels.rotate = steeringWheel.steerAngle / 20;
        if (battery > tab.values[3].x)
        {
            MotorSounds();
            CarMove();
        }
        else foreach(WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0;
            wheel.brakeTorque = 0;
        }
        prePlayerInCar = playerInCar;
    }
    void GetInCar()
    {
        if(playerInCar)
        {
            locomotion.enabled = false;
            dynamicMoveProvider.enabled = false;
            locomotion.transform.parent.parent = rigidBodyObject.transform;
        }
        if (playerInCar == false && prePlayerInCar != playerInCar)
        {
            locomotion.transform.parent.parent = null;
            locomotion.transform.parent.transform.eulerAngles = new Vector3(0, 0, 0);
            locomotion.enabled = true;
            dynamicMoveProvider.enabled = true;
        }
    }
    void SetStats()
    {
        if (charge.charged && battery < tab.values[3].z) battery += Time.deltaTime * charge.chargeSpeed;
        if (battery >= tab.values[3].z) battery = tab.values[3].z;
        tab.values[3].y = battery;
    }
    void CarMove()
    {
        if (playerInCar)
        {
            battery -= Time.deltaTime * batteryLoseSpeed;
            float pedalForce = leftHandLocomotion.action.ReadValue<Vector2>().y;
            pedal += pedalForce * pedalMultiplier * (pedalForce < 0 ? pedalMultiplier / 2 : 1);
            pedal = Mathf.Max(pedal, -1);
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = Mathf.Min(maxMotorForce, Mathf.Max(0, pedal)) * (1 - Mathf.Min(Mathf.Abs(frontWheels.rotate) / 60, 0.5f));
                wheel.brakeTorque = pedalForce < 0 ? 1 : 0;
            }
        }
        if (playerInCar == false && prePlayerInCar != playerInCar)pedal = 0;
    }
    void PlaySound()
    {
        if(playerInCar)
        {
            if (motorSoundTimer <= 0)
            {
                motorSoundTimer = motorSoundKD;
                motorSound = !motorSound;
            }
            if(motorSound!=preMotorSound)
            {
                if (motorSound)
                {
                    motorSound1.Play();
                    motorVolume1 = 6;
                }
                else
                {
                    motorSound2.Play();
                    motorVolume2 = 6;
                }
            }
            motorSound1.volume = motorVolume1;
            motorSound2.volume = motorVolume2;
            motorSound1.volume *= 0.6f;
            motorSound2.volume *= 0.6f;
            motorVolume1 -= Time.deltaTime * 6;
            motorVolume2 -= Time.deltaTime * 6;
            motorSoundTimer -= Time.deltaTime;
            preMotorSound = motorSound;
        }

    }
    void MotorSounds()
    {
        if (!playerInCar)
        {
            motorSound1.Stop();
            motorSound2.Stop();
        }
        if (playerInCar != prePlayerInCar)
        {
            if (playerInCar) motorStartSound.Play();
            else motortStopSound.Play();
        }
        PlaySound();
    }
}
