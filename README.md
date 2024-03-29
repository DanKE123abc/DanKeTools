<img src="./favicon.png" width="100" height="100" />

<center><h1>DanKeTools</h1></center>

### 自2023.7.21起不再添加新功能，推荐使用[Dewity](https://github.com/DanKE123abc/DewityKit)

![Language](https://img.shields.io/badge/Language-Csharp-C#) ![LICENSE](https://img.shields.io/badge/LICENSE-MIT-yellow) ![Author](https://img.shields.io/badge/Author-DanKe-blue) ![Unity](https://img.shields.io/badge/Unity-2021.3.0f1-red)

DanKeTools是基于Unity的一个基础开发框架，包含了一些项目中的常用组件。

#### [文档](https://github.com/DanKE123abc/DanKeTools/blob/main/Assets/DanKeTools/README.md)

### 环境

Unity开发版本： 2021.3.0f1

### 模块简介

Base —— 单例模式、数据转换类、多任务类

Editor —— 编辑器工具

Event —— 事件中心模块

IO —— 输入管理、文件管理模块

Mono —— Mono管理器模块

Net —— http模块、在线机器翻译

Json —— Json处理类

Log —— 日志类

Pool —— 缓存池模块

Scene —— 场景加载模块

UI —— UI基础面板、UI事件管理器模块

Voice —— 音效管理器模块

Utils —— 常用工具类

FSM —— 有限状态机

Thread —— 子线程管理器

### 更新日志

**v2.0.0**

-- 可能是最后一个大版本了，开发的时候没有想好怎么规划，导致项目太混乱了，可以看一下我的新项目：[DewityKit](https://github.com/DanKE123abc/DewityKit) --

增加Json处理类（基于LitJson）

增加Log类（包含游戏内控制台，全新的Debug类）

增加多任务类

FileManager支持读写Json

ToJson合并到Json处理类中

优化的部分函数

修复了一些bug

**v1.3.0**

增加了网络在线机器翻译模块（利用Baidu翻译接口）

增加了常用类——数据转换类（DEncoding）

Utils.Base64整合到DEncodeing中

Request库函数优化

删除UIManager

重写单例模式

重写子线程管理器

淘汰了旧方法

**v1.2.0**

修复缓存池提示的Bug

增加了部分提示

新增子线程管理器

**v1.1.3**

支持Rsa加解密

修复了一些Bug

**v1.1.2**

更改协议为MIT

修复http模块输出Bug

**v1.1.0**

http模块新增下载文件功能

FSM有限状态机

**v1.0.0**

版本1

### 开源许可

本项目MIT开源协议，由DanKe（github@DanKE123abc）领导开发。

DanKeTools使用了以下开源库，向这些为开源奉献的开发者致以敬意！

```
---------------------
DanKeTools
—— DanKE123abc
MIT License
https://github.com/DanKE123abc/DanKeTools
---------------------
litjson
—— LitJSON
litjson License
https://github.com/LitJSON/litjson
---------------------
```

部分代码受到网络上部分文章和代码片段的影响，感谢分享这些知识的人！
