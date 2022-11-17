using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject
{
	[RequireComponent(typeof(ScrollRect))]
    public class AutoScroll : MonoBehaviour
    {
	    private ScrollRect _scrollRect;

	    [SerializeField] private float Scrollspeed = 1;

	    private void Awake()
	    {
		    _scrollRect = GetComponent<ScrollRect>();
	    }

	    // Update is called once per frame
        void Update()
        {
	        _scrollRect.verticalNormalizedPosition -= Scrollspeed * Time.deltaTime;

	        if (_scrollRect.verticalNormalizedPosition <= 0)
	        {
		        _scrollRect.verticalNormalizedPosition = 1;
	        }
        }
    }
}
