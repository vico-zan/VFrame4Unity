# VFrame4Unity
# 1.概述

VFrame是一个逻辑框架，主要用于流程化数据传递的过程。
UI部分基于UGUI进行扩展，并添加了一些本地化UI组件。

Excel数据表转化为unity3d内置dataasset或sqlite数据库格式。
SQLite功能，需另行添加SQLite包。


# 2.导入

2.1.首次导入

如果打开VFInstaller.cs脚本的[InitializeOnLoad]，则第一次导入会自动将Template目录下的几个文件放入相应的文件夹中。    

2.2.更新

新建一个空场景

删除VFrame目录

导入更新包

2.3.导出

若需要修改库，需要将NDefine.cs文件拖回Template文件夹中一并导出。导出完毕再拖回Core文件夹。

![image](https://github.com/vico-zan/Images/blob/master/img1.png?raw=true)

# 3.主要功能

# 4.本地化

4.1.1.Excel语言表与定义语言类型

注意以下三者的对应关系。

RFLocalize：

![image](https://github.com/vico-zan/Images/blob/master/img3.png?raw=true)

Vtexcel：

![image](https://github.com/vico-zan/Images/blob/master/img8.png?raw=true)

Excel表：

![image](https://github.com/vico-zan/Images/blob/master/img4.png?raw=true)

4.1.2.支持的本地化类型

根据UGUI的组件扩展而来

Text

Image

RawImage

![image](https://github.com/vico-zan/Images/blob/master/img5.png?raw=true)

4.1.3.添加方法

Create菜单中直接创建

在对应的ui物体上手动添加LocalizeText等组件

4.1.4.实时变更语言

使用RFlocalize的静态方法SetLanguage。


# 5.辅助功能

5.1.图片工具

UGUI的image控件设置图片有点特殊，需要先生成图片prefab，然后调用指定的prefab去设置sprite。

图片必须放在Src下的子目录中才能被自动转换成prefab

![image](https://github.com/vico-zan/Images/blob/master/img6.png?raw=true)

目前代码中只转换png图片，代码中可修改

![image](https://github.com/vico-zan/Images/blob/master/img7.png?raw=true)

目前目录结构尚不清晰，可自行根据需要进行修改整理


# 6.已知问题
