using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioClip shootClip;
    private AudioSource laserShootSource;
    [SerializeField] private float shootVolume = 0.3f;

    private void Start()
    {
        laserShootSource = GetComponent<AudioSource>();
        lineRenderer.startWidth = 0.0f;
        StartCoroutine(IncreaseWidthAnimation());
    }


    private void Update()
    {
        if (lineRenderer != null)
            transform.position = shootPoint.transform.position;

        if (gameObject.activeSelf && !laserShootSource.isPlaying)
            laserShootSource.PlayOneShot(shootClip, shootVolume);
    }

    IEnumerator IncreaseWidthAnimation()
    {
        if (lineRenderer.startWidth < 0.1f)
        {
            lineRenderer.startWidth += 0.08f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(IncreaseWidthAnimation());
        }
    }

    public Transform getShootPoint()
    {
        return shootPoint;
    }

    public void setShootPoint(Transform shootPoint)
    {
        this.shootPoint = shootPoint;
    }


}