﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : ContactArrow
{
    [SerializeField] private float speed;

    public override void OnLodgingEnterAction(GameObject arrow)
    {
        Rigidbody2D rb2D = arrow.GetComponent<Rigidbody2D>();

        rb2D.velocity = Vector2.zero;
        rb2D.bodyType = RigidbodyType2D.Static;

        float targetPosX = transform.position.x;
        arrow.transform.position = new Vector2(targetPosX, arrow.transform.position.y);
    }

    public override void OnLodgingExitAction(GameObject arrow)
    {
        arrow.GetComponent<ArrowController>().Disable();
    }

    public override void OnLodgingStayAction(GameObject arrow)
    {
        if (!arrow.GetComponent<ArrowController>().IsTherePlayer)
        {
            arrow.GetComponent<Collider2D>().isTrigger = false;
            arrow.layer = LayerMask.NameToLayer("Platform");
        }
        //Debug.Log("Colliding with Rail...");

        arrow.transform.localPosition += new Vector3(0, speed * Time.fixedDeltaTime);
    }
}
