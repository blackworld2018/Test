# Test_1
1.物体Cube上可设置开始位置，结束位置，与动画时间。
2.点击屏幕左方按键in,out,pingpang分别实现不同的动画效果。
3.点击屏幕上方按键可以选择其中一种缓动方式移动。
# Test_2
1.做了一个点击屏幕按键下载图片并附着到的物体上的小例子实现多线程网络。
2.使用线程安全的queue储存下载到的数据，那个线程先下载完成就先使用那个。
3.使用HttpWebRequest配合Thread实现多线程下载。
# Test_3
1.首先使用协程动态创建10w个物体，并随机分布在屏幕上，并将每一个gameObject放入dictionary中管理。
2.随机移动摄像机，当gameObject进入camera范围，将其添加到专用的可见dictionary中，同时也将不在摄像机范围的gameObject移出。
