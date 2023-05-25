using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameListener { };

public interface IGameStartListener: IGameListener
{
    public void OnGameStarted();
}

public interface IGamePausedListener : IGameListener
{
    public void OnGamePaused();
}

public interface IGameResumeListener : IGameListener
{
    public void OnGameResumed();
}

public interface IGameFinishListener : IGameListener
{
    public void OnGameFinished();
}