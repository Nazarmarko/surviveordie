

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using InventoryCamera;

public class GameHandler_Setup : MonoBehaviour {

    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform followTransform;

    private void Start() {
        //Sound_Manager.Init();
        cameraFollow.Setup(GetCameraPosition, () => 60f, true, true);

        //FunctionPeriodic.Create(SpawnEnemy, 1.5f);
        //EnemyHandler.Create(new Vector3(20, 0));
    }

    private Vector3 GetCameraPosition() {
        return followTransform.position;
    }

}
