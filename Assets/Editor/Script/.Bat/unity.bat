#设置 unity 安装路径
set UNITY_PATH=D:\programfiles\Unity5.6.3\Editor\Unity.exe

#设置工程路径
set PROJECT_PATH=D:\BuildApk

#设置工程需要执行的方法
set METHOD_NAME=Builder.BuildApk

#设置 log 输出路径
set LOG_PATH=%cd%\unity_log.txt

#执行 Method 方法
%UNITY_PATH% -quit -batchmode -logFile %LOG_PATH% -projectPath %PROJECT_PATH% -executeMethod %METHOD_NAME%