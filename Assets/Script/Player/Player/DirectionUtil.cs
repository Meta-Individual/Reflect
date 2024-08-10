using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionUtils
{
    private static PlayerController _playerController;

    // PlayerController 인스턴스를 설정하는 메서드
    public static void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public static bool CheckDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.RIGHT:
                return _playerController.anim.GetFloat("DirX") == 1;
            case Direction.LEFT:
                return _playerController.anim.GetFloat("DirX") == -1;
            case Direction.UP:
                return _playerController.anim.GetFloat("DirY") == 1;
            case Direction.DOWN:
                return _playerController.anim.GetFloat("DirY") == -1;
            default:
                return false;
        }
    }
}
