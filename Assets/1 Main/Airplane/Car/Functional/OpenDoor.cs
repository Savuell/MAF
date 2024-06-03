using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OpenDoor : MonoBehaviour
{
    [SerializeField] private InputActionProperty leftHand, rightHand;
    [SerializeField] private Spedometer speedometer;
    [SerializeField] private GameObject spawnPointIn, spawnPointOut;
    private int handed;
    private List<GameObject> hand = new List<GameObject>();
    private InputActionProperty currentHand;
    private FadeScreen fade;
    private GameObject playerOrigin;
    private CarHinge doorAxis;
    private bool gettingInCar;
    private void Start()
    {
        fade = Camera.main.transform.GetChild(0).GetComponent<FadeScreen>();
        playerOrigin = Camera.main.transform.parent.parent.gameObject;
        doorAxis = transform.parent.GetComponent<CarHinge>();
    }
    void Update()
    {
        if (handed > 0)
        {
            currentHand = hand[0].transform.parent.name == "LeftHand" ? leftHand : rightHand;
            if (handed > 1) currentHand = leftHand.action.ReadValue<float>() > rightHand.action.ReadValue<float>() ? leftHand : rightHand;
            if (currentHand.action.ReadValue<float>() > 0.5f && !gettingInCar) if (speedometer.speed < 1) GetInCar();
        }
    }
    IEnumerator WaitFade()
    {
        gettingInCar = true;
        doorAxis.isOpen = true;
        fade.FadeOut(FadeScreen.fadeDuration);
        yield return new WaitForSeconds(1);
        doorAxis.isOpen = false;
        playerOrigin.transform.position = CarController.playerInCar ? spawnPointOut.transform.position : spawnPointIn.transform.position;
        CarController.playerInCar = !CarController.playerInCar;
        fade.FadeIn(FadeScreen.fadeDuration);
        gettingInCar = false;
    }
    void GetInCar()
    {
        StartCoroutine(WaitFade());        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            hand.Add(other.gameObject);
            handed++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            handed--;
            hand.Remove(other.gameObject);
        }
    }
}
