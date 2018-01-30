# [Windows.Media.Ocr.Cli]( https://github.com/zh-h/Windows.Media.Ocr.Cli)
Ocr 命令行工具， **本地调用** UWP API Windows.Media.Ocr 支持多种语言识别。

## 使用
### 下载
https://github.com/zh-h/Windows.Media.Ocr.Cli/releases

### 命令行执行
```powershell
PS C:\Tools>Windows.Media.Ocr.Cli.exe .\x.png
9·哪位科学家发现了电磁感应现象？
```
![x-out.png](https://github.com/zh-h/Windows.Media.Ocr.Cli/blob/master/Windows.Media.Ocr.Cli/x-out.png?raw=true)

### 查看帮助
```powershell
PS C:\Tools>Windows.Media.Ocr.Cli.exe -h
Usage: Windows.Media.Ocr.Cli.exe [options...] <image file path>
Example: Windows.Media.Ocr.Cli.exe x.png
-l      <language>  Default:zh-Hans-CN   Specify language to reconizing
-s      Show all supported languages
-h      Show help like this
```

### 运行依赖
1. 需要 Windows 10 系统，其他平台没有测试，理论上可以支持所有支持 UWP 的设备，包括 Xbox 甚至安装 Windows 10 IoT 的树莓派，不太了解 Xamarin 能不能引入 UWP 的 runtime，如果能引入 UWP 就可以用在 iOS 和 Android 平台。

## 功能
- [x] 文字识别
- [ ] 输出图片显示文字区域方块
- [ ] 提供提高对比参数（有时后明明图片很清晰，但是不调高对比度什么文字都没识别）

## 参考
- Get Started With Optical Character Recognition with the OCR Library for Windows Runtime <div align="center">
  <a href="https://www.youtube.com/watch?v=9TXl0sUHEMg"><img src="https://img.youtube.com/vi/9TXl0sUHEMg/0.jpg" alt="Get Started With Optical Character Recognition with the OCR Library for Windows Runtime"></a>
</div>

- 微软官方文档 https://docs.microsoft.com/en-us/uwp/api/Windows.Media.Ocr
- 官方示例 APP https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/OCR


## 声明
仅供学习参考，请勿用于服务器端生产环境。

请遵守微软 Windows Runtime 使用协议 [MICROSOFT OCR LIBRARY FOR WINDOWS RUNTIME](https://www.microsoft.com/web/webpi/eula/windows_runtime_ocr_library_terms_of_use.htm)
> distribute Distributable Code to run on a platform other than the Windows Store or Windows Phone;