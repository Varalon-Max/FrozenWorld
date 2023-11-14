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

# About Game {#frozen-world}
## Game Design & Game Idea

Game was fully created for Mini Jam 145: Frozen in 2 days.

[Game page on itch.io](https://vakor03.itch.io/frozen-fire)

Jam limitation was Inconvenient Superpowers and theme Frozen. In this game was created heating area that helps to progress through puzzles and platforming but it also can ruin your run.

Game has 6 playable levels with Main menu and About us.

## How to Run

You can run game on itch.io in browser. I also provided archive to open it locally on Windows.

Another option could be cloning project and running it in Unity itself. If chosen this option you should run game from Bootstrapper scene only!

# Game Architecture & Patterns Used
## Singleton

The Singleton pattern ensures that a class has only one instance and provides a global point of access to it.

In this project it was used to ensure that game have only one [GameManager]() and [SoundManager]() and they are persistent.

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

In this project it was used to control states of player. It has 2 main parts: [IPlayerState]() and [Player.StateMachine]()

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

It's realization is [ServiceLocator.cs]()

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

For loading game scenes was created [Loader](), which responsibility was to load "Loading" scene at first and then target scene when it is ready. This lowers chance of lagging while loading scene

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
## UI
## Animator
## Sprites
