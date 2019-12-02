#设置 unity 安装路径
export UNITY_PATH=/Applications/Unity/Hub/Editor/2019.2.9f1/unity.app/Contents/MacOS/Unity

#设置工程路径
export PROJECT_PATH=/Users/biweixiong/Documents/GitHub/Unity/UGUIEditor

#设置工程需要执行的方法
export METHOD_NAME=EditorMenu.AddFullScreenWindow

#设置 log 输出路径
export LOG_PATH=.

#执行 Method 方法
$UNITY_PATH -quit -batchmode -logFile $LOG_PATH -projectPath $PROJECT_PATH  -executeMethod $METHOD_NAME