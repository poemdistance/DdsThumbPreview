# Windows 10 文件夹内 DDS 缩略图预览
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
# 以下为原作者的Readme内容# Xmh Tools
A tools suite for image processing.  

Project hosted at: https://github.com/xMadHack/ImageWarp  
For more information of the project and the tools, visit the wiki: https://github.com/xMadHack/ImageWarp/wiki  

Included features: 
- Enables generation of **DDS files thumbnails** in window explorer (including compressed formats)
- **Context Menu** with shortcuts for DDS and PNG files functionalities.
- **TexPatcher**: a Tool to quickly apply patches to texture files. (Mosly useful to patch body seams in textures like Skyrim, when having unmatching body and head textures.)
- **LiteView**: A light-weight image viewer supporting DDS file formats.

**Only Windows x64 operative systems are supported**  
**Requires .Net 6.0 and .NetFramework 4.7.2**  

**Tested in:**  
- Windows 10 x64

***Textures not included.***  
They have to be downloaded externally.

## Contribute!

With a pull request: https://github.com/xMadHack/ImageWarp  
Be a Patreon: https://www.patreon.com/xMadHack  
Donate to the effort: https://paypal.me/xMadHack  

## Instructions For Users

### Legitimate Download Sites

Do no accept binaries from sites that are not listed here.   
Legitimate:  
- Official Mod in NexusMods. https://www.nexusmods.com/skyrimspecialedition/mods/60852
- Releases of  https://github.com/xMadHack/ImageWarp/releases

### Installation
Normally, the installation instruction are listed in the site that hosted the link to the binaries.  
Here is listed the general usage.  

First, install the required runtimes (both of them):

https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.1-windows-x64-installer

https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-web-installer

1. The binaries of Xmh Tools should come packaged in a zip file. 
1. Extract the files to the target application path (Where you want the application to be installed).
1. To enable Shell Extensions (DDS thumbnails in windows explorer, and image context menu), execute the file
"XmhShellExtensionsInstaller.exe" and follow instructions.

## For Developers
The solution is currently tested only with Visual Studio 2022 Comunity Edition.

### Building Instrutions
1. Get the source code.
2. Open it in VisualStudio 2022.
3. Build Debug x64 or Release x64.

All the required libraries should be downloaded automatically by NuGet.

### Creating outputs

Use the Publish feature (right click a project, and select Publish...) to create the outputs for:

- TexPatcher
- ImgConvertCmd
- XmhShellExtensionsInstaller

The next projects can't be used with the Publish tool, and the outputs must be copied manually.  

- LiteView

## License
- **ImageWarp** (AKA [Xmh] Tools)  
This is the current repository and the produced tools.
The solution (as in Visual Studio Solution, including all its projects) license is: GNU GPL license v3. 
https://www.gnu.org/licenses/gpl-3.0.txt

### External Libraries.
**Emgu CV**
Installed through NuGet when building.  
https://www.emgu.com/ 
Licence (GNU GPL license v3) copied in the Licenses folder.  

**SharpShell** (MIT license)
**OpenTK** (MIT license)
