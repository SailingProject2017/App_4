/***********************************************************************/
/*! @file   NextLineRenderer.cs
*************************************************************************
*   @brief  次のブイまでの線を描画する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLineRenderer : MarkerBase {

    private Vector3 startPosition;     // @brief 線の開始位置
    private Vector3 goalPosition;      // @brief 線の目標位置
    private Vector3 yondPosition;      // @brief 目標のその先の位置
    private GameObject playerObject;   // @brief プレイヤーを格納する
    private MarkerBase markerBase;     // @brief MarkerBaseを格納する
    private LineRenderer lineRenderer; // @brief LineRendererを格納する

    /// <summary>
    /// @brief マーカー
    /// </summary>
    protected override void MarkerInitialize()
    {
        base.MarkerInitialize();

        playerObject = GameObject.Find("Player");
        markerBase = playerObject.GetComponent<MarkerBase>();

        lineRenderer = GetComponent<LineRenderer>();

        startPosition = playerObject.transform.position;
        goalPosition = hitMarkerList[markerBase.CurrentMarker].gameObject.transform.position;
        yondPosition = hitMarkerList[markerBase.CurrentMarker + 1].gameObject.transform.position;
    }

    /// <summary>
    ///  @brief 更新処理
    /// </summary>
    private void StraightLineUpdate()
    {
        startPosition = playerObject.transform.position;
        goalPosition = hitMarkerList[markerBase.CurrentMarker].gameObject.transform.position;

        if(hitMarkerList[markerBase.CurrentMarker].gameObject.tag != "goal")
        {
            yondPosition = hitMarkerList[markerBase.CurrentMarker + 1].gameObject.transform.position;
        }
    }

    /// <summary>
    /// @brief 線の描画
    /// </summary>
    private void StraightLineRenderer()
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, goalPosition);
        lineRenderer.SetPosition(2, yondPosition);
    }

    public override void OnUpdate()
    {
        StraightLineUpdate();
        StraightLineRenderer();
        
        if(Singleton<GameInstance>.Instance.IsGoal)
        {
            Delete(gameObject);
        }
    }
}
