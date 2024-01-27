<p align="center">
  <img src="ManaPixel\Images\LOGO.png" height=120>
</p>

# ManaPixel-一键超分辨工具
![GitHub repo size](https://img.shields.io/github/repo-size/isNinesun/MANAPixel)
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/isNineSun/ManaPixel/total)
![GitHub issues](https://img.shields.io/github/issues/isNineSun/ManaPixel)
![GitHub License](https://img.shields.io/github/license/isNinesun/MANAPixel)



## Table of Contents
- [ManaPixel-一键超分辨工具](#manapixel-一键超分辨工具)
  - [Table of Contents](#table-of-contents)
  - [✨前言](#前言)
  - [🚀快速开始](#快速开始)
    - [🖼️动图演示（\>17MB）](#️动图演示17mb)
  - [😎进阶配置](#进阶配置)
    - [模型](#模型)
    - [缩放比例](#缩放比例)
    - [GPU选择](#gpu选择)
    - [TTA模式](#tta模式)
  - [🤓高级配置](#高级配置)
  - [🔊更新计划](#更新计划)
  - [💻写在最后](#写在最后)


## ✨前言
现在网上有很多用来超分辨率的工具，但是有的收费，有的有功能繁杂很多用不到且导致启动速度慢，很多时候我们自己用的时候并不太关注大多参数，只是想快速转个高清图片或者视频，看到一张好看的1080P图片想直接转4K拿来做壁纸而已，因此就有了这个工具的诞生，工具的初衷就是快捷简单，**即使不进行任何配置**，只要拖入图片，即可一键超分辨率且质量在线，当然，如果你有更多的需求，工具也提供进阶的高级配置，可以手动调整！    

## 🚀快速开始
1. 拖入图片至蓝色框内。    
2. 点击“MANA!”进行超分！    
3. 等待超分完成可点击“打开文件夹”打开超分辨率输出的文件夹（不进行配置的情况下输出至输入文件同文件夹）。  

![](https://raw.githubusercontent.com/isNineSun/img_repository/main/PixPin_2024-01-25_23-42-47.png)    

### 🖼️动图演示（>17MB）
![](https://raw.githubusercontent.com/isNineSun/img_repository/main/PixPin_2024-01-27_00-43-16.gif)    

## 😎进阶配置
本章节将进行软件界面可配置参数的介绍。    
### 模型
通过下拉框选择超分辨率模型，默认为默认为realesrgan-x4plus。
* realesrgan-x4plus：默认模型。
* realesrnet-x4plus
* realesrgan-x4plus-anime：针对动漫插画图像优化，生成图片有更小的体积。
* realesr-animevideov3：针对动漫视频（后续更新计划）。    
### 缩放比例
调整模型输出的缩放比例，默认为4。
### GPU选择
如果电脑有多个GPU的话可以进行GPU的选择，默认会自动选择GPU进行超分。
### TTA模式
**CharGpt**：
> TTA 是一种在模型推理时进行数据增强的技术，以提高模型的性能。具体来说，在 TTA 模式下，模型对输入图像进行多次处理，每次采用不同的数据增强方法（例如旋转、翻转、缩放等），然后对这些处理后的图像进行预测。最终的预测结果可能是多次预测结果的平均值或投票结果。
使用 TTA 可以提高模型的鲁棒性，因为它考虑了多个输入变换，有助于减轻模型对输入变化的敏感性。然而，这通常会导致推理时间的增加，因为需要对每个图像进行多次处理。

默认关闭，可以选择打开。    

## 🤓高级配置
高级功能正在开发中（施工中...）    

## 🔊更新计划
* 更多高级配置项目的上线，为用户提供更多的选择；
* 历史输出文件夹路径的记录功能；
* 配置项的保存、导入和导出功能；
* 视频超分辨率功能的支持！

## 💻写在最后
非常欢迎，也非常希望使用的大家能够给我提出更多的建议来让这款工具更好用，也希望用过这款工具感觉还不错的朋友帮忙给仓库点个⭐Star，谢谢！