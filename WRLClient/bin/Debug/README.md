## WebRunLocal
### 1. 程序说明
WebRunLocal旨在实现网页(Web Page)和本地程序(Local App)之间的动态调用，WebRunLocal作为Windows本地系统服务在客户端电脑运行，网页中使用JavaScript通过http的方式调用WebRunLocal服务，WebRunLocal服务根据传入的参数，动态的调用本地程序并返回结果，参数格式使用JSON，方便在网页中进行解析。

在网页中调用本地程序，一般采用的技术方案是ActiveX、OCX及NPAPI插件技术，其中ActiveX仅能够在IE浏览器中使用，随着Chrome，Firefox等浏览器基于安全性及稳定性的考虑，不再支持插件，OCX和NPAPI插件在高版本的浏览器上均不能正常的使用，想要访问本地程序就不得不固定浏览器版本，项目兼容性比较差。有的项目会在网页中调用第三方提供的本地程序，用以实现对电脑硬件的调用或者作为第三方项目之间的交互方式，然而第三方提供的程序稳定性无法得到保证，动辄无响应或者崩溃导致浏览器崩溃，极大的降低用户体验度。WebRunLocal在网页和本地程序之间增加了一个中间服务层，即保证了网页对本地程序的正常调用，又可以降低网页和本地程序之间的耦合度，增加程序的可拓展性，解决了在网页中调用本地程序出现的各种问题。

### 2. 兼容性要求
系统兼容性：1、全面兼容Windows XP以上微软桌面操作系统；2、.net framework 4.0以上运行环境。
浏览器兼容性：全版本浏览器，支持http协议即可使用。

### 3. 使用场景
根据本地程序的种类，本地程序大致分为两种:

1. DLL插件，用于调用电脑本地硬件（打印机、扫描仪、读卡器等）或者通过DLL插件和第三方项目程序进行交互。
2. Exe可执行程序，主要用于集成第三方程序。

### 4. 程序优点

1. 低耦合，http请求本地服务的方式可以设置异步同步请求方式以及超时时间，不会因为本地程序的原因导致浏览器长时间无响应或崩溃。
2. 强兼容可拓展，WebRunLocal程序在调用本地程序时，采用动态编译技术，再也不需要将每个本地程序都封装为插件，本地程序即拿即用，方便高效。
3. 解决了大多数高版本浏览器不再支持插件的问题。

### 5. 程序安装及使用说明
解压软件包至实际磁盘，双击运行WRLClient.exe，然后分别单击界面上的“安装服务”，“启动服务”两个按钮，完成WebRunLocal本地系统服务的安装及启动，服务已设置为开机自启动，保证随时在线可用。
#### 5.1 目录说明
Log：日志目录
Plugins：本地程序目录
WRL.exe：windows 服务程序
WRL.exe.config：windows 服务程序配置文件
WRLClient.exe：windows服务管理程序
WebRunLocal测试.html：测试用网页
#### 5.2 参数说明
网页中http请求参数格式如下：
```
{
  "TYPE": "调用类型，1：调用DLL，2：调用Exe",
  "PATH": "本地程序相对路径",
  "METHOD": "本地程序方法名，调用DLL时使用",
  "PARAM": [{
    "TYPE": "入参类型，调用DLL时使用",
	"VALUE": "入参值",
	"MODE": "入参传递方式，调用DLL时使用"
  }],
  "RETRUN_TYPE": "返回值类型，调用DLL时使用"
}
```
本地系统服务返回结果格式如下：
```
{
  "CODE": WebRunLocal服务处理状态，0：服务处理成功，-1：服务处理失败,
  "MSG": "服务处理失败信息",
  "RETURN": {
    "RESULT": "本地程序返回值",
    "VALUES": [本地程序出参返回值，调用DLL时使用]
  }
}
```
#### 5.3 调用Exe可执行程序
在浏览器中打开"WebRunLocal测试.html"进行测试，在文本框中输入如下内容，点击“发送消息”按钮，完成对测试用程序CallExe.exe的启动测试，软件启动成功会在主界面打印出入参。
{"TYPE": "2","PATH": "Plugins\\CallExe\\CallExe.exe","PARAM": [{"VALUE": "1111"},{"VALUE": "222"},{"VALUE": "333"}]}

#### 5.4 调用DLL插件
WebRunLocal服务调用DLL采用了C#的动态编译功能，使用者不需要对本地程序进行二次封装，直接将本地程序放入Plugins目录即可使用，使用者根据DLL插件对外提供的方法设置不同的入参参数。测试用DLL插件CallDLL.dll模拟了经常使用的DLL封装方法的况用以测试，DLL插件接口方法定义如下：
```
extern "C" __declspec(dllexport) int add(int a,int b);//返回a+b的结果
extern "C" __declspec(dllexport) int CallString(char* output);//方法返回值为1，output为输出参数
extern "C" __declspec(dllexport) int CallStringInAndOut(char* input, char* output);//方法返回值为1，input为输入参数，output为输出参数
```
在浏览器中打开"WebRunLocal测试.html"，在文本框中分别输入以下内容，点击“发送消息”按钮，完成对测试用插件CallDLL.dll的调用测试。
1. add方法测试入参：{"TYPE": "1","PATH": "Plugins\\CallDLL\\CallDLL.dll","METHOD": "add","PARAM": [{"TYPE": "int","VALUE": "1","MODE": "0"},{"TYPE": "int","VALUE": "2","MODE": "0"}],"RETRUN_TYPE": "int"}，出参：{"CODE": 0,"MSG": "","RETURN": {"RESULT": 3,"VALUES": []}}
2. CallString方法测试入参：{"TYPE": "1","PATH": "Plugins\\CallDLL\\CallDLL.dll","METHOD": "CallString","PARAM": [{"TYPE": "StringBuilder","VALUE": "","MODE": "1"}],"RETRUN_TYPE": "int"}，出参：{"CODE": 0,"MSG": "","RETURN": {"RESULT": 1,"VALUES": ["HelloWorld!"]}}
3. CallStringInAndOut方法测试入参：{"TYPE": "1","PATH": "Plugins\\CallDLL\\CallDLL.dll","METHOD": "CallStringInAndOut","PARAM": [{"TYPE": "StringBuilder","VALUE": "HelloWorld","MODE": "0"},{"TYPE": "StringBuilder","VALUE": "","MODE": "1"}],"RETRUN_TYPE": "int"}，出参：{"CODE": 0,"MSG": "","RETURN": {"RESULT": 1,"VALUES": ["HelloWorld123456"]}}

通过对测试用DLL插件三个方法的调用测试，可以学习根据DLL提供的方法设置合适的入参参数然后进行插件的调用，使用这种方式可以避免对本地程序进行二次封装使用，方便高效。

#### 5.5 配置说明
软件包根目录下的WRL.exe.config为WebRunLocal服务的配置文件，通过它可以对本地系统服务进行一些配置，配置内容如下：
ListenerPort：设置http监听端口
PramaterLoggerPrint：是否将系统服务的入参出参输出到日志文件


启发于：[https://github.com/wangzuohuai/WebRunLocal](https://github.com/wangzuohuai/WebRunLocal)