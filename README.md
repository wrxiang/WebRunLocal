## WebRunLocal
### 1. 程序说明
WebRunLocal旨在实现网页(Web Page)和本地程序(Local App)之间的动态调用，WebRunLocal作为Windows本地托盘程序在客户端电脑运行，网页中使用JavaScript通过http的方式WebRunLocal服务，WebRunLocal服务根据传入的参数，动态的调用本地程序并返回结果，并将结果作为返回值返回，方便在网页中进行解析。

在网页中调用本地程序，一般采用的技术方案是ActiveX或NPAPI插件技术，其中ActiveX仅能够在IE浏览器中使用，随着Chrome，Firefox等浏览器基于安全性及稳定性的考虑，不再支持插件，NPAPI插件在高版本的浏览器上均不能正常的使用，想要访问本地程序就不得不固定浏览器版本，项目兼容性比较差。有的项目会在网页中调用第三方提供的本地程序，用以实现对电脑硬件的调用或者作为第三方项目之间的交互方式，然而第三方提供的程序稳定性无法得到保证，动辄无响应或者崩溃导致浏览器崩溃，极大的降低用户体验度。WebRunLocal在网页和本地程序之间增加了一个中间服务层，即保证了网页对本地程序的正常调用，又可以降低网页和本地程序之间的耦合度，增加程序的可拓展性，解决了在网页中调用本地程序出现的各种问题。

### 2. 兼容性要求
系统兼容性：1、Windows 7或以上操作系统；2、.net framework 4.5以上运行环境。
浏览器兼容性：全版本浏览器，支持http协议即可使用。

### 3. 使用场景
根据本地程序的种类，本地程序大致分为两种:

1. DLL插件，用于调用电脑本地硬件（打印机、扫描仪、读卡器等）或者通过DLL插件和第三方项目程序进行交互。
2. Exe可执行程序，主要用于集成第三方程序。

### 4. 程序优点

1. 低耦合，http请求本地服务的方式可以设置异步同步请求方式以及超时时间，不会因为本地程序的原因导致浏览器长时间无响应或崩溃。
2. 强兼容可拓展，WebRunLocal对外提供的http服务采用WebAPI实现，拓展方便。
3. 解决了大多数高版本浏览器不再支持插件的问题。
4. http的方式既可以网页调用也可以后台调用，后台调用方便进行事务以及日志的统一处理。

### 5. 程序安装及使用说明
解压软件包至实际磁盘，双击运行WRL.exe，在托盘程序中可以看到运行的插件管理程序，程序默认已设置为开机自启动，保证随时在线可用。
#### 5.1 目录说明
Log：日志目录
Plugins：第三方本地程序或者动态库目录
WRL.exe：windows 服务程序
WRL.exe.config：程序配置文件
WRL自动更新.exe：自动更新程序
WRL自动更新.exe.config：自动更新程序配置文件

#### 5.2 使用说明
WRL程序提供的http服务采用WebAPI方式实现，每个不同的第三方程序对使用者而言都是http请求，通过不同的url路径来区分各个第三方程序或者动态库，通过这样的开发使用约束，可以让WRL不断的集成不同的第三方程序，丰富功能。
#### 5.2.1 http请求示例程序
程序提供了Hello World的请求示例，在浏览器中输入以下地址，会将参数以JSON的方式返回
[http://127.0.0.1:8090/api/hello/getecho?name=XXX](http://127.0.0.1:8090/api/hello/getecho?name=XXX)
#### 5.2.2 dll调用示例
程序提供上海市医保动态库调用，在对接其他动态库程序时可以参考

#### 5.3 配置说明
软件包根目录下的WRL.exe.config为WebRunLocal服务的配置文件，通过它可以对本地系统服务进行一些配置，配置内容如下：
ListenerPort：设置http监听端口<br />
AutoStart：程序是否开机自启动<br />
AutoUpdate：是否自动更新<br />
DesktopLnk：是否创建桌面快捷方式<br />
RetainLogDays：日志保留天数<br />
PramaterLoggerPrint：是否将系统服务的入参出参输出到日志文件<br />

WRL自动更新.exe.config为自动更新程序的配置文件，配置内容如下：
WebSocketServiceAddr：服务器WebSocket服务地址<br />
UpdateFileUri：更新文件下载地址


启发于：[https://github.com/wangzuohuai/WebRunLocal](https://github.com/wangzuohuai/WebRunLocal)
