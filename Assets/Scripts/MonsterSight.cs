﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float viewRange = 5f;
    [SerializeField] [Range(0, 90f)] private float viewAngle = 60f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private MonsterAI AI;

    private void Start()
    {
        AI = GetComponent<MonsterAI>();
    }

    private void FixedUpdate()
    {
        FindTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float dx = Mathf.Cos(viewAngle * Mathf.Deg2Rad) * viewRange;
        float dy = Mathf.Sin(viewAngle * Mathf.Deg2Rad) * viewRange;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + new Vector2(dx, dy));
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + new Vector2(viewRange, 0));
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + new Vector2(dx, -dy));
    }

    private void FindTarget()
    {
        AI.isFindHero = false;

        Vector2 originPos = transform.position;
        Collider2D[] hittedTargets = Physics2D.OverlapCircleAll(originPos, viewRange, targetLayer);

        if (hittedTargets.Length == 0) return;

        foreach (Collider2D hitTarget in hittedTargets)
        {
            Vector2 targetPos = hitTarget.transform.position;
            Vector2 dir = targetPos - originPos;
            float distance = Mathf.Abs(dir.magnitude);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            if (distance > viewRange) continue;                     // 거리가 멀면 넘긴다.
            if (angle > viewAngle || angle < -viewAngle) continue;  // 시야각을 벗어나면 넘긴다.

            RaycastHit2D rayTarget = Physics2D.Raycast(originPos, dir, viewRange, obstacleLayer);

            // '장애물이 존재하지 않'거나 '장애물이 존재해도 타겟 뒤에 있으면'
            if (!rayTarget || Vector2.Distance(rayTarget.transform.position, originPos) > distance)
            {
                AI.isFindHero = true;
                Debug.DrawLine(originPos, targetPos, Color.red);
                return;
            }
        }
    }
    public void FlipFacingDir()
    {
        viewRange *= -1;
    }
}