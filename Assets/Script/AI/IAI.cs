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

    void SetEngageMode(bool status);
    bool IsInEngageMode();
    void OnAim();
    Transform GetTheBestCover();
    void SetBestCoverSpot(Transform cover);

}
