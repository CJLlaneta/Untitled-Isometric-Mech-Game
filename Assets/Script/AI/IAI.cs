using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    bool GetReloadState();



    void Shoot();
    void SetIdle();
    void OnMove();
    Transform GetTheBestCover();
    void SetBestCoverSpot(Transform cover);

}
