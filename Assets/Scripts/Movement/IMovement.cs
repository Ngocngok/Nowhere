using UnityEngine;
using System;

public interface IMovement
{
    public void ProcedureMoveTo(Vector3 targetDestination, Quaternion targetRotation, Action onDestinationReached);

    public void EndProcedureMove();
}
