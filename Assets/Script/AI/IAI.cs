using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    bool GetReloadState();



    void Shoot();
    bool GetIdleStatus();
    void SetToIdle(bool status);
    Transform GetTheBestCover();
    void SetBestCoverSpot(Transform cover);

}
