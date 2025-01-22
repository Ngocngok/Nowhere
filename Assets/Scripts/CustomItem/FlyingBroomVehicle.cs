using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBroomVehicle : MonoBehaviour
{
    [SerializeField] private FlyingBroom flyingBroom;

    public void RegisterBattery(SparePartAssembled sparePartAssembled)
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            flyingBroom.StartFlying();
        });
    }

    public void UnregisterBattery(SparePartAssembled sparePartAssembled)
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            flyingBroom.StopFlying();
        });
    }
}
