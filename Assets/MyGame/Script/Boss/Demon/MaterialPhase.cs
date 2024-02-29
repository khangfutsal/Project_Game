using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaterialPhase : MonoBehaviour
{
    private static MaterialPhase _ins;

    [SerializeField] private float _dissolveTime = 0.75f;

    [SerializeField] private int _fade = Shader.PropertyToID("_Fade");

    [SerializeField] private Material curShaderPhase;
    [SerializeField] private List<Material> materials;
    [SerializeField] private int _numMat;
    public static UnityEvent OnAppear = new UnityEvent();
    public static UnityEvent OnDisappear = new UnityEvent();

    private void Awake()
    {
        _ins = this;
    }

    public static MaterialPhase GetInstance() => _ins;

    public void SetShaderPhase(int numMat)
    {
        Debug.Log("numMat" + numMat);
        _numMat = numMat;
        curShaderPhase = materials[_numMat];

        transform.GetComponent<SpriteRenderer>().material = curShaderPhase;
        curShaderPhase.SetFloat(_fade, 1.2f);
    }


    private void Start()
    {
        curShaderPhase = materials[_numMat];
        transform.GetComponent<SpriteRenderer>().material = curShaderPhase;
        curShaderPhase.SetFloat(_fade, 1.2f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            StartCoroutine(CouroutineAppearDissolve());
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StartCoroutine(CouroutineDisappearDissolve());
        }
    }

    public void AppearDissolve()
    {
        StartCoroutine(CouroutineAppearDissolve());
    }

    public void DisappearDissolve()
    {
        StartCoroutine(CouroutineDisappearDissolve());
    }
    public IEnumerator CouroutineAppearDissolve()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime * .4f;

            float lerpedDissolve = Mathf.Lerp(-1, 1.2f, (elapsedTime / _dissolveTime));
            curShaderPhase.SetFloat(_fade, lerpedDissolve);
            yield return null;
        }
        OnAppear?.Invoke();
    }

    public IEnumerator CouroutineDisappearDissolve()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime * .4f;

            float lerpedDissolve = Mathf.Lerp(1.2f, -1, (elapsedTime / _dissolveTime));
            curShaderPhase.SetFloat(_fade, lerpedDissolve);
            yield return null;
        }
        OnDisappear?.Invoke();
    }


}
