# Table of Contents

- [Frozen World](#frozen-world)
- [About Game](#about-game)
  - [Game Design & Game Idea](#game-design--game-idea)
  - [How to Run](#how-to-run)
- [Game Architecture](#game-architecture)
  - [Singleton](#singleton)
  - [Bootstrapper](#bootstrapper)
  - [State Machine](#state-machine)
  - [Observer](#observer)
  - [Service Locator](#service-locator)
  - [Scene Loading](#scene-loading)
- [Game Visuals](#game-visuals)
  - [Parallax](#parallax)
  - [Custom Full-Screen Shader](#custom-full-screen-shader)
  - [UI](#ui)
  - [Animator](#animator)
  - [Sprites](#sprites)

# About Game
## Game Design & Game Idea

Game was fully created for Mini Jam 145: Frozen in 2 days.

[Game page on itch.io](https://vakor03.itch.io/frozen-fire)

Jam limitation was Inconvenient Superpowers and theme Frozen. In this game was created heating area that helps to progress through puzzles and platforming but it also can ruin your run.

Game has 6 playable levels with Main menu and About us.
![1](https://github.com/Varalon-Max/FrozenWorld/assets/71404364/a8d6d836-e48f-4e2d-957a-828386562b7d)
![2](https://github.com/Varalon-Max/FrozenWorld/assets/71404364/ce16cf39-00ce-4dcb-bf91-f10f127bc4ec)
![5](https://github.com/Varalon-Max/FrozenWorld/assets/71404364/8a36a47c-d184-4e71-9e88-6720a5d4ceec)
![4](https://github.com/Varalon-Max/FrozenWorld/assets/71404364/b97bb664-6895-41bd-9423-05e8136b6d43)
![3](https://github.com/Varalon-Max/FrozenWorld/assets/71404364/1a0e2259-7699-41a2-80c3-b5bc3fd947b4)

## How to Run

You can run game on itch.io in browser. I also provided archive to open it locally on Windows.

Another option could be cloning project and running it in Unity itself. If chosen this option you should run game from Bootstrapper scene only!

# Game Architecture & Patterns Used
## Singleton

The Singleton pattern ensures that a class has only one instance and provides a global point of access to it.

In this project it was used to ensure that game have only one [GameManager](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/GameManager.cs) and [SoundManager](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Sound/SoundManager.cs) and they are persistent.

```csharp
private void Awake()
{
    if (Instance != null)
    {
        Destroy(gameObject);
    }
    else
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
```



## Bootstrapper

A Bootstrapper pattern is used to initialize and configure the components of a game.

In this project it was used to initialize GameManager and SoundManager before game starts. For this purpose was created Bootstrapper scene.



## State Machine

A State Machine pattern is employed to manage the state of an entity or a system.

In this project it was used to control states of player. It has 2 main parts: [IPlayerState](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Player/IPlayerState.cs) and [Player.StateMachine](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Player/Player.StateMachine.cs)

__IPlayerState__

```csharp
public interface IPlayerState
{
    event Action OnStart;
    event Action OnUpdate;
    event Action OnExit;
    
    void Enter();
    void Update(float deltaTime);
    void Exit();
}
```

__Player.StateMachine__

```csharp
private IPlayerState _currentState;
private readonly Dictionary<Type, IPlayerState> _states = new();

public void SetState<T>() where T : IPlayerState
{
    _currentState?.Exit();
    _currentState = _states[typeof(T)];
    _currentState.Enter();
}

public IPlayerState GetState()
{
    return _currentState;
}
```



## Observer

Observer pattern is commonly used for event handling and communication between different game components. 

In this project it was realized with _event_ keyword

__Publisher code:__

```csharp
public event Action OnPlayerDead;
```

__Subscriber code:__

```csharp
private void OnEnable()
{
    GameManager.Instance.OnPlayerDead += PlayerDead;
}

private void OnDisable()
{
    GameManager.Instance.OnPlayerDead -= PlayerDead;
}
```



## Service Locator

The Service Locator pattern is a design pattern used in game development to centralize the access to various services or resources.

It's realization is [ServiceLocator.cs](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Tools/ServiceLocator/ServiceLocator.cs)

```csharp
private static Dictionary<Type, IService> _services = new();

public static T GetService<T>() where T : IService
{
    return (T) _services[typeof(T)];
}

public static void RegisterService<T>(T service) where T : IService
{
    _services.Add(typeof(T), service);
}

public static void UnregisterService<T>() where T : IService
{
    _services.Remove(typeof(T));
}
```



## Scene Loading

For loading game scenes was created [Loader](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Tools/Loader.cs), which responsibility was to load "Loading" scene at first and then target scene when it is ready. This lowers chance of lagging while loading scene

```csharp
public static void Load(SceneReference scene)
{
    _targetSceneReference = scene;
    SceneManager.LoadScene("Loading");
    Timing.RunCoroutine(LoadTargetSceneReference());
}

private static IEnumerator<float> LoadTargetSceneReference()
{
	yield return Timing.WaitForOneFrame;
    SceneManager.LoadScene(_targetSceneReference.Name);
}
```



# Game Visuals
## Custom Full-Screen Shader

For this game was created custom Frost shader using Shader Graph.

## UI
Created ScreenSpace and WorldSpace UI

## Cinemachine
Cinemachine was used for camera to follow Player

## Animator
For Player animations was used Crossfade animation method. All code in here: [CrossfadeAnimator](https://github.com/Varalon-Max/FrozenWorld/blob/main/Assets/_Project/Scripts/Animation/CrossfadeAnimator.cs)

## Sprites
Hand-drawn sprites
