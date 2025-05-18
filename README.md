# Nine Lives Laboratory

The Nine Lives Laboratory is a place shrouded in mystery. The player awakens with no memory of how they arrived, the only clue to their escape lies in Kat, an unusually intelligent feline who seems to understand the secrets buried within the facility’s walls. As the player follows Kat deeper into the lab, they begin to uncover unsettling truths. But as the pieces come together, one question remains: was their arrival here truly an accident?
To uncover the secrets of the Nine Lives Laboratory the player will need to solve a puzzle with the help of Kat the cat


VR game for CS-444 course

docs: https://docs.google.com/document/d/1_WkB36oNDu4BM4dqhUGnGxLp7eM0t8F7UxKzdL2E7LU/edit?tab=t.0

useful assets:
https://www.kenney.nl/assets/page:8
https://assetstore.unity.com/zh-CN/3d
https://www.fab.com/category/3d-model?is_free=1


To be able to get the PDF document from the .rmd file some installations are needed:
- install.packages('tinytex')
- tinytex::install_tinytex()

## 📁 Script & Asset Attribution

| Type                     | Description                                       |
|--------------------------|---------------------------------------------------|
| 🟢 Produced              | Created entirely by our team.                     |
| 🟡 Adapted               | Modified from external sources.                   |
| 🔴 Unmodified            | Used as-is from external sources.                 |

---

<details>
<summary><strong>🔧 Assets: [3D models]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/LowPoly SciFi`     | 🔴 Unmodified | Free asset from [source](https://assetstore.unity.com/packages/3d/environments/sci-fi/free-lowpoly-scifi-110070)                 |
| `Assets/Rooms/animations`     | 🔴 Unmodified | Free asset from [source](https://assetstore.unity.com/packages/3d/characters/animals/animals-free-animated-low-poly-3d-models-260727)              |

</details>
<details>
<summary><strong>🔧 Basic Feature: [built-in XRIT ]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Climbing,...`     | 🔴 Unmodified | Free asset from [source](#)                  |
| `Rotation.cs`     | 🟡 Adapted | Button to change rotation type [source](https://discussions.unity.com/t/vr-swapping-turn-provider-locomotion-provider-from-ui/951489)|

</details>
<details>
<summary><strong>🔧 Movement Custom Feature: [Jumping as a cat]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/scripts/InputData.cs`        | 🔴 Unmodified | Script necessary to extract input data from the controllers. Tutorial and code download from [this video](https://youtu.be/Kh_94glqO-0?feature=shared).|
| `Assets/Rooms/scripts/CatJumpController.cs`   | 🟡 Adapted  | Make the cat jump by swinging both controllers. Code template from [this tutorial](https://youtu.be/Xf2eDfLxcB8?feature=shared), adapted with `InputData.cs` to handle inputs based on controllers motion.  |

</details>

<details>
<summary><strong>🔧 Non-movement Custom Feature: [Puzzles]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/scripts/cage_puzzle.cs`         | 🟢 Produced |    NOTES          |
| `Assets/Rooms/scripts/Skip_scene.cs`     | 🟢 Produced|  Button becomes interactable once the puzzle is solved and skips to the next scene    |
| `Assets/Rooms/scripts/memory.cs`     | 🟢 Produced| Implementation of specific order of material on an object used as a clue   |
| `Assets/Rooms/scripts/ScreenMaterialSquence.cs`     | 🟢 Produced| Implementation of specific order of texture on a specific material of an object used as a clue   |
| `Assets/Rooms/scripts/ButtonSquenceManager.cs`     | 🟢 Produced| Implementation of specific order at which buttons need to be pressed to solve puzzle   |
| `Assets/Rooms/scripts/LevelCompletion.cs`     | 🟢 Produced| Trigger when the level or puzzle is completed   |
| `Assets/Rooms/scripts/socket1/2/3.cs`     | 🟢 Produced| The socket can only connect with the assigned specific character to solve the puzzle. Once completed, it will trigger the cage to open |
| `Assets/Rooms/scripts/cage_interaction.cs`     | 🟢 Produced| Once triggered, play the cage opening animation |
| `Assets/Rooms/scripts/InteractionModeManager.cs`     | 🟢 Produced| Click to switch between Near-Far Interaction mode and Laser mode. In Near-Far Interaction mode, the cat will not follow. In Laser mode, the cat will automatically follow the laser pointer |
| `Assets/Rooms/scripts/plate.cs`     | 🟢 Produced| Control the plates and fishbones to move slowly and randomly around a central point in the water. When the cat touches a plate, the fishbone on it will be eaten. |
| `Assets/Rooms/scripts/SyncObject.cs` | 🟢 Produced| Key mechanism of the final room. Allow the transforms of an object to be mirrored on another object. |
| `Assets/Rooms/scripts/Breakable.cs` | 🟡 Adapted  | Allow the cat to break vases by knocking them down the shelves in the final room. Need to pair with a trigger zone with the script `BreakZone.cs`. Code template from [this tutorial](https://youtu.be/TaLvL0GdH-I?feature=shared) and adapted to our specific application. |
| `Assets/Rooms/scripts/BreakZone.cs` | 🟢 Produced| To trigger the break action of vases. |
</details>

<details>
<summary><strong>🔧 Non-movement Custom Feature: [Cat]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/scripts/kitty_follow.cs`     | 🟢 Produced |  After being freed from the container, the cat will automatically follow the player.    |
| `Assets/Rooms/scripts/kitty_follow_laser.cs`     | 🟢 Produced |  Control the cat to automatically follow the laser pointer in Laser Mode.    |
| `Assets/Rooms/scripts/cat_detector.cs`     | 🟢 Produced |  When the player holds the cat close enough to the right clue, the cat purrs and the controller vibrates.   |
| `Assets/Rooms/scripts/after_grad.cs`     | 🟢 Produced |  When player grab the cat it will meow    |
| `Assets/Rooms/scripts/CharacterSwitchManager.cs` | 🟢 Produced | Key mechanism of the final room. Allow the player to switch between human and cat. This includes switching position, rescaling the controllable character, enabling and disabling capacities depending on the controlled character. |

| `PET CARE`   | TYPE |NOTES        |
</details>
<details>
<summary><strong>🔧 Cybersickness Consideration: [Vision narrowing]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `cybersickeness.cs`     | 🟡 Adapted  |   NOTES    | Added simple script to turn off tunneling vignette, followed tuto from [source](https://www.youtube.com/watch?v=9Q6mEmZEoa4)   


</details>
<details>
<summary><strong>🔧 Other: [Narrative Story and Arts]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/Sync Rooms/Narrative Story`     | 🟡 Adapted | The final exploits the Timeline feature of Unity to create a narrative story and trigger specific story events. Adapted based on [this tutorial](https://youtu.be/AJBb-PA-nAk?feature=shared).        |
| `Assets/Rooms/Sync Rooms/Narrative Story`     | 🟢 Produced | The narration script are fully generated using [DeepSeek](https://www.deepseek.com/), then converted to AI voicelines using [OpenAI TTS](https://ttsopenai.com/).         |
| `Assets/Rooms/Textures` | 🟡 Adapted | Images used for notes and posters. Composed of web found images, our personal drawings, and images generated using [Sora](https://sora.chatgpt.com/) and Copilote (Joana c'est quoi ton truc mets le lien stp)

</details>
