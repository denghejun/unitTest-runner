### .NET 构建工具 Cake 集成化单元测试
* 集成NUnit Console Runner 自动编译、运行 Unit Test Assembly
* 构建前的清理工作
* 构建后的单元测试结果报表生成

### 使用说明

###### 只需关注 run.bat 文件
设定好你所需要的参数，直接执行`run.bat`.
```.\core\cake\cake.exe -workspace=C:\Users\ld71\Desktop\temp -task=Core -mode=Release -output=C:\report -popup=true```
###### 参数说明
* `-workspace`:不是必需的，默认为`build.cake`文件所在路径的上层目录（`../`）; 标识解决方案文件（`*.sln`）所在的文件夹路径


* `-task`:不是必需的，默认`Core`;标识要执行的 `Task Name`(`Clean`, `Build`, `Report`, `Core`)


* `-mode`:不是必需的，默认`Release`;标识编译模式（`Debug`、`Release`）


* `-output`:不是必需的，默认`build.cake`文件所在目录`report`文件夹; 标识测试报表存放路径;


* `-popup`:不是必需的，默认：`true`，标识测试完成后，是否立即打开测试结果报表文件（`report-xxx.html`）;

### 约定
该构建脚本默认会运行指定`-workspace`下所有`{-workspace}/**/bin/{-mode}/*.Tests.dll`程序集中的使用NUnit编写的单元测试（`-workspace`、`-mode`见上一小节`参数说明`）。
