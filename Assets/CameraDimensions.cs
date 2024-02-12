using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDimensions : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFramingTransposer framingTransposer;
    [Header("Current Deadzone")]
    public float deadzoneWidth;
    public float deadzoneHeight;

    [Header("Deadzone Wanted")]
    public float deadzoneW_Wanted;
    public float deadzoneH_Wanted;


    public BoxCollider2D boxCollider2D;

    private void Awake()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        float orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        float aspect = Screen.width / (float)Screen.height; 

        float width = orthographicSize * aspect * 2f;
        float height = orthographicSize * 2f;

        boxCollider2D.size = new Vector2(width, height);


        deadzoneWidth = framingTransposer.m_DeadZoneWidth;
        deadzoneHeight = framingTransposer.m_DeadZoneHeight;

    }



    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("DeadZoneX"))
    //    {
    //        Debug.Log("deadzone : " + collision.name);
    //        framingTransposer.m_DeadZoneWidth = deadzoneW_Wanted;
    //    }
    //    if (collision.CompareTag("DeadZoneY"))
    //    {
    //        framingTransposer.m_DeadZoneHeight = deadzoneH_Wanted;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("DeadZoneX"))
    //    {
    //        Debug.Log("deadzone : " + collision.name);
    //        framingTransposer.m_DeadZoneWidth = deadzoneWidth;

    //    }
    //    if (collision.CompareTag("DeadZoneY"))
    //    {
    //        framingTransposer.m_DeadZoneHeight = deadzoneHeight;
    //    }
    //}




}
