using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    bool GetReloadState();
    float GetHealth();

    bool IsOnCriticalLevel();
    void Shoot();
    void SetIdle();
    void OnMove();

    void OnAim();
    Transform GetTheBestCover();
    void SetBestCoverSpot(Transform cover);

}
