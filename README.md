# Description
The package contains a system for loading scenes with customizable transitions. It includes screen fading, progress display with image/slider/text, scene activation delay (until a key was pressed) and events for complex situations. System also allows to add new transition implementations.
# How To Use
See **ExampleTransition_Menu** scene as an example. It shows the system usage with service locator, but you can implement other ways. Also take a look at **SimpleFadeTransition** and **LoadingScreenTransition** prefabs to better understand how transitions are customized.
# Quick Overview
## Scene Loader
**ISceneLoader** is an interface for calling scene loading methods with transition and callback parameters. Used to bind implementations in project context.
**SceneLoader** is the current implementation of **ISceneLoader** based on **SceneManager** asynchronous scene operations wrapped in a coroutine.
>[!IMPORTANT]
>**SceneLoader** instance should be declared in a persistant MonoBehaviour (marked as DontDestroyOnLoad).
## Transitions
**ITransition** is an interface for calling loading transition methods in active scene loader. Used to bind implementations in project context.
**TransitionSequence** is an intended way to customize scene transitions. It implements **ITransition** interface and contains the list of transitions with dropdown menu.
>[!NOTE]
>The process of loading a scene with transition sequence looks like this: fade in each transition => load scene => fade out each transition in reverse order.
>[!IMPORTANT]
>If specific **TransitionSequence** is bounded to non-persistent object then its **Context** value should be set to **Scene**.
**Transition** is an abstract **ITransition** implementation which represents an element in **TransitionSequence**. Create your own implementations by deriving from it.
>[!NOTE]
>Child classes of **Transition** can be passed as transition parameter in the scene loader. But it's recommended to use **TransitionSequence** instead.
### Fade
**FadeTransition** is an abstract class for fading alpha value of some source (Image, CanvasGroup, etc.) based on specified configuration.
**FadeConfig** is a scriptable object which contains animation curves and durations for fade in and fade out proccesses.
**CanvasFadeTransition** is using CanvasGroup as source with alpha value.
### Progress
**ProgressTransition** is an abstract class for handling loading progress display.
>[!NOTE]
>If the loading scene contains few objects, the loading progress will instantly fill to 100%. Therefore, it is better to test on heavy scenes.
**ImageProgressTransition** is using an image with filled type to display progress.
**SliderProgressTransition** is using slider to display progress.
**TextProgressTransition** is using TextMeshPro component to display progress with specified text format.
### Wait
**WaitTransition** is an abstract class for handling scene activation delay after it was fully loaded but before transition fade out.
>[!NOTE]
>For example, it allows you to setup loading screen message: *Press any key to continue!*.
**WaitInputKeyTransition** is awaiting for specified (including any) key code press using default unity input system.
### Events
**UnityEventTransition** contains unity events for fade in and fade out stages of transition. Can be used for calling methods without implementing new transitions.
**UnityEventProgressTransition** contains unity events on different progress stages: start (0), end (1) and each value update.