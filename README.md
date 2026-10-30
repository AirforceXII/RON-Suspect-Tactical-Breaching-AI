# RON Suspect Tactical Breaching AI

[中文教程](docs/UASSETGUI-GUIDE.zh-CN.md) · [AI 文件夹关系](docs/AI-FOLDERS.zh-CN.md) · [English overview](docs/REFERENCE.en.md)

## 中文

记录本项目怎样把《新美国》（A New America / MLO / Tower）的原版小队破门链接到《父之罪》（Sins of the Father / Sins），并配置 **列队 → 踢门 → 投刺痛弹 → 入室** 的流程。

教程以阶段12 `Sins-MLOBreach-HighWeight-StingerAlways-r1` 为例，附真实 UAssetGUI 界面截图、字段路径、修改前后值、装备引用及 AI 目录说明。它是资产修改教程，不是另一仓库中的 UE4SS 反穿模组。

### 从哪里开始

1. 阅读[逐步修改指南](docs/UASSETGUI-GUIDE.zh-CN.md)：具体打开哪个文件、哪个字段需要改、哪个只需核对。
2. 阅读[AI 文件夹执行关系](docs/AI-FOLDERS.zh-CN.md)：Archetypes、ActionPresets、Actions、AIActionActivity、Squads 等怎样通过引用和运行时对象连接。
3. 对照[机器可读修改清单](data/changes.json)、[独立读取的字段值](data/verified-values.json)和[投掷许可修改示例](examples/enable-grenade-branch.cs)。
4. 最后查看[证据与适用边界](docs/EVIDENCE.zh-CN.md)，区分配置、运行时活动与实际动作。

### 核心改动

| 文件 | 作用 |
| --- | --- |
| `AI/Squads/DefaultSquadRules` | 原3项活动后追加官方 BreachandClear、SoloBreach |
| `AI/Archetypes/Core/Suspect_Terrorist` | 原7项 SquadActions 后补 Leader、Solo 映射，保留已有 Support |
| `DataTables/AI/AIDataTable_Hotel` | 只改 `sp_m_shooter_a/c`：角色资格、MLO 语音、每套装备3枚刺痛弹 |
| `AI/ActionPresets/AP_Breach_Leader`、`AP_Breach_Follower`、`AP_SoloBreach` | 可选测试增强：根 `Action.Weight` 从5改20 |
| `AI/Squads/SquadActivities/BP_Squad_BreachandClear` | `SetCanUseItem` 每次返回前启用原版投掷分支 |

**投掷物就是原版 `Grenade_Stinger_V2_C`**，放在 `WeaponLoadouts → Grenades`。没有为此更换主武器，也没有增加一把用于投弹的发射器。

UAssetGUI 用于实际字段定位、普通属性检查和对照。跨包完整结构复制及 `SetCanUseItem` 的已编译 Kismet 修改，本项目实际使用 UAssetAPI 类型化工具完成；教程会明确指出这一步，避免把默认值勾选框当成函数逻辑。

### 适用范围

- 本次资产研究对应 RON 1.5.3、UE 5.3.2、1.5.3 usmap。
- 核心接入和阶段12配置已经离线回读核验。父之罪旧高权重版本有真实小队活动、当前 `KickDoorActivity` 和玩家回放脚踢证据。
- “每次启用投掷许可”不等于库存耗尽或动作中断时仍保证投出；完整多人列队、投弹、清房与本破门包的主机单装联机，未被本教程扩大为已验收。
- `DefaultSquadRules`、Terrorist、预设和小队蓝图属于共享资产，其他引用者也可能受影响。迁移到另一张地图时，先找该地图真正的生成行、原型和规则，不能只按地图名字猜表。

## English

This repository documents how the original MLO/Tower squad-breaching chain was connected to **Sins of the Father**, with the intended sequence **stack up → kick the door → throw a Stinger → enter**.

It covers the stage12 `Sins-MLOBreach-HighWeight-StingerAlways-r1` asset configuration, including real UAssetGUI screenshots, exact field paths, before/after values, grenade references, and the relationships between the AI folders.

- [Step-by-step Chinese guide with screenshots](docs/UASSETGUI-GUIDE.zh-CN.md)
- [English technical overview](docs/REFERENCE.en.md)
- [AI folder relationships, Chinese](docs/AI-FOLDERS.zh-CN.md)
- [Change manifest](data/changes.json) and [independently read field values](data/verified-values.json)

The throwable is the game's existing `Grenade_Stinger_V2_C`, added to `WeaponLoadouts.Grenades`, three entries per loadout. Primary firearms were not replaced to enable throwing.

UAssetGUI is used for field inspection and ordinary property work. The actual cross-package structural copying and compiled `SetCanUseItem` change were made with typed UAssetAPI tools, not by dragging Blueprint graph nodes in UAssetGUI.

The original project targeted RON 1.5.3 / UE 5.3.2. Static configuration checks, runtime activity observations, and player-visible actions are reported separately. Earlier Sins gameplay confirmed kicking; enabling the grenade branch does not guarantee a throw when stock is empty, execution is interrupted, or native conditions reject it. Full squad coordination and host-only multiplayer for this breaching package are not claimed as verified. The separate UE4SS retaliation mod's client result is not evidence for this package.
