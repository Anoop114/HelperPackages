## [0.0.3-preview] - 2023-10-23
## Change Log
- Create REST API for `GET, POST, PUT`
- In both `Async` and `Coroutine` ways.

### Use
- Add a *sample scene* that helps to understand the implementation.
### Added
- [Sample Scene](https://github.com/Anoop114/HelperPackages/tree/main/Samples~/ServerDemo) Sample Scene Folder.
---
Coroutine Server Call
- [ServerCall](https://github.com/Anoop114/HelperPackages/blob/main/HelperFunctions/Server/ServerCall.cs) ServerCall Main Logic API.
- [Coroutine (GET)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L50) Coroutine GET API Implementation.
- [Coroutine (PUT)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L72) Coroutine PUT API Implementation.
- [Coroutine (POST)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L110) Coroutine POST API Implementation.
---
Async Server Call
- [ServerCallAsync](https://github.com/Anoop114/HelperPackages/blob/main/HelperFunctions/Server/ServerCallAsync.cs) ServerCallAsync Main Logic API.
- [Async (GET)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L170) Async GET API Implementation.
- [Async (PUT)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L186) Async PUT API Implementation.
- [Async (POST)](https://github.com/Anoop114/HelperPackages/blob/main/Samples~/ServerDemo/Script/ServerExample.cs#L212) Async POST API Implementation.
---


## [0.0.2-preview] - 2023-10-13
## Change Log
- Add editor tool for the scene to add directly in the Inspector window.
- Add Byte convert *convert byte[] to object and object to byte[]*
### Use
- You can just use it like `public SceneField myScene` and drag a Scene to the Inspector and use `Application.loadLevel( myScene )` or `SceneManager.LoadScene( myScene )`

### Added
- [SceneField](https://github.com/Anoop114/HelperPackages/tree/main/HelperTools/Scene/SceneField.cs) SceneFiled.cs File.
- [ByteManipulation](https://github.com/Anoop114/HelperPackages/blob/main/HelperFunctions/Utilities/ByteManipulation.cs) ByteManipulation.cs File.
---


## [0.0.1-preview] - 2023-10-09
## Change Log
Add Camera, Random, Coroutines, UI, Utilites helper functions.

#### About Change
Here we write upgrading notes for brands. It's a team effort to make them as
straightforward as possible.
 
### Added
- [Camera](https://github.com/Anoop114/HelperPackages/tree/main/HelperFunctions/Camera)
    All Camera related scripts here.
- [Random](https://github.com/Anoop114/HelperPackages/tree/main/HelperFunctions/Random)
    All Random related scripts here.
- [Coroutines](https://github.com/Anoop114/HelperPackages/tree/main/HelperFunctions/Routines)
    All Coroutines related scripts here.
- [UI](https://github.com/Anoop114/HelperPackages/tree/main/HelperFunctions/UI)
    All UI related scripts here.
- [Utilites](https://github.com/Anoop114/HelperPackages/tree/main/HelperFunctions/Utilites)
      All Utilites related scripts here.
 
### Changed
    Add Camera, Random, Coroutines, UI, Utilites helper functions.
### Fixed
    nothing
 


