# STG

---

## Unity版本 ： 2019.4.21f1c1

-----

## 简介

Unity实现的一个类似于东方Project中的STG系列游戏，实现的比较粗糙。由于是第一次做这种游戏缺乏经验，同时也没有策划方面的相关知识，因此游戏的可玩性基本上为 0 。可以作为一个简单的Unity练手项目。

---

## 项目总结

写完之后好好想了下，也让周围的朋友试玩了一下。得到了很多的反馈，其中不乏对游戏数值方面的吐槽、UI界面的吐槽、莫名其妙的BUG的吐槽。由于个人只是一个程序员，因此在这里只列举一下游戏代码书写和设计方面可以做出的一些优化和可以修复的BUG，如果有其他的可优化的地方，敬请指正。

- 敌机的移动，完全可以使用DOTween插件来代替，自己实现的移动方式有些还可以，有些则完全达不到目的，而DOTween插件则很轻松的解决了这些问题;
- UI的更新，完全可以把子机的控制器脚本设计为一个单例，添加一个事件，然后在游戏的UI控制脚本中对这个事件加入。这样就可以完成游戏角色的生命和UI的实时更新;
- 子弹过多时，游戏会出现明显的掉帧，应该是可以使用批处理来优化的，子弹模型除了材质中的颜色属性会有差异，其他的都没有差异，并且子弹的顶点属性和数目也不多，完全符合动态批处理的要求，使用批处理完全没有问题。
- 子弹的加速还未开始时，如果此时集中飞机，就会产生空引用异常，导致游戏出问题。解决方法是在给子弹加速之前判断这个子弹的物体还是否存在，这个也不容易实现，因此在子弹击中飞机之后，先把子弹的active属性设置为 false，然后判断他的 active 属性就可以了；
- 游戏的内部逻辑实际上可以使用 MVC 模式来优化，包括UI等等。

目前想到的暂时就只有这么些。
接触了一些大项目之后，突然意识到之前的代码的凌乱和一些优化的方法，更觉得写出来的这个项目的代码属实糟糕透顶。

---

## 补充：优化方面

跟大佬交流了一下，又发现了不少问题，不过基本上是优化方面的

- 关于碰撞检测，实际上根本不需要给子弹挂载触发器脚本，可以在被碰撞物体上实现触发器。这里简单的举一个例子，如果是在子弹的身上检测，那么假设圆环形的爆炸弹幕每次生成 12，那么每次生成的时候，会有12处新出现12个新子弹，而且是重叠的，也就是会会发生碰撞检测，一处发生的次数是 11 的阶乘次，12处就是 12的阶乘次，大概四亿多次的代码执行。这还只是一个环形爆炸弹幕。如果是在角色身上进行判断，可以大幅度的减少这个运算量，掉帧也就不会太明显。
- 还有一个方法就是使用对象池，不过由于弹幕游戏的弹幕数量不确定，所以对象池在这里似乎并不是特别的合适，可以对一些比较少的，大型的弹幕使用对象池。

---

总而言之目前的实现很烂，虽然代码量也不大，但是改起来还是太麻烦，感觉改的话基本上就是重新把项目做一遍的样子，就暂时先不改了，以后如果有时间可以重新的制作一下这个项目。这里只是练手就不耗费太多的时间在上面了。


