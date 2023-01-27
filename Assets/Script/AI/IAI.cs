using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    bool GetReloadState();

    void SetAggressive();

    void Shoot();
    void SetToIdle();
    Transform GetTheBestCover();
    void SetBestCoverSpot(Transform cover);

}
