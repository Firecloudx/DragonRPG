﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);


    CameraRaycaster cameraRaycaster;
    
    // Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.layerChangeObservers += OnDelegateCalled; //registering 

    }
	
	// Only called when layer changes
	void OnDelegateCalled () {

        print("CursorAffordance delegate reporting for duty");

        switch (cameraRaycaster.currentLayerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
    
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;

            default:
                Debug.LogError("Dont know what cursor to show");
                return;

        }
        
        
	}
}
