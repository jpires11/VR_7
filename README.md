# Nine Lives Laboratory

The Nine Lives Laboratory is a place shrouded in mystery. The player awakens with no memory of how they arrived, the only clue to their escape lies in Kat, an unusually intelligent feline who seems to understand the secrets buried within the facility’s walls. As the player follows Kat deeper into the lab, they begin to uncover unsettling truths. But as the pieces come together, one question remains: was their arrival here truly an accident?
To uncover the secrets of the Nine Lives Laboratory the player will need to solve a puzzle with the help of Kat the cat


VR game for CS-444 course

docs:https://docs.google.com/document/d/1_WkB36oNDu4BM4dqhUGnGxLp7eM0t8F7UxKzdL2E7LU/edit?tab=t.0

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
| `Assets/LowPoly SciFi`     | 🔴 Unmodified | Free asset from [[source](https://assetstore.unity.com/packages/3d/environments/sci-fi/free-lowpoly-scifi-110070)](#)                  |
| `Assets/Rooms/animations`     | 🔴 Unmodified | Free asset from [[source](https://assetstore.unity.com/packages/3d/characters/animals/animals-free-animated-low-poly-3d-models-260727)](#)                  |

</details>
<details>
<summary><strong>🔧 Basic Feature: [built-in XRIT ]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Climbing,...`     | 🔴 Unmodified | Free asset from [source](#)                  |
| `Rotation.cs`     | 🟡 Adapted | Button to change rotation type [[source](https://discussions.unity.com/t/vr-swapping-turn-provider-locomotion-provider-from-ui/951489)](#)|

</details>
<details>
<summary><strong>🔧 Movement Custom Feature: [Jumping as a cat]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `name file`         | 🟢 Produced | Implements the core logic for feature X         |
| `name file`   | 🟡 Adapted  | Resized and color-corrected                     |
| `name file`     | 🔴 Unmodified | Free asset from [source](#)                  |

</details>

<details>
<summary><strong>🔧 Non-movement Custom Feature: [Puzzles]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/scripts/cage_puzzle.cs`         | 🟢 Produced |    NOTES          |
| `Assets/Rooms/scripts/Skip_scene.cs`     | 🟢 Produced|  Button becomes interactable once puzzle is solve and skip to next scene    |
| `Assets/Rooms/scripts/memory.cs`     | 🟢 Produced| Implementation of specific order of material on a object used as a clue   |
| `Assets/Rooms/scripts/ScreenMaterialSquence.cs`     | 🟢 Produced| Implementation of specific order of texture on a specific material of an object used as a clue   |
| `Assets/Rooms/scripts/ButtonSquenceManager.cs`     | 🟢 Produced| Implementation of specific order at which button need to be pressed to solve puzzle   |
| `Assets/Rooms/scripts/LevelCompletion.cs`     | TYPE| NOTES   |
</details>

<details>
<summary><strong>🔧 Non-movement Custom Feature: [Cat]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `Assets/Rooms/scripts/kitty_follow.cs`     | 🟢 Produced |   NOTES    |
| `Assets/Rooms/scripts/cat_detector.cs`     | 🟢 Produced |  When player hold the cat close enough to the right clue cat purr and controller vibrates    |

| `PET CAR`   | TYPE |NOTES        |
</details>
<details>
<summary><strong>🔧 Cybersickness Consideration: [Vision narrowing]</strong></summary>

| File/Asset                     | Type       | Notes                                           |
|-------------------------------|------------|-------------------------------------------------|
| `FILE`     | 🟡 Adapted  |   NOTES    |
</details>
