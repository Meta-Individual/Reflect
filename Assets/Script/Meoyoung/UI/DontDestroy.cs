﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public static DontDestroy Instance { get; private set; } // Singleton 인스턴스

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }
}
