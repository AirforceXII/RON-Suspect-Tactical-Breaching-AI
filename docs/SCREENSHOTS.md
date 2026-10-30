# UAssetGUI 真实截图索引 / Actual screenshots

拍摄日期：2026-10-30。程序：UAssetGUI v1.1.0；界面选择 UE 5.3 与 `Mappings V1.5.3(2026.10.14)`。以下都是实际打开既有阶段12结果文件后的界面截图，没有生成或重绘表格内容。

来源目录代号：

- **payload**：`Sins-MLOBreach-HighWeight-StingerAlways-r1/payload/ReadyOrNot/Content/`
- **context**：`sins-stinger-always-12/build/context/ReadyOrNot/Content/`，带完整依赖的同一结果副本

| 截图 | 来源与资产 | 界面路径 / 画面验证点 |
| --- | --- | --- |
| [01-default-squad-breach.jpg](images/01-default-squad-breach.jpg) | payload / `Blueprints/AI/Squads/DefaultSquadRules` | Export 2 → SquadRulesData → SquadActivityData(5) → 3；`BP_Squad_BreachandClear_C` |
| [01-default-squad-solo.jpg](images/01-default-squad-solo.jpg) | 同上 | 第4项；`BP_Squad_SoloBreach_C` |
| [02-suspect-squad-actions.jpg](images/02-suspect-squad-actions.jpg) | context / `Blueprints/AI/Archetypes/Core/Suspect_Terrorist` | 根数据导出 → SquadActions；9项映射 |
| [02-suspect-leader-tag.jpg](images/02-suspect-leader-tag.jpg) | 同上 | SquadActions → 7 → ActionTag；`Squads.BreachAndClear.Leader` |
| [02-suspect-leader.jpg](images/02-suspect-leader.jpg) | 同上 | SquadActions → 7；`Preset = AP_Breach_Leader` |
| [02-suspect-solo-tag.jpg](images/02-suspect-solo-tag.jpg) | 同上 | SquadActions → 8 → ActionTag；`Squads.BreachAndClear.Solo` |
| [02-suspect-solo.jpg](images/02-suspect-solo.jpg) | 同上 | SquadActions → 8；`Preset = AP_SoloBreach` |
| [03-hotel-roles.jpg](images/03-hotel-roles.jpg) | payload / `Blueprints/DataTables/AI/AIDataTable_Hotel` | Table Info → sp_m_shooter_a → AllowedSquadsActivityRoles → 内层容器；8个角色标签 |
| [04-hotel-speech.jpg](images/04-hotel-speech.jpg) | 同上 | RandomCharacterMesh → 0；`CharacterSpeechHandle = TowerMLO_01` |
| [05-hotel-grenades.jpg](images/05-hotel-grenades.jpg) | 同上 | WeaponLoadouts → 0 → Grenades(3)；Package 为 Grenade_Stinger_V2，Value 2 为 Grenade_Stinger_V2_C |
| [06-leader-weight.jpg](images/06-leader-weight.jpg) | context / `Blueprints/AI/ActionPresets/AP_Breach_Leader` | Export 7 → AIActionPresetData → Action；`Weight = 20` |
| [07-set-can-use-item.jpg](images/07-set-can-use-item.jpg) | context / `Blueprints/AI/Squads/SquadActivities/BP_Squad_BreachandClear` | Export 17 → UStruct Data → ScriptBytecode；CanUseItem 绑定、EX_True、Return 和 EndOfScript，共3条表达式 |

图片里显示的导出号是 GUI 的1基编号，JSON导出数组使用0基索引。字段和函数名称比固定编号更适合跨版本定位。

独立 payload 打开 Terrorist 时曾出现缺依赖与146个未解析导出的提示；完整 context 打开没有再出现该提示。最终02系列使用完整context重新拍摄的版本。不要把单个文件的可见字段当成全资产可安全回写的证明。

07中标题的 `*` 是打开字节码编辑视图后出现的界面状态；本次没有输入或保存资产。拍摄后独立复比了14个payload文件与原构建散列，以及context对应文件，结果一致。

The screenshots show real UAssetGUI windows opened on the existing stage12 output assets. The `02` series uses the full dependency context after an isolated-payload dependency warning. The `*` in the bytecode-view title was not a saved asset modification. Post-capture checks confirmed that all 14 payload files and their context counterparts still match the original build hashes.

图片尺寸与 SHA-256 见 [screenshots.json](../data/screenshots.json)。
