using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBroom : MonoBehaviour
{
    [SerializeField] private Vector2 xPositionSample;
    [SerializeField] private Vector2 yPositionSample;
    [SerializeField] private Vector2 zPositionSample;
    [SerializeField] private Vector3 positionSampleScale;
    [SerializeField] private Vector3 positionSampleOffset;

    [SerializeField] private Vector2 uRotationSample;
    [SerializeField] private Vector2 vRotationSample;
    [SerializeField] private Vector2 tRotationSample;
    [SerializeField] private Vector3 rotationSampleScale;
    [SerializeField] private Vector3 rotationSampleOffset;

    [SerializeField] private GameObject flyingBroomFX;

    [SerializeField] private float positionSampleRate;


    private Vector3 _initialPosition;
    private Vector3 _initialEulerAngle;

    private void Start()
    {
        _initialPosition = Vector3.zero;
        _initialEulerAngle = Vector3.zero;
    }

    public void StartFlying()
    {
        enabled = true;
        flyingBroomFX.SetActive(true);
    }

    public void StopFlying()
    {
        enabled = false;
        flyingBroomFX.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        float x = SamplePerlinNoise(xPositionSample, Time.time, positionSampleOffset.x);
        float y = SamplePerlinNoise(yPositionSample, Time.time, positionSampleOffset.y);
        float z = SamplePerlinNoise(zPositionSample, Time.time, positionSampleOffset.z);

        float u = SamplePerlinNoise(uRotationSample, Time.time, rotationSampleOffset.x);
        float v = SamplePerlinNoise(vRotationSample, Time.time, rotationSampleOffset.y);
        float t = SamplePerlinNoise(tRotationSample, Time.time, rotationSampleOffset.z);


        Vector3 offSetPosition = new Vector3(x, y, z);
        Vector3 offSetRotation = new Vector3(u, v, t);

        offSetPosition.Scale(positionSampleScale);
        offSetRotation.Scale(rotationSampleScale);

        offSetPosition -= positionSampleScale / 2;
        offSetRotation -= rotationSampleScale / 2;

        transform.SetLocalPositionAndRotation(_initialPosition + offSetPosition,
            Quaternion.Euler(_initialEulerAngle + offSetRotation));
    }

    private float SamplePerlinNoise(Vector2 input, float time, float offset)
    {
        return Mathf.PerlinNoise(input.x * time + offset, input.y * time + offset);
    }
}
