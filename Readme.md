# Windows 10 文件夹内 DDS 缩略图预览  

本程序基于xMadHack的ImageWarp二次开发, 重构原作者的缩略图生成逻辑: 每次调用时都复制一份dds临时文件并调用texconv.exe处理, 然后又写一次文件.   
为了避免对硬盘的频繁写操作, 并加速缩略图的展示, 本程序经重构采用直接在内存中处理生成缩略图的方式, 然后传回给windows文件管理器.

## 背景 
作者使用了SageThumbs和MysticThumbs等等软件, 个个都异常没法预览, MysticThumbs一开始安装时预览成功了, 但是过了不久就出现故障, 卸载重装多次都无法再正常工作, 而且好像要收费？
实在是非常费解, 这么简单的一个DDS贴图预览功能, 怎么没几个能正确处理的。  

后面找到这个开源软件, 试了下安装很快, 装上就能用, 鬼使神差地打开看了一下源码, 发现它竟然是在windows请求预览的缩略图时, 复制一份文件到它的工作目录, 使用texconv.exe处理后生成缩略图再传回给请求方   

速度慢不说, 还伤硬盘. 而texconv.exe是微软DirectXTex(C++)项目里的一个程序, 刚好它也开源, 所以把它的源码也clone下来, 并将依赖的部分代码一并迁移到本项目, 然后修改了原作者的dds缩略图生成逻辑, 桥接到DirectXTex的缩略图生成代码(本项目是 C# + VB 写的， 故需要桥接), 并修改这部分代码让它从内存中加载dds数据, 处理完后再把缩略图写回内存交还给Windows文件夹展示.

## 使用  
下载发布的压缩包, 解压后运行里面的XmhShellExtensionsInstaller.exe, 根据界面提示安装就行, 注意解压后的文件夹跟程序运行有关故不能删除, 移动位置后需要进行重装.  
安装完成后如果没有意外就能正常预览贴图了, 如果安装异常, 请打开解压文件夹内的InstallerLog.txt自行排查或贴上来提issue.

## 问题排查  
如果预览时遇到问题, 请打开解压文件夹内的config/config.json(会在预览调用后被自动创建), 将里面的log字段设置成true,
之后再去尝试预览dds文件并生成日志, 日志文件在解压文件夹内的log目录下, 如果你会编程, 可以自己根据错误提示去修改源码调试, 调试源码需要使用Vistual Studio 2022. 如果不会, 请携带你上你的log日志以及出错的dds文件提issue.

## 注意  
该软件只在我的Win10上测试, Win11能否正常运行未知.

---

# 以下为原作者的Readme内容 (翻译并去除了部分内容)  

原作者仓库: https://github.com/xMadHack/ImageWarp

一个用于图像处理的工具套件。

包含的功能特色：
- 支持在 Windows 资源管理器中生成 **DDS 文件缩略图**（包括压缩格式）。
- 针对 DDS 和 PNG 文件功能的**右键菜单**快捷方式。
- **TexPatcher**：一款用于快速对纹理文件应用补丁的工具。（主要用于修补像《天际/上古卷轴5》这类游戏中的纹理身体接缝，解决身体和头部纹理不匹配的问题。）
- **LiteView**：一款支持 DDS 文件格式的轻量级图像查看器。

**仅支持 Windows x64 操作系统** **需要安装 .Net 6.0 和 .NetFramework 4.7.2** **测试环境：** - Windows 10 x64

***不包含纹理文件。*** 纹理需要从外部自行下载。

## 用户使用说明

### 安装步骤
通常情况下，安装说明会列在托管二进制文件链接的网站上。  
以下列出的是通用使用方法。

首先，请安装所需的运行时环境（两者都需要安装）：

https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.1-windows-x64-installer

https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-web-installer

1. Xmh Tools 的二进制文件应该打包在一个 zip 压缩包中。
1. 将文件解压到目标应用程序路径（即你希望安装该软件的位置）。
1. 如需启用外壳扩展（Windows 资源管理器中的 DDS 缩略图以及图像右键菜单），请运行 "XmhShellExtensionsInstaller.exe" 文件并按照提示操作。

## 开发者说明
该解决方案目前仅在 Visual Studio 2022 社区版（Community Edition）下进行了测试。

### 编译构建说明
1. 获取源代码。
1. 在 Visual Studio 2022 中打开它。
1. 编译为 Debug x64 或 Release x64 版本。

所有需要的依赖库应该都会通过 NuGet 自动下载。

### 生成输出文件

使用“发布”（Publish）功能（右键点击项目，然后选择“发布...”）来为以下项目创建输出：

- TexPatcher
- ImgConvertCmd
- XmhShellExtensionsInstaller

以下项目无法使用“发布”工具，必须手动复制其输出文件：

- LiteView

## 许可证
- **ImageWarp**（又名 [Xmh] Tools）  
即当前的仓库以及所生成的工具。
本解决方案（指 Visual Studio 解决方案，包括其旗下的所有项目）采用的许可证为：GNU GPL v3 许可证。  
https://www.gnu.org/licenses/gpl-3.0.txt

### 外部依赖库
**Emgu CV** 在编译构建时通过 NuGet 安装。  
https://www.emgu.com/  
其许可证（GNU GPL v3 许可证）副本已存放在 Licenses 文件夹中。  

**SharpShell**（MIT 许可证）  
**OpenTK**（MIT 许可证）

