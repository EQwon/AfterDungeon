﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLightController : MonoBehaviour
{
    [SerializeField] private float Brightness;
    [Range(0, 1)]
    [SerializeField] private float Thickness;
    [Range(0,1)]
    [SerializeField] private float Proportion;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetFloat("_Brightness", Brightness);
        GetComponent<Renderer>().material.SetFloat("_Thickness", Thickness);
        GetComponent<Renderer>().material.SetFloat("_Proportion", Proportion);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_Brightness", Brightness);
        GetComponent<Renderer>().material.SetFloat("_Thickness", Thickness);
        GetComponent<Renderer>().material.SetFloat("_Proportion", Proportion);
    }

    public void SetBright(float Bright)
    {
        Brightness = Bright;
    }

    public void SetThickness(float Thick)
    {
        Thickness = Thick;
    }

    public void SetProportion(float num)
    {
        Proportion = num;
    }
}